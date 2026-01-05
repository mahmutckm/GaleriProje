using System.ComponentModel.DataAnnotations;

namespace ArabaGaleri.Models
{
    public class Araba
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Marka zorunludur")]
        [Display(Name = "Marka")]
        public string Marka { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model zorunludur")]
        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yıl zorunludur")]
        [Display(Name = "Yıl")]
        public int Yil { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Display(Name = "Fiyat")]
        [DataType(DataType.Currency)]
        public decimal Fiyat { get; set; }

        [Display(Name = "Kilometre")]
        public int? Kilometre { get; set; }

        [Display(Name = "Yakıt Tipi")]
        public string? YakıtTipi { get; set; }

        [Display(Name = "Vites Tipi")]
        public string? VitesTipi { get; set; }

        [Display(Name = "Renk")]
        public string? Renk { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string? Aciklama { get; set; }

        [Display(Name = "Resim URL")]
        public string? ResimUrl { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        [Display(Name = "Satıldı mı?")]
        public bool SatildiMi { get; set; } = false;
    }
}




