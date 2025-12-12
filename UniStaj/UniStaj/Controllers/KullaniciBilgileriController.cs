using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class KullaniciBilgileriController : Sayfa
    {
        public async Task<IActionResult> Index()
        {
            if (!oturumAcildimi())
                return OturumAcilmadi();

            UniStaj.Models.KullaniciModel model = new Models.KullaniciModel();
            using (veri.Varlik vari = new veri.Varlik())
            {
                sayfaTuru = enum_sayfaTuru.anaSayfa;
                string tanitim = await Genel.dokumKisaAciklamaKos(this, "Kullanıcı Bilgileri");
                ViewBag.mevcut = Genel.mevcutKullanici(this);
                sayfaTuru = enum_sayfaTuru.anaSayfa;
                await adresBelirleKos(vari, this, "Kullanıcı Bilgileri");
                int kk = mevcutKullanici().kullaniciKimlik;
                sayfaTuru = enum_sayfaTuru.rapor;
            }
            await model.veriCekKos(mevcutKullanici());
            return View(model);

        }
    }
}
