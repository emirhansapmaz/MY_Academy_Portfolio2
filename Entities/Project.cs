using System.ComponentModel.DataAnnotations;

namespace MY_Academy_Portfolio2.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Proje başlığı zorunludur.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Proje başlığı 2 ile 150 karakter arasında olmalıdır.")]
        [Display(Name = "Proje Başlığı")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Proje açıklaması zorunludur.")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Açıklama 5 ile 1000 karakter arasında olmalıdır.")]
        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "GitHub bağlantısı zorunludur.")]
        [Url(ErrorMessage = "Lütfen geçerli bir URL giriniz.")]
        [Display(Name = "GitHub URL")]
        public string GithubUrl { get; set; } = string.Empty;

        [Display(Name = "Kapak Görseli")]
        public string CoverImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen bir kategori seçiniz.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
