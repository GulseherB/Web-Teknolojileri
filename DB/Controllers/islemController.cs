using DB.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;


namespace DB.Controllers
{
    public class islemController : Controller
    {
        hesaprandevuContext k = new hesaprandevuContext();
        public IActionResult Index()
        {
            var hesaplar = k.Hesaplar.ToList();
            return View(hesaplar);
        }

       


        public IActionResult HesapEkle()
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
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Lütfen Dataları düzgün giriniz";
            return RedirectToAction("HesapEkle");
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

            if (user != null)
            {
                // Login başarılı
                HttpContext.Session.SetString("SesUsr", user.HesapAd);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddSeconds(100),
                };
               

                TempData["msj"] = "Başarıyla giriş yaptınız.";
                return RedirectToAction("SadeceLogin");
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
            return View();
        }

        public IActionResult RandevuGoster()
        {
            var randevular = k.Randevular.ToList();
            return View(randevular);
        }



        [HttpPost]
        public IActionResult RandevuKaydet2(Randevu r, string[] SacIslemleri)
        {
            if (ModelState.IsValid)
            {
                // Aynı gün ve saat kontrolü
                bool mevcutRandevu = k.Randevular.Any(x => x.RandevuGun == r.RandevuGun && x.RandevuSaat == r.RandevuSaat);

                if (mevcutRandevu)
                {
                    TempData["msj"] = "Bu tarih ve saate randevu alınmış. Lütfen başka bir saat seçin.";
                    return RedirectToAction("RandevuEkle");
                }

                // SacIslemleri'ni birleştir
                r.SacIslemleri = string.Join(", ", SacIslemleri);

                // Toplam tutar hesapla
                r.ToplamTutar = SacIslemleri.Sum(islem =>
                {
                    switch (islem)
                    {
                        case "Kesim": return 450;
                        case "Boya": return 750;
                        case "Fön": return 250;
                        case "Perma": return 300;
                        default: return 0;
                    }
                });

                // Yeni randevuyu kaydet
                k.Randevular.Add(r);
                k.SaveChanges();

                TempData["msj"] = "Randevunuz başarıyla alındı!";
                return RedirectToAction("RandevuGoster");
            }

            TempData["msj"] = "Lütfen bilgileri eksiksiz doldurun.";
            return RedirectToAction("RandevuEkle");
        }






    }
}

