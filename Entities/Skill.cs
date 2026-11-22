using System.ComponentModel.DataAnnotations;

namespace MY_Academy_Portfolio2.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Yetenek başlığı zorunludur.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Yetenek adı 1 ile 100 karakter arasında olmalıdır.")]
        [Display(Name = "Yetenek Adı")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yetenek seviyesi zorunludur.")]
        [Range(0, 100, ErrorMessage = "Yetenek yüzdesi 0 ile 100 arasında olmalıdır.")]
        [Display(Name = "Yetenek Seviyesi (%)")]
        public int Value { get; set; }

        [Display(Name = "Kategori")]
        public string? Category { get; set; }
    }
}
