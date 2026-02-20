using DenemeAdminPanel.Entities;
using DenemeAdminPanel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DenemeAdminPanel.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<MiniApp> MiniApps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // GÜNCELLENMİŞ SABİT ID'LER
            string adminId = "2c5e174e-3b0e-446f-86af-483d56fd7210";
            string editorId = "3d6f285f-4c1f-557g-97bg-594e67ge8321";
            string financeId = "5f8h407i-6e3i-779i-19di-716g89ig0543";

            // Sadece Admin, Editor ve Finance rollerini HasData ile ekliyoruz
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = editorId, Name = "Editor", NormalizedName = "EDITOR" },
                new IdentityRole { Id = financeId, Name = "Finance", NormalizedName = "FINANCE" }
            );
        }
    }
}