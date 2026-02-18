using DenemeAdminPanel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DenemeAdminPanel.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options)  : base(options) { }

        public DbSet<Announcement> Announcements { get; set; }
    }
}
