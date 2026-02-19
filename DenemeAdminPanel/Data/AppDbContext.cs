using DenemeAdminPanel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenemeAdminPanel.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options)  : base(options) { }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<MiniApp> MiniApps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Profesyonel Not: Burada veritabanı tabloların için özel kurallar yazabilirsin.
            // Örneğin: "Tüm tablolar oluşturulurken Id alanı birincil anahtar olsun."
        }
    }
}
