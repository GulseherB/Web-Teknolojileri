using denemekicin1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace denemekicin1.Controllers
{
    public class HesapConsumeApiController : Controller
    {
        hesaprandevuContext k = new hesaprandevuContext();
        public async Task<IActionResult> Index()
        {
            List<Hesap> hesaplar = new List<Hesap>();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://localhost:7287/api/HesapApi/");
            var jsondata = await response.Content.ReadAsStringAsync();
            hesaplar = JsonConvert.DeserializeObject<List<Hesap>>(jsondata);
            return View(hesaplar);
        }

        public ActionResult Index2(int id)
        {
            var hesaplar = k.Hesaplar.ToList();
            return View(hesaplar);
        }
    }
}
