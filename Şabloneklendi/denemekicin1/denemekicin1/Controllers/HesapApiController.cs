using denemekicin1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace denemekicin1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HesapApiController : ControllerBase
    {
        hesaprandevuContext k = new hesaprandevuContext();
        // GET: api/<HesapApiController>
        [HttpGet]
        public List<Hesap> Get()
        {    var hesaplar=k.Hesaplar.ToList();
           // var yazarlar2=k.Yazarlar.Include(x=>x.Kitaplar).ToList();
           //Yukardaki yazarlar2 yi göndermek için ayar yapmak lazım
           //aşağdaki yazarlar3 için yeni bir model oluşturup o modeli göndermek lazım
            var hesaplar3 = (from y in k.Hesaplar
                           join kt in k.Randevular
                          on y.HesapID equals kt.HesapID
                          select new
                          {
                              RandevuAd=kt.RandevuGun,
                              HesapAd=y.HesapAd
                          }).ToList();

            var hesaplar4 = k.Hesaplar
               .Include(y => y.Randevular)
               .Select(y => new Hesap
               {
                   HesapID = y.HesapID,
                   HesapAd = y.HesapAd,
                   Randevular = y.Randevular.Select(k => new Randevu
                   {
                       RandevuID = k.RandevuID,
                       RandevuGun = k.RandevuGun
                   }).ToList()
               }).ToList();


            //return yazarlar;
            return hesaplar4;
        }

        // GET api/<HesapApiController>/5
        [HttpGet("{id}")]
        public ActionResult<Hesap> Get(int id)
        {
            var hesap = k.Hesaplar.FirstOrDefault(x => x.HesapID == id);
            if (hesap is null)
            {
                return NoContent();
            }
            return hesap;
        }




        // POST api/<YazarApiController>
        [HttpPost]
        public ActionResult Post([FromBody] Hesap y)
        {
            k.Hesaplar.Add(y);
            k.SaveChanges();
            return Ok(y.HesapAd + " adlı hesap eklendi");

        }

        // PUT api/<YazarApiController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hesap y)
        {
            var hesap = k.Hesaplar.FirstOrDefault(x => x.HesapID == id);
            if (hesap is null)
            {
                return NotFound();
            }
            hesap.HesapAd = y.HesapAd;
            hesap.HesapSoyad = y.HesapSoyad;
            hesap.HesapEmail = y.HesapEmail;
            hesap.HesapSifre = y.HesapSifre;


            k.Hesaplar.Update(hesap);
            k.SaveChanges();
            return Ok(y.HesapAd + " adlı hesap güncellendi");
        }

        public ActionResult Delete(int id)
        {
            var yazar = k.Hesaplar.Include(x => x.Randevular).FirstOrDefault(x => x.HesapID == id);
            if (yazar is null)
            {
                return NotFound();
            }
            if (yazar.Randevular.Count > 0)
            {
                return NotFound();
            }
            k.Hesaplar.Remove(yazar);
            k.SaveChanges();
            return Ok();
        }
    }
}
