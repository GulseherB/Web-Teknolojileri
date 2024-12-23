using System.ComponentModel.DataAnnotations;

namespace denemekicin1.Models
{
    public class Calisan
    {
        [Key]
        public int CalisanID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Çalışan Adı")]
        public string CalisanAd { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Çalışan Soyadı")]
        public string CalisanSoyad { get; set; }

        [Phone]
        [Display(Name = "Telefon Numarası")]
        public string Telefon { get; set; }

        public ICollection<Randevu>? Randevular { get; set; }
    }
}
