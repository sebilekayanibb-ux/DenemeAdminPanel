
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DenemeAdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var viewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };
                userRolesViewModel.Add(viewModel);
            }

            ViewBag.AllRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return View(userRolesViewModel);
        }

        // --- YENİ PERSONEL EKLEME ---
        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password, string roleName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "E-posta ve şifre boş bırakılamaz.";
                return RedirectToAction(nameof(Index));
            }

            var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(roleName))
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                TempData["Success"] = "Yeni personel başarıyla oluşturuldu.";
            }
            else
            {
                TempData["Error"] = "Hata: " + string.Join(", ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction(nameof(Index));
        }

        // --- ROL DEĞİŞTİRME VE KORUMA MANTIĞI ---
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            bool isAdmin = currentRoles.Contains("Admin");

            // KRİTİK KONTROL: Eğer kullanıcı Admin rolünü bırakıyorsa, sistemde başka Admin var mı?
            if (isAdmin && roleName != "Admin")
            {
                var allAdmins = await _userManager.GetUsersInRoleAsync("Admin");
                if (allAdmins.Count <= 1)
                {
                    TempData["Error"] = "Sistemde en az bir Admin bulunmalıdır. Kendi yetkinizi almadan önce başka bir personeli Admin olarak atamalısınız.";
                    return RedirectToAction(nameof(Index));
                }
            }

            // Rol Güncelleme
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!string.IsNullOrEmpty(roleName))
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }

            TempData["Success"] = "Yetki başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
    }

    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}