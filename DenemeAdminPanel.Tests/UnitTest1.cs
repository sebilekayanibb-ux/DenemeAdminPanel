using DenemeAdminPanel.Controllers;
using DenemeAdminPanel.Data;
using DenemeAdminPanel.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DenemeAdminPanel.Tests
{
    public class MiniAppControllerTests
    {
        // RAM üzerinde çalışan geçici bir DbContext oluşturur
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact] // Bu bir test metodudur
        public async Task Create_Post_ValidApp_RedirectsToIndex()
        {
            // --- ARRANGE (Hazırlık) ---
            var dbContext = GetInMemoryDbContext();
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            var controller = new MiniAppController(dbContext, mockEnvironment.Object);

            var newApp = new MiniApp
            {
                Name = "Test Uygulaması",
                AppUrl = "https://test.ibb.gov.tr",
                IsActive = true
            };

            // --- ACT (Eylem) ---
            var result = await controller.Create(newApp, null);

            // --- ASSERT (Doğrulama) ---
            // 1. İşlem bittikten sonra Index'e yönlendirdi mi?
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            // 2. Veritabanına gerçekten kaydedildi mi?
            var appInDb = await dbContext.MiniApps.FirstOrDefaultAsync(x => x.Name == "Test Uygulaması");
            Assert.NotNull(appInDb);
            Assert.Equal(1, appInDb.DisplayOrder); // İlk eklenen uygulama olduğu için sırası 1 olmalı
        }

        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsViewWithModel()
        {
            // --- ARRANGE ---
            var dbContext = GetInMemoryDbContext();
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            var controller = new MiniAppController(dbContext, mockEnvironment.Object);

            // Model hatası simüle ediyoruz (Örn: Isim boş bırakılmış)
            controller.ModelState.AddModelError("Name", "Gerekli");

            var invalidApp = new MiniApp { Name = "" };

            // --- ACT ---
            var result = await controller.Create(invalidApp, null);

            // --- ASSERT ---
            // Model hatalı olduğu için tekrar aynı View'ı döndürmeli
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(invalidApp, viewResult.Model);
        }
    }
}