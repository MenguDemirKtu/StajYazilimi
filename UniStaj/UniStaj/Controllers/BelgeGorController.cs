using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class BelgeGorController : Sayfa
    {
        public async Task<IActionResult> Index(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "BursKredi");
            gorunumAyari("", "", "Ana Sayfa", "/", "/BursKredi/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                BelgeGorModel model = new BelgeGorModel();
                model.veriCek(mevcutKullanici(), id);
                return View(model);
            }
            else
            {
                return YetkiYok();
            }

        }
    }
}
