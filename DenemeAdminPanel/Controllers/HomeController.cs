using DenemeAdminPanel.Data;
using DenemeAdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore; 

namespace DenemeAdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context) { _context = context; }

        public async Task<IActionResult> Index()
        {
            // Duyuru sayısını veritabanından çekip Dashboard'a gönderiyoruz
            ViewBag.AnnouncementCount = await _context.Announcements.CountAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
