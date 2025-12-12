using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class KimlikDogrulaController : Sayfa
    {
        public async Task<IActionResult> Index()
        {
            if (oturumAcildimi() == false)
                return OturumAcilmadi();
            KimlikDogrulaModel modeli = new KimlikDogrulaModel();
            await modeli.veriCekKos(mevcutKullanici());
            return View(modeli);
        }

        [HttpPost]
        public async Task<IActionResult> Onayla(KimlikDogrulaModel gelen)
        {
            try
            {
                await gelen.kodDenetimi(mevcutKullanici());
                Yonetici simdiki = mevcutKullanici();
                simdiki.e_kodOnaylandimi = true;
                string jsonser = JsonConvert.SerializeObject(simdiki);
                HttpContext.Session.SetString("mevcutKullanici", jsonser);
                HttpContext.Session.SetInt32("kullaniciKimlik", simdiki.kullaniciKimlik);
                return basariBildirimi("Kodunuz başarıyla onaylandı", 1, "/anasayfa");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }
    }
}
