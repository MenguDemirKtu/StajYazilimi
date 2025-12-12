using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class BelgeTuruController : Sayfa
    {
        public async Task<IActionResult> Index()
        {
            string tanitim = await Genel.dokumKisaAciklamaKos(this, "BelgeTuru");
            gorunumAyari("", "", "Ana Sayfa", "/", "/BelgeTuru/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.BelgeTuruModel modeli = new Models.BelgeTuruModel();
                modeli.veriCekKosut(mevcutKullanici());
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }
        public async Task<ActionResult> Kart(long id)
        {
            if (!oturumAcildimi())
                return OturumAcilmadi();

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "BelgeTuru");
            gorunumAyari("Belge Türü Kartı", "Belge Türü Kartı", "Ana Sayfa", "/", "/BelgeTuru/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("BelgeTuru", yetkiTuru))
            {
                Models.BelgeTuruModel modeli = new Models.BelgeTuruModel();
                modeli.veriCek(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Sil(string id)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (id == null)
                    uyariVer(Ikazlar.hicKayitSecilmemis(dilKimlik));
                if (await yetkiVarmiKos("Ogrenci", enumref_YetkiTuru.Silme))
                {
                    Models.BelgeTuruModel modeli = new Models.BelgeTuruModel();
                    if (string.IsNullOrEmpty(id))
                        throw new Exception("Yanlış parametre");
                    modeli.sil(this, id, mevcutKullanici());
                    modeli.veriCekKosut(mevcutKullanici());
                    return basariBildirimi(Ikazlar.basariylaSilindi(dilKimlik));
                }
                else
                {
                    return yetkiYokBildirimi();
                }
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.BelgeTuruModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.yetkiKontrolu(this);
                gelen.kaydet(this);
                return basariBildirimi(gelen.kartVerisi, dilKimlik);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
