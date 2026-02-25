using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class SettingsController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public SettingsController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendResetEmail()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        // Burası mevcut ForgotPassword mantığını tetikler
        return RedirectToPage("/Account/ForgotPassword", new { area = "Identity", email = user.Email });
    }
}