namespace DenemeAdminPanel.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true; // Görsellerdeki "Aktif" rozeti için
    }
}
//BaseEntity abstract (soyut) bir sınıftır.
//Onun amacı kendi başına bir tablo oluşturmak değil,
//Announcement gibi gerçek sınıflara Id, CreatedDate ve IsActive özelliklerini miras (inheritance) yoluyla aktarmaktır.