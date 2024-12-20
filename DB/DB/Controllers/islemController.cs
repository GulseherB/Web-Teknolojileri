using DB.Models;
using Microsoft.AspNetCore.Mvc;
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

       



    }
}
