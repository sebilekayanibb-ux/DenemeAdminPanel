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
            // Hataya sebep olan uyarıyı devre dışı bırakıyoruz
            optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // SABİT ID'LER
            string adminId = "2c5e174e-3b0e-446f-86af-483d56fd7210";
            string editorId = "3d6f285f-4c1f-557g-97bg-594e67ge8321";
            string supportId = "4e7g396h-5d2h-668h-08ch-605f78hf9432";
            string financeId = "5f8h407i-6e3i-779i-19di-716g89ig0543";
            string analystId = "6g9i518j-7f4j-880j-20ej-827h90jh1654";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = editorId, Name = "Editor", NormalizedName = "EDITOR" },
                new IdentityRole { Id = supportId, Name = "Support", NormalizedName = "SUPPORT" },
                new IdentityRole { Id = financeId, Name = "Finance", NormalizedName = "FINANCE" },
                new IdentityRole { Id = analystId, Name = "Analyst", NormalizedName = "ANALYST" }
            );
        }
    }
}