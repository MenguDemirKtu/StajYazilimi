using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class SifreDegistirController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "SifreDegistir");
            gorunumAyari("", "", "Ana Sayfa", "/", "/SifreDegistir/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            SifreDegistirModel modeli = new SifreDegistirModel();
            await modeli.veriCekKos(mevcutKullanici());
            return View(modeli);
        }

        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.SifreDegisikligiModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                SifreDegistirModel modeli = new SifreDegistirModel();
                modeli.kartVerisi = gelen.kartVerisi;
                string gerekce = "";
                bool son = GenelIslemler.GuvenlikIslemi.parolaGecerliMi(gelen.kartVerisi.yeniSifre, out gerekce);
                if (son == false)
                    throw new Exception(gerekce);
                await modeli.kontrolEtKos(mevcutKullanici());
                await modeli.kaydetKos(mevcutKullanici());
                return basariBildirimi("Şifreniz başarıyla değiştirildi. Lütfen yeni şifrenizle tekrar giriniz.");
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
