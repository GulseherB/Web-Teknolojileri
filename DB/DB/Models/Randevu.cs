using System.ComponentModel.DataAnnotations;

namespace DB.Models
{
    public class Randevu
    {
        [Key]
        public int RandevuID { get; set; }



        public string RandevuGun { get; set; }
        public string RandevuSaat { get; set; }
        public int HesapID { get; set; }
        public Hesap Hesap { get; set; }


    }
}
