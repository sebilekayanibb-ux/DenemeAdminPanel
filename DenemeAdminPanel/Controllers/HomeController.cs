using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DenemeAdminPanel.Data;
using System.Linq;

namespace DenemeAdminPanel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // İstatistik Verileri
            ViewBag.TotalAnnouncements = _context.Announcements.Count();
            ViewBag.ActiveAnnouncements = _context.Announcements.Count(x => x.IsActive);
            ViewBag.MiniAppCount = _context.MiniApps.Count();

            // Kullanıcıya özel mesaj
            if (User.IsInRole("Admin"))
                ViewBag.Message = "Sistem tam yetki ile yönetiliyor.";
            else if (User.IsInRole("Analyst"))
                ViewBag.Message = "Veri analiz ve raporlama modu aktif.";
            else
                ViewBag.Message = "Personel işlem paneli.";

            return View();
        }
    }
}