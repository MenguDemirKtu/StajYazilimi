using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class AnaSayfaController : Sayfa
    {
        public async Task<IActionResult> Index(string id)
        {
            if (!oturumAcildimi())
                return OturumAcilmadi();
            //string tanitim = await Genel.dokumKisaAciklamaKos(this, "Ana Sayfa");
            ViewBag.mevcut = Genel.mevcutKullanici(this);
            ViewBag.dil = mevcutKullanici().dilKimlik;
            sayfaTuru = enum_sayfaTuru.anaSayfa;
            AnaSayfaModel modeli = new AnaSayfaModel();
            await modeli.veriCekKosut(mevcutKullanici());
            return View(modeli);
        }
    }
}
