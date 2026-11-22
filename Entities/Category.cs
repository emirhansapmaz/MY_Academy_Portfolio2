using System.ComponentModel.DataAnnotations;

namespace MY_Academy_Portfolio2.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Kategori adı 2 ile 50 karakter arasında olmalıdır.")]
        [Display(Name = "Kategori Adı")]
        public string CategoryName { get; set; } = string.Empty;

        public List<Project>? Projects { get; set; }
    }
}
