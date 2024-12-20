using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    public class Hesap
    {
        [Key] // Birincil anahtar olduğunu belirtir
        public int HesapID { get; set; } // Varsayılan olarak Identity olur
        [Required]
        [MaxLength(100)]
        [Display(Name = "hesap isim")]
        public string HesapAd { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "hesap soyisim")]
        public string HesapSoyad { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "E-posta Adresi")]
        public string HesapEmail { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        [Display(Name = "Şifre")]
        public string HesapSifre { get; set; }

      
        public ICollection<Randevu>? Randevular { get; set; }
    }
}
