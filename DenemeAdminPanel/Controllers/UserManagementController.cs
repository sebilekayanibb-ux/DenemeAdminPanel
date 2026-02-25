using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DenemeAdminPanel.Entities; // UserLog için gerekli
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using DenemeAdminPanel.Data; // ApplicationDbContext için gerekli

namespace DenemeAdminPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly AppDbContext _context; // Veritabanı bağlantısı eklendi

        public UserManagementController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _context = context;
        }

        // Yardımcı Metot: Log Kaydı Oluşturma
        private async Task LogAction(string email, string action)
        {
            var log = new UserLog
            {
                Email = email,
                Action = action,
                Date = DateTime.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
            };
            _context.UserLogs.Add(log);
            await _context.SaveChangesAsync();
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

            ViewBag.AllRoles = await _roleManager.Roles
                .Where(r => r.Name == "Admin" || r.Name == "Editor" || r.Name == "Finance")
                .Select(r => r.Name).ToListAsync();

            return View(userRolesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password, string roleName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "E-posta ve şifre zorunludur.";
                return RedirectToAction(nameof(Index));
            }

            var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(roleName)) await _userManager.AddToRoleAsync(user, roleName);

                // LOGLAMA
                await LogAction(email, $"Yeni personel oluşturuldu. Rol: {roleName} (Yapan: {User.Identity.Name})");

                TempData["Success"] = "Personel eklendi.";
            }
            else { TempData["Error"] = "Hata oluştu."; }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Contains("Admin") && roleName != "Admin")
            {
                var admins = await _userManager.GetUsersInRoleAsync("Admin");
                if (admins.Count <= 1)
                {
                    TempData["Error"] = "Sistemde en az bir Admin kalmalıdır.";
                    return RedirectToAction(nameof(Index));
                }
            }

            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!string.IsNullOrEmpty(roleName)) await _userManager.AddToRoleAsync(user, roleName);

            // LOGLAMA
            await LogAction(user.Email, $"Yetki güncellendi. Eski: {string.Join(",", currentRoles)}, Yeni: {roleName}");

            TempData["Success"] = "Yetki güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SystemLogs()
        {
            // Logları en yeni tarihten en eskiye doğru sıralayarak listeler
            var logs = await _context.UserLogs
                .OrderByDescending(l => l.Date)
                .ToListAsync();
            return View(logs);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Contains("Admin"))
            {
                var admins = await _userManager.GetUsersInRoleAsync("Admin");
                if (admins.Count <= 1)
                {
                    TempData["Error"] = "Son Admin silinemez.";
                    return RedirectToAction(nameof(Index));
                }
            }

            var userEmail = user.Email;
            await _userManager.DeleteAsync(user);

            // LOGLAMA
            await LogAction(userEmail, "Personel sistemden silindi.");

            TempData["Success"] = "Personel silindi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SendResetEmail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code = encodedToken },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "İBB Admin - Şifre Sıfırlama",
                $"<div style='font-family:Arial; padding:20px; border:1px solid #ddd; border-radius:10px;'>" +
                $"<h2 style='color:#E5007D;'>Şifre Sıfırlama</h2>" +
                $"<p>Şifrenizi yenilemek için aşağıdaki butona tıklayın.</p>" +
                $"<div style='text-align:center; margin-top:25px;'>" +
                $"<a href='{callbackUrl}' style='background:#E5007D; color:white; padding:12px 25px; text-decoration:none; border-radius:5px; font-weight:bold; display:inline-block;'>Şifremi Sıfırla</a>" +
                $"</div></div>");

            // LOGLAMA
            await LogAction(user.Email, $"Yönetici ({User.Identity.Name}) tarafından şifre sıfırlama maili tetiklendi.");

            TempData["Success"] = "Şifre sıfırlama maili başarıyla gönderildi.";
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