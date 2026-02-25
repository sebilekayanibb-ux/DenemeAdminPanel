using DenemeAdminPanel.Data;
using DenemeAdminPanel.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace DenemeAdminPanel.Controllers
{
    [Authorize]
    public class MiniAppController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public MiniAppController(AppDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // Listeleme Sayfası
        public async Task<IActionResult> Index()
        {
            // Sayfa her açıldığında sıraların ardışık (1,2,3...) olduğundan emin oluyoruz
            await ReOrderApps();
            var apps = await _context.MiniApps.OrderBy(x => x.DisplayOrder).ToListAsync();
            return View(apps);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MiniApp miniApp, IFormFile IconFile)
        {
            if (ModelState.IsValid)
            {
                if (IconFile != null)
                {
                    miniApp.IconUrl = await SaveIcon(IconFile);
                }

                miniApp.CreatedDate = DateTime.Now;

                // Eğer kullanıcı manuel bir sıra girmediyse (0 veya boşsa) en sona at
                if (miniApp.DisplayOrder <= 0)
                {
                    var maxOrder = await _context.MiniApps.AnyAsync()
                                   ? await _context.MiniApps.MaxAsync(x => x.DisplayOrder)
                                   : 0;
                    miniApp.DisplayOrder = maxOrder + 1;
                }

                _context.Add(miniApp);
                await _context.SaveChangesAsync();

                // ARDIŞIKLIK GARANTİSİ: Yeni eklenenle birlikte tüm listeyi 1'den başlayarak tekrar diz
                await ReOrderApps();

                TempData["SuccessMessage"] = "Uygulama başarıyla eklendi!";
                return RedirectToAction(nameof(Index));
            }

            return View(miniApp);
        }
        // Tekli Silme İşlemi
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var app = await _context.MiniApps.FindAsync(id);
            if (app == null) return NotFound();

            // İkon dosyasını fiziksel olarak sil (opsiyonel ama temiz tutar)
            if (!string.IsNullOrEmpty(app.IconUrl))
            {
                var filePath = Path.Combine(_webHost.WebRootPath, app.IconUrl.TrimStart('/'));
                if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
            }

            _context.MiniApps.Remove(app);
            await _context.SaveChangesAsync();

            // Silme sonrası sıraları kaydırarak düzelt
            await ReOrderApps();

            return Json(new { success = true, message = "Uygulama başarıyla silindi." });
        }


        // Toplu Silme İşlemi
        [HttpPost]
        public async Task<IActionResult> BulkDelete([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any()) return BadRequest();

            var appsToDelete = await _context.MiniApps.Where(x => ids.Contains(x.Id)).ToListAsync();
            _context.MiniApps.RemoveRange(appsToDelete);
            await _context.SaveChangesAsync();

            // Sıraları yeniden düzenle
            await ReOrderApps();

            return Json(new { success = true, message = $"{appsToDelete.Count} uygulama başarıyla silindi." });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] List<OrderUpdateModel> orders)
        {
            if (orders == null) return BadRequest();

            foreach (var item in orders)
            {
                var app = await _context.MiniApps.FindAsync(item.Id);
                if (app != null)
                {
                    app.DisplayOrder = item.NewOrder;
                }
            }
            await _context.SaveChangesAsync();

            // Sürükle bırak sonrası sayıların kopmaması için tekrar düzenle
            await ReOrderApps();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var app = await _context.MiniApps.FindAsync(id);
            if (app == null) return NotFound();
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MiniApp miniApp, IFormFile? IconFile)
        {
            if (id != miniApp.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingApp = await _context.MiniApps.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                    if (existingApp == null) return NotFound();

                    if (IconFile != null)
                        miniApp.IconUrl = await SaveIcon(IconFile);
                    else
                        miniApp.IconUrl = existingApp.IconUrl;

                    miniApp.CreatedDate = existingApp.CreatedDate;

                    _context.Update(miniApp);
                    await _context.SaveChangesAsync();

                    // Manuel sayı girişinden sonra ardışıklığı bozmamak için listeyi mühürle
                    await ReOrderApps();

                    TempData["SuccessMessage"] = "Uygulama bilgileri güncellendi!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiniAppExists(miniApp.Id)) return NotFound();
                    else throw;
                }
            }
            return View(miniApp);
        }

        // ARDIŞIK SIRALAMA GARANTİSİ: Listenin her zaman 1, 2, 3... gitmesini sağlar
        private async Task ReOrderApps()
        {
            var allApps = await _context.MiniApps
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.CreatedDate)
                .ToListAsync();

            int counter = 1;
            foreach (var app in allApps)
            {
                if (app.DisplayOrder != counter)
                {
                    app.DisplayOrder = counter;
                    _context.Update(app);
                }
                counter++;
            }
            await _context.SaveChangesAsync();
        }

        private bool MiniAppExists(int id) => _context.MiniApps.Any(e => e.Id == id);

        private async Task<string> SaveIcon(IFormFile file)
        {
            string folder = "images/miniapps/";
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string path = Path.Combine(_webHost.WebRootPath, folder);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/" + folder + fileName;
        }

        public class OrderUpdateModel
        {
            public int Id { get; set; }
            public int NewOrder { get; set; }
        }
    }
}