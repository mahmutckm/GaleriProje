using System.ComponentModel.DataAnnotations;

namespace ArabaGaleri.Models
{
    public class Kullanici
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        [Display(Name = "Kullanıcı Adı")]
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

        [Display(Name = "Admin mi?")]
        public bool IsAdmin { get; set; } = false;

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }
}




