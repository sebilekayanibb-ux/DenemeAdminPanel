namespace DenemeAdminPanel.Entities
{
    public class MiniApp : BaseEntity
    {
        public string Name { get; set; } // Örn: İBB Wi-Fi
        public string IconPath { get; set; } // İkon görseli yolu
        public string AppUrl { get; set; } // Tıklayınca gideceği link
    }
}
