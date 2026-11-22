using System.ComponentModel.DataAnnotations;

namespace MY_Academy_Portfolio2.Entities
{
    public class Education
    {
        public int EducationId { get; set; }

        [Required(ErrorMessage = "Okul / Kurum adı zorunludur.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Okul adı 2 ile 150 karakter arasında olmalıdır.")]
        [Display(Name = "Okul / Kurum")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Bölüm / Derece")]
        public string? SubTitle { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tarih / Süre bilgisi zorunludur.")]
        [Display(Name = "Tarih Aralığı")]
        public string Duration { get; set; } = string.Empty;
    }
}
