using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DenemeAdminPanel.Entities
{
    public class Announcement : BaseEntity
    {
        [Required(ErrorMessage = "Lütfen duyuru başlığını giriniz.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Lütfen duyuru içeriğini yazınız.")]
        public string Content { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public string? SelectedPresetImage { get; set; }

        // İngilizce hatayı engellemek için nullable yaptık
        [Required(ErrorMessage = "Yayın başlangıç tarihi zorunludur.")]
        public DateTime? StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

        // İngilizce hatayı engellemek için nullable yaptık
        [Required(ErrorMessage = "Görüntüleme sırası zorunludur.")]
        public int? DisplayOrder { get; set; }
    }
}