using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }

        [Required(ErrorMessage = "Randevu günü seçilmelidir.")]
        public string RandevuGun { get; set; }

        [Required(ErrorMessage = "Randevu saati seçilmelidir.")]
        public string RandevuSaat { get; set; }

        [Required]
        public int HesapID { get; set; }

        public Hesap Hesap { get; set; }

        [Required(ErrorMessage = "En az bir saç işlemi seçilmelidir.")]
        public string SacIslemleri { get; set; } // Virgülle ayrılmış saç işlemleri

        [Range(0, double.MaxValue, ErrorMessage = "Toplam tutar negatif olamaz.")]
        public decimal ToplamTutar { get; set; } // Ödenecek toplam tutar
    }
}
