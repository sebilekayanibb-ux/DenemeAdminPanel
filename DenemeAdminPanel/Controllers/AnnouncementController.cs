using DenemeAdminPanel.Data;
using DenemeAdminPanel.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DenemeAdminPanel.Controllers
{
    [Authorize]
    public class AnnouncementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public AnnouncementController(AppDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            // Listeyi her zaman güncel sıralamaya göre getiriyoruz
            return View(await _context.Announcements.OrderBy(x => x.DisplayOrder).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Announcement announcement)
        {
            if (announcement.ImageFile == null && string.IsNullOrEmpty(announcement.SelectedPresetImage))
            {
                ModelState.AddModelError("ImageUrl", "Lütfen bir görsel ekleyin veya seçin!");
            }

            if (ModelState.IsValid)
            {
                if (announcement.ImageFile != null)
                    announcement.ImageUrl = await SaveImage(announcement.ImageFile);
                else if (!string.IsNullOrEmpty(announcement.SelectedPresetImage))
                    announcement.ImageUrl = announcement.SelectedPresetImage;

                announcement.CreatedDate = DateTime.Now;
                _context.Add(announcement);
                await _context.SaveChangesAsync();

                // YENİ: Kayıt sonrası tüm sıraları 1'den başlayarak yeniden diz
                await ReorderAllAnnouncements();

                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return NotFound();
            return View(announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Announcement announcement)
        {
            if (id != announcement.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = await _context.Announcements.FindAsync(id);
                if (existing == null) return NotFound();

                if (announcement.ImageFile != null)
                    existing.ImageUrl = await SaveImage(announcement.ImageFile);
                else if (!string.IsNullOrEmpty(announcement.SelectedPresetImage))
                    existing.ImageUrl = announcement.SelectedPresetImage;

                existing.Title = announcement.Title;
                existing.Content = announcement.Content;
                existing.StartDate = announcement.StartDate;
                existing.EndDate = announcement.EndDate;
                existing.DisplayOrder = announcement.DisplayOrder;
                existing.IsActive = announcement.IsActive;

                await _context.SaveChangesAsync();

                // YENİ: Düzenleme sonrası tüm sıraları 1'den başlayarak yeniden diz
                await ReorderAllAnnouncements();

                return RedirectToAction(nameof(Index));
            }
            return View(announcement);
        }

        // --- ARDIŞIK SIRALAMA MOTORU ---
        // Bu metod, her işlemden sonra veritabanındaki tüm duyuruları gezer, 
        // DisplayOrder'a göre dizer ve 1, 2, 3... diye boşluksuz numaralandırır.
        private async Task ReorderAllAnnouncements()
        {
            var all = await _context.Announcements
                .OrderBy(x => x.DisplayOrder)
                .ThenByDescending(x => x.CreatedDate) // Aynı sıra numarasında yeniyi üste al
                .ToListAsync();

            int currentOrder = 1;
            foreach (var item in all)
            {
                item.DisplayOrder = currentOrder++;
            }
            await _context.SaveChangesAsync();
        }

        private async Task<string> SaveImage(IFormFile file)
        {
            string folder = "images/announcements/";
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(_webHost.WebRootPath, folder);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/" + folder + fileName;
        }
    }
}