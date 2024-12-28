
using System.ComponentModel.DataAnnotations;

namespace denemekicin1.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }

        [Required(ErrorMessage = "Randevu günü seçilmelidir.")]
        public string RandevuGun { get; set; }

        [Required(ErrorMessage = "Randevu saati seçilmelidir.")]
        public int RandevuSaat { get; set; }

        public int RandevuBitisSaati { get; set; } // Yeni alan

        [Required]
        public int HesapID { get; set; }

        public Hesap Hesap { get; set; }

        [Required]
        public int CalisanID { get; set; } // Yeni çalışan ilişkisi

        public Calisan Calisan { get; set; } // Yeni çalışan ilişkisi

        [Required(ErrorMessage = "En az bir saç işlemi seçilmelidir.")]
        public string SacIslemleri { get; set; } // Virgülle ayrılmış saç işlemleri

        public int IslemSuresi { get; set; } // Yeni alan
        public int ToplamTutar { get; set; } // Ödenecek toplam tutar
    }
}
