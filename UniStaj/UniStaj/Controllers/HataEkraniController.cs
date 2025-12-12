using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class HataEkraniController : Sayfa
    {
        public async Task<IActionResult> Index(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Hata Ekranı");
            gorunumAyari("", "", "Hata Ekranı", "/", "/HataEkrani/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            HataEkraniModel modeli = new HataEkraniModel();
            try
            {
                using (veri.Varlik vari = new Varlik())
                {
                    var uyan = vari.SistemHatasis.FirstOrDefault(p => p.kodu == id);
                    if (uyan != null)
                        modeli.aciklama = uyan.tanitim;
                }
            }
            catch
            {

            }
            return View(modeli);

        }
    }
}
