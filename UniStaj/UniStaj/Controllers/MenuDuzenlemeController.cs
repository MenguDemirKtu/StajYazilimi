using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class MenuDuzenlemeController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "MenuDuzenleme");
            gorunumAyari("", "", "Ana Sayfa", "/", "/MenuDuzenleme/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                MenuDuzenlemeModel model = new MenuDuzenlemeModel();
                model.veriCekKosut(mevcutKullanici());
                return View(model);
            }
            else
            {
                return YetkiYok();
            }


        }

        [HttpPost]
        public async Task<IActionResult> kaydet(MenuDuzenlemeModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.kaydet();
                return basariBildirimi("Başarıyla kaydedildi");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }
    }
}
