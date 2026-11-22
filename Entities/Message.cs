using System.ComponentModel.DataAnnotations;

namespace MY_Academy_Portfolio2.Entities
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Ad ve Soyad alanı zorunludur.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2 ile 100 karakter arasında olmalıdır.")]
        [Display(Name = "Ad Soyad")]
        public string NameSurname { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz.")]
        [StringLength(100, ErrorMessage = "E-posta en fazla 100 karakter olabilir.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesaj konusu zorunludur.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Konu en az 3, en fazla 150 karakter olmalıdır.")]
        [Display(Name = "Konu")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mesaj içeriği boş bırakılamaz.")]
        [StringLength(2000, MinimumLength = 5, ErrorMessage = "Mesajınız en az 5, en fazla 2000 karakter olmalıdır.")]
        [Display(Name = "Mesaj İçeriği")]
        public string MessageContent { get; set; } = string.Empty;

        public DateTime SendDate { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;
    }
}
