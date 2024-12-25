using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using denemekicin1.Models;
using System;


namespace denemekicin1.Controllers
{
    public class islemController : Controller
    {
       


        hesaprandevuContext k = new hesaprandevuContext();
        hesaprandevuContext m = new hesaprandevuContext();
        public IActionResult HesapGoster()
        {
            var hesaplar = k.Hesaplar.ToList();
            return View(hesaplar);
        }
        public IActionResult CalisanGoster()
        {
            var calisanlar = m.Calisanlar.ToList(); // Doğru tabloyu alıyoruz
            return View(calisanlar); // Çalışan listesini gönderiyoruz
        }


        public IActionResult HesapDuzenle(int id)
        {
            var hesap = k.Hesaplar.FirstOrDefault(h => h.HesapID == id);
            if (hesap == null)
            {
                TempData["msj"] = "Düzenlemek istediğiniz hesap bulunamadı.";
                return RedirectToAction("HesapGoster");
            }

            return View(hesap);
        }

        [HttpPost]
        public IActionResult HesapDuzenle(Hesap hesap)
        {
            if (!ModelState.IsValid)
            {
                TempData["msj"] = "Lütfen tüm alanları doğru şekilde doldurun.";
                return View(hesap);
            }

            var mevcutHesap = k.Hesaplar.FirstOrDefault(h => h.HesapID == hesap.HesapID);
            if (mevcutHesap == null)
            {
                TempData["msj"] = "Düzenlemek istediğiniz hesap bulunamadı.";
                return RedirectToAction("HesapGoster");
            }

            // Hesap bilgilerini güncelle
            mevcutHesap.HesapAd = hesap.HesapAd;
            mevcutHesap.HesapSoyad = hesap.HesapSoyad;
            mevcutHesap.HesapEmail = hesap.HesapEmail;
            mevcutHesap.HesapSifre = hesap.HesapSifre;

            k.SaveChanges();
            TempData["msj"] = $"{hesap.HesapAd} {hesap.HesapSoyad} adlı hesabın bilgileri başarıyla güncellendi.";
            return RedirectToAction("HesapGoster");
        }


        public IActionResult HesabinRandevusu()
        {
            // Oturumdan HesapID'yi alıyoruz
            var hesapId = HttpContext.Session.GetInt32("HesapID");

            // Eğer kullanıcı giriş yapmamışsa, hata mesajı ile giriş sayfasına yönlendiriyoruz
            if (hesapId == null)
            {
                TempData["msj"] = "Lütfen önce giriş yapınız.";
                return RedirectToAction("Index1");
            }

            // Kullanıcıya ait randevuları getiriyoruz
            var randevular = k.Randevular
                .Where(r => r.HesapID == hesapId.Value)
                .Include(r => r.Calisan)  // Randevuyla ilişkili çalışan bilgisini de dahil ediyoruz
                .ToList();

            // Eğer randevu yoksa, kullanıcıya bir mesaj gösteriyoruz
            if (randevular.Count == 0)
            {
                TempData["msj"] = "Bu kullanıcıya ait herhangi bir randevu bulunmamaktadır.";
            }

            return View(randevular);  // Randevuları view'e gönderiyoruz
        }



        public IActionResult HesapEkle()
        {
            //form
            return View();
        }
        public IActionResult CalisanEkle()
        {
            //form
            return View();
        }
        public IActionResult SadeceLogin()
        {
            //form
            return View();
        }

        public IActionResult HesapSil(int id)
        {
            var hesap = k.Hesaplar.FirstOrDefault(h => h.HesapID == id);
            if (hesap == null)
            {
                TempData["msj"] = "Silmek istediğiniz hesap bulunamadı.";
                return RedirectToAction("HesapGoster");
            }

            // Hesaba ait randevuları kontrol et
            var randevuVarMi = k.Randevular.Any(r => r.HesapID == id);
            if (randevuVarMi)
            {
                TempData["msj"] = "Hesabı silmeden önce hesaba ait randevular silinmelidir.";
                return RedirectToAction("HesapGoster");
            }

            k.Hesaplar.Remove(hesap);
            k.SaveChanges();

            TempData["msj"] = $"{hesap.HesapAd} {hesap.HesapSoyad} adlı hesap başarıyla silindi.";
            return RedirectToAction("HesapGoster");
        }


        public IActionResult HesapKaydet(Hesap y)
        {
            if (ModelState.IsValid)
            {
                // Aynı e-posta adresi ile bir hesap olup olmadığını kontrol et
                var existingAccount = k.Hesaplar.FirstOrDefault(h => h.HesapEmail == y.HesapEmail);

                if (existingAccount != null)
                {
                    TempData["msj"] = "Bu e-posta adresi zaten kullanılıyor. Lütfen farklı bir e-posta adresi deneyin.";
                    return RedirectToAction("HesapEkle");
                }

                // Yeni hesap ekle
                k.Hesaplar.Add(y);
                k.SaveChanges();

                TempData["msj"] = y.HesapAd + " adlı hesap başarıyla eklendi.";
                return RedirectToAction("HesapGoster");
            }

            TempData["msj"] = "Lütfen gerekli alanları düzgün şekilde doldurunuz.";
            return RedirectToAction("HesapEkle");
        }

        public IActionResult CalisanKaydet(Calisan y)
        {
            if (ModelState.IsValid)
            {
                m.Calisanlar.Add(y);
                //  k.Add(y);
                m.SaveChanges();
                TempData["msj"] = y.CalisanAd + " adı calisan eklendi";
                return RedirectToAction("CalisanGoster");
            }
            TempData["msj"] = "Lütfen Dataları düzgün giriniz";
            return RedirectToAction("CalisanEkle");
        }




        public IActionResult CalisanSil(int id)
        {
            var calisan = m.Calisanlar.Include(c => c.Randevular).FirstOrDefault(c => c.CalisanID == id);
            if (calisan == null)
            {
                TempData["msj"] = "Silmek istediğiniz çalışan bulunamadı.";
                return RedirectToAction("CalisanGoster");
            }

            // Çalışanın randevularını kontrol et
            if (calisan.Randevular != null && calisan.Randevular.Any())
            {
                TempData["msj"] = "Bu çalışana ait randevular bulunmaktadır. Önce randevuları silmelisiniz.";
                return RedirectToAction("CalisanGoster");
            }

            // Çalışanı sil
            m.Calisanlar.Remove(calisan);
            m.SaveChanges();

            TempData["msj"] = $"{calisan.CalisanAd} {calisan.CalisanSoyad} adlı çalışan başarıyla silindi.";
            return RedirectToAction("CalisanGoster");
        }


        public IActionResult CalisanDuzenle(int id)
        {
            var calisan = m.Calisanlar.FirstOrDefault(c => c.CalisanID == id);
            if (calisan == null)
            {
                TempData["msj"] = "Düzenlemek istediğiniz çalışan bulunamadı.";
                return RedirectToAction("CalisanGoster");
            }

            return View(calisan);
        }
        [HttpPost]
        public IActionResult CalisanDuzenle(Calisan calisan)
        {
            if (!ModelState.IsValid)
            {
                TempData["msj"] = "Lütfen tüm alanları doğru şekilde doldurun.";
                return View(calisan);
            }

            var mevcutCalisan = m.Calisanlar.FirstOrDefault(c => c.CalisanID == calisan.CalisanID);
            if (mevcutCalisan == null)
            {
                TempData["msj"] = "Düzenlemek istediğiniz çalışan bulunamadı.";
                return RedirectToAction("CalisanGoster");
            }

            // Çalışan bilgilerini güncelle
            mevcutCalisan.CalisanAd = calisan.CalisanAd;
            mevcutCalisan.CalisanSoyad = calisan.CalisanSoyad;
            mevcutCalisan.Telefon = calisan.Telefon;
            mevcutCalisan.alan = calisan.alan;

            m.SaveChanges();
            TempData["msj"] = $"{calisan.CalisanAd} {calisan.CalisanSoyad} adlı çalışanın bilgileri başarıyla güncellendi.";
            return RedirectToAction("CalisanGoster");
        }





        public IActionResult RandevuKaydet(Hesap y)
        {
            if (ModelState.IsValid)
            {
                k.Hesaplar.Add(y);
                //  k.Add(y);
                k.SaveChanges();
                TempData["msj"] = y.HesapAd + " adı hesap eklendi";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Lütfen Dataları düzgün giriniz";
            return RedirectToAction("HesapEkle");
        }





        static List<Hesap> Users = new List<Hesap>()
        {
            new Hesap{ HesapAd="can",HesapSifre="123456789",HesapSoyad="Green",HesapEmail="ff@gmail.com"},
            new Hesap{ HesapAd="ayse",HesapSifre="456",HesapSoyad="Greefn",HesapEmail="f@gmail.com"},
        };

        public IActionResult Index1()
        {
            if (HttpContext.Session.GetString("SesUsr") is null)
            {
                return View();
            }
            return View("AnaSayfa");
        }
        public IActionResult LogOut()
        {
            if (HttpContext.Session.GetString("SesUsr") is not null)
            {
                TempData["msj"] = "Güvenli bir şekilde çıkış yaptınız";
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index1");


        }
        public IActionResult UsrLogin(Hesap u)
        {
            if (u == null || string.IsNullOrEmpty(u.HesapEmail) || string.IsNullOrEmpty(u.HesapSifre))
            {
                TempData["msj"] = "Lütfen e-posta adresi ve şifre alanlarını doldurun.";
                return RedirectToAction("Index1");
            }

            // Veritabanında Email ve HesapSifre eşleşmesini kontrol et
            var user = k.Hesaplar.FirstOrDefault(h => h.HesapEmail == u.HesapEmail && h.HesapSifre == u.HesapSifre);

            if (u.HesapEmail == "g221210081@sakarya.edu.tr" && u.HesapSifre == "123456789")
            {
                HttpContext.Session.SetString("SesUsr", "admin");
                return RedirectToAction("RandevuGoster");
            }
            else if (user != null)
            {
                // Login başarılı
                HttpContext.Session.SetString("SesUsr", user.HesapEmail);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddSeconds(100),
                };

                TempData["msj"] = "Başarıyla giriş yaptınız.";
                HttpContext.Session.SetInt32("HesapID", user.HesapID); // HesapID'yi oturuma kaydet
                return RedirectToAction("RandevuEkle");
            }

            TempData["msj"] = "E-posta adresi veya şifre hatalı.";
            return RedirectToAction("Index1");
        }


        public IActionResult UsrIcerik()
        {
            if (HttpContext.Session.GetString("SesUsr") is null)
            {
                TempData["msj"] = "Lütfen önce Login olun";
                return RedirectToAction("Index1");
            }
            return View();
        }

        public IActionResult UsrSesSet()
        {
            Hesap usr = new Hesap();
            usr.HesapAd = "Hasan";
            usr.HesapSifre = "ztaa";

            string usrJson = JsonConvert.SerializeObject(usr);

            HttpContext.Session.SetString("SesObj", usrJson);

            return RedirectToAction("UsrIcerik");
        }

        public IActionResult UsrSesGet()
        {
            if (HttpContext.Session.GetString("SesObj") is not null)
            {
                var stJson = HttpContext.Session.GetString("SesObj");
                Hesap u = JsonConvert.DeserializeObject<Hesap>(stJson);
                return View(u);
            }
            TempData["msj"] = "Once Obje Session oluştur";
            return RedirectToAction("Index1");
        }





        public IActionResult RandevuEkle()
        {

            var hesapId = HttpContext.Session.GetInt32("HesapID"); // Oturumdan HesapID'yi al

            if (hesapId == null)
            {
                TempData["msj"] = "Lütfen giriş yapınız.";
                return RedirectToAction("Login");
            }
            ViewBag.Calisanlar = k.Calisanlar.ToList(); // Çalışanları ViewBag ile gönder



            // Çalışan listesini ViewBag'e ekliyoruz
            var calisanlar = m.Calisanlar.Select(c => new
            {
                c.CalisanID,
                c.CalisanAd,
                c.alan // Çalışanın alan bilgisi
            }).ToList();

            ViewBag.Calisanlar = calisanlar;

            

            var model = new Randevu
            {
                HesapID = hesapId.Value // Modelde HesapID'yi ayarla
            };
            return View(model);
        }

        public IActionResult RandevuGoster()
        {
            var randevular = k.Randevular
                .Include(r => r.Calisan) // Calisan bilgilerini de ilişkiyle beraber çekiyoruz
                .ToList();
            return View(randevular);
        }

       






        public IActionResult RandevuSil(int id)
        {
            var randevu = k.Randevular.FirstOrDefault(r => r.RandevuID == id);
            if (randevu == null)
            {
                TempData["msj"] = "Silmek istediğiniz randevu bulunamadı.";
                return RedirectToAction("RandevuGoster");
            }

            k.Randevular.Remove(randevu);
            k.SaveChanges();

            TempData["msj"] = $"Randevu başarıyla silindi.";
            return RedirectToAction("RandevuGoster");
        }


        public IActionResult RandevuSilk(int id)
        {
            var hesapId = HttpContext.Session.GetInt32("HesapID");

            if (hesapId == null)
            {
                TempData["msj"] = "Lütfen önce giriş yapınız.";
                return RedirectToAction("Index1");
            }

            // Randevuyu alıyoruz
            var randevu = k.Randevular.FirstOrDefault(r => r.RandevuID == id && r.HesapID == hesapId.Value);

            if (randevu == null)
            {
                TempData["msj"] = "Randevu bulunamadı veya bu randevuya erişim izniniz yok.";
                return RedirectToAction("HesabinRandevusu");
            }

            k.Randevular.Remove(randevu);
            k.SaveChanges();

            TempData["msj"] = "Randevunuz başarıyla silindi.";
            return RedirectToAction("HesabinRandevusu");
        }






        [HttpPost]
        public IActionResult RandevuKaydet2(Randevu r, string SacIslemleri)
        {





            // İşlem süreleri
            var islemSureleri = new Dictionary<string, int>
    {
        { "Kesim", 1 },
        { "Boya", 3 },
        { "Fon", 1 },
        { "Perma", 2 },
        { "Brezilya Fönü", 1 },
        { "Saç Bakımı", 2 },
        { "Saç Düzleştirme", 1 }
    };

            // Seçilen işlem
            var islem = SacIslemleri.Split('-')[0].Trim();
            if (islemSureleri.ContainsKey(islem))
            {
                r.IslemSuresi = islemSureleri[islem];
                r.RandevuBitisSaati = r.RandevuSaat + r.IslemSuresi; // int değer olarak hesapla
            }
            else
            {
                TempData["msj"] = "Geçersiz işlem seçildi.";
                return RedirectToAction("RandevuEkle");
            }

            // Çakışma kontrolü
            bool randevuVarMi = k.Randevular.Any(randevu =>
                randevu.RandevuGun == r.RandevuGun &&
                randevu.CalisanID == r.CalisanID &&
                (
                    (randevu.RandevuSaat <= r.RandevuSaat && randevu.RandevuBitisSaati > r.RandevuSaat) ||
                    (randevu.RandevuSaat < r.RandevuBitisSaati && randevu.RandevuBitisSaati >= r.RandevuBitisSaati)
                )
            );

            if (randevuVarMi)
            {
                TempData["msj"] = "Seçilen gün ve saat için bu çalışanla bir randevu zaten mevcut.";
                return RedirectToAction("RandevuEkle");
            }






            if (string.IsNullOrEmpty(SacIslemleri))
            {
                TempData["msj"] = "Lütfen bir saç işlemi seçiniz.";
                return RedirectToAction("RandevuEkle");
            }

           

            // Çalışan ID kontrolü
            if (r.CalisanID == 0)
            {
                TempData["msj"] = "Lütfen bir çalışan seçiniz.";
                return RedirectToAction("RandevuEkle");
            }
            // Seçilen saç işlemini ata
            r.SacIslemleri = SacIslemleri;

            // Toplam tutar hesapla
            switch (SacIslemleri)
            {
                case "Kesim - 50":
                    r.ToplamTutar = 50;
                    break;
                case "Boya - 150":
                    r.ToplamTutar = 150;
                    break;
                case "Fon - 30":
                    r.ToplamTutar = 30;
                    break;
                case "Perma - 200":
                    r.ToplamTutar = 200;
                    break;
                case "Brezilya Fönü - 250":
                    r.ToplamTutar = 250;
                    break;
                case "Saç Bakımı - 120":
                    r.ToplamTutar = 120;
                    break;
                case "Saç Düzleştirme - 180":
                    r.ToplamTutar = 180;
                    break;
                default:
                    r.ToplamTutar = 0;
                    break;
            }


            var calisan = m.Calisanlar.FirstOrDefault(c => c.CalisanID == r.CalisanID);
            if (calisan != null && !calisan.alan.Contains(r.SacIslemleri.Split(" - ")[0]))
            {
                TempData["msj"] = "Seçilen işlem, çalışanın alanı ile uyumlu değil.";
                return RedirectToAction("RandevuEkle");
            }

            // Yeni randevuyu kaydet
            k.Randevular.Add(r);
            k.SaveChanges();

            TempData["msj"] = "Randevunuz başarıyla alındı!";
            return RedirectToAction("HesabinRandevusu");


            
        }








    }
}

