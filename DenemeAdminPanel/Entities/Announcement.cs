namespace DenemeAdminPanel.Entities
{
    public class Announcement : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; } // İstanbul Senin'deki banner resimleri için
    }
}