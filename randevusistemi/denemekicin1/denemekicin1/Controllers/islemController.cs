using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

using denemekicin1.Models;


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




        public IActionResult HesapKaydet(Hesap y)
        {
            if (ModelState.IsValid)
            {
                k.Hesaplar.Add(y);
                //  k.Add(y);
                k.SaveChanges();
                TempData["msj"] = y.HesapAd + " adı hesap eklendi";
                return RedirectToAction("HesapGoster");
            }
            TempData["msj"] = "Lütfen Dataları düzgün giriniz";
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
            if (u == null || string.IsNullOrEmpty(u.HesapAd) || string.IsNullOrEmpty(u.HesapSifre))
            {
                TempData["msj"] = "Lütfen kullanıcı adı ve şifre alanlarını doldurun.";
                return RedirectToAction("Index1");
            }


            // Veritabanında HesapAd ve HesapSifre eşleşmesini kontrol et
            var user = k.Hesaplar.FirstOrDefault(h => h.HesapAd == u.HesapAd && h.HesapSifre == u.HesapSifre);




            if (u.HesapAd == "admin" && u.HesapSifre == "123456789")
            {
                HttpContext.Session.SetString("SesUsr", "admin");
                return RedirectToAction("RandevuGoster");
            }


            else if (user != null)
            {
                // Login başarılı
                HttpContext.Session.SetString("SesUsr", user.HesapAd);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddSeconds(100),
                };


                TempData["msj"] = "Başarıyla giriş yaptınız.";
                HttpContext.Session.SetInt32("HesapID", user.HesapID); // HesapID'yi oturuma kaydet
                return RedirectToAction("RandevuEkle");
            }

            TempData["msj"] = "Kullanıcı Adı veya Şifre hatalı.";
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

            // Yeni randevuyu kaydet
            k.Randevular.Add(r);
            k.SaveChanges();

            TempData["msj"] = "Randevunuz başarıyla alındı!";
            return RedirectToAction("RandevuGoster");


            
        }








    }
}

