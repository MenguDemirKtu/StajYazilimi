using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class KareKodController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Bursiyer");
            gorunumAyari("", "", "Ana Sayfa", "/", "/Bursiyer/", tanitim);
            _sayfaTuru = enum_sayfaTuru.rapor;
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                try
                {
                    KareKodModel model = new KareKodModel();
                    model.veriCekKosut(mevcutKullanici());
                    return View(model);
                }
                catch (Exception ex)
                {
                    return await HataSayfasiKosut(ex);
                }
            }
            else
            {
                return YetkiYok();
            }

        }
    }
}
