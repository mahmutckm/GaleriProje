using System.ComponentModel.DataAnnotations;

namespace ArabaGaleri.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [Display(Name = "Kullanıcı Adı")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3-50 karakter arasında olmalıdır")]
        public string KullaniciAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Sifre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrar zorunludur")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Sifre", ErrorMessage = "Şifreler eşleşmiyor")]
        public string SifreTekrar { get; set; } = string.Empty;
    }
}




