using DenemeAdminPanel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Identity için gerekli
using DenemeAdminPanel.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. MVC ve Razor Runtime Compilation Desteği
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// 2. Veritabanı Bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // Giriş sayfası yolu
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Yetki hatası yolu
});

// 3. Identity Servislerini Ekle (Admin Girişi İçin)
// Identity Ayarları: .AddRoles<IdentityRole>() eklendiğinden emin ol!
builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() // İŞTE EKSİK OLAN SATIR BURASI!
.AddEntityFrameworkStores<AppDbContext>();

// MAILKIT SERVISI KAYDI
builder.Services.AddTransient<IEmailSender, EmailSender>();

// 4. Razor Pages Desteği (Identity'nin kendi sayfaları için zorunlu)
builder.Services.AddRazorPages();

var app = builder.Build();

// HTTP İstek Hattı Yapılandırması (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// 5. KİMLİK DOĞRULAMA (Sıralama Çok Önemli!)
app.UseAuthentication(); // "Sen kimsin?" sorusunu sorar
app.UseAuthorization();  // "Bu sayfaya girmeye yetkin var mı?" der

app.MapStaticAssets();

// 6. Rota Tanımlamaları
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Announcement}/{action=Index}/{id?}")
    .WithStaticAssets();

// Identity için gerekli olan Razor Pages eşlemesi
app.MapRazorPages();

app.Run();