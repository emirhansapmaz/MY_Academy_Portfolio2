using System.ComponentModel.DataAnnotations;

namespace MY_Academy_Portfolio2.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        [Required(ErrorMessage = "Pozisyon / Ünvan alanı zorunludur.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Pozisyon 2 ile 150 karakter arasında olmalıdır.")]
        [Display(Name = "Pozisyon")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Şirket adı 2 ile 150 karakter arasında olmalıdır.")]
        [Display(Name = "Şirket Adı")]
        public string Company { get; set; } = string.Empty;

        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Alt Başlık / Konum")]
        public string SideTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "Süre / Tarih aralığı zorunludur.")]
        [Display(Name = "Süre")]
        public string Duration { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çalışma türü zorunludur.")]
        [Display(Name = "Çalışma Türü")]
        public string WorkType { get; set; } = string.Empty;

        [Display(Name = "Aktiflik Durumu")]
        public bool IsActive { get; set; } = true;
    }
}
