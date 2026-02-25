namespace DenemeAdminPanel.Entities // Buranın tam olarak böyle olduğundan emin ol
{
    public class MiniApp
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AppUrl { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public string? PermissionText { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
