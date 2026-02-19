using Microsoft.AspNetCore.Mvc;
using DenemeAdminPanel.Entities;
using DenemeAdminPanel.Data;
using Microsoft.EntityFrameworkCore;

namespace DenemeAdminPanel.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly AppDbContext _context;

        // Veritabanı bağlantısını Constructor üzerinden alıyoruz (Dependency Injection)
        public AnnouncementController(AppDbContext context)
        {
            _context = context;
        }

        // 1. LİSTELEME: Tüm duyuruları veritabanından çeker ve Index sayfasına gönderir.
        public async Task<IActionResult> Index()
        {
            var announcements = await _context.Announcements.ToListAsync();
            return View(announcements);
        }

        // 2. EKLEME (Görüntüleme): Yeni duyuru ekleme formunu açar.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. EKLEME (Kaydetme): Formdan gelen veriyi veritabanına kaydeder.
        [HttpPost]
        [ValidateAntiForgeryToken] // Güvenlik için şart!
        public async Task<IActionResult> Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Başarılıysa listeye dön
            }
            return View(announcement); // Hata varsa formu tekrar göster
        }

        // 4. SİLME: Belirli bir ID'ye sahip duyuruyu siler.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        // 1. DÜZENLEME FORMU (Sayfayı Açar)
        public async Task<IActionResult> Edit(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement == null) return NotFound();
            return View(announcement);
        }

        // 2. GÜNCELLEME (Veriyi Kaydeder)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Announcement announcement)
        {
            if (id != announcement.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = await _context.Announcements.FindAsync(id);
                if (existing == null) return NotFound();

                existing.Title = announcement.Title;
                existing.Content = announcement.Content;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(announcement);
        }
    }
}
