using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class TopluSMSAlicisiController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "TopluSMSAlicisi");
            gorunumAyari("", "", "Ana Sayfa", "/", "/TopluSMSAlicisi/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.TopluSMSAlicisiModel modeli = new Models.TopluSMSAlicisiModel();
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "TopluSMSAlicisi");
            gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/TopluSMSAlicisi/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("TopluSMSAlicisi", yetkiTuru))
            {
                Models.TopluSMSAlicisiModel modeli = new Models.TopluSMSAlicisiModel();
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
                    Models.TopluSMSAlicisiModel modeli = new Models.TopluSMSAlicisiModel();
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
        public async Task<ActionResult> Kaydet(Models.TopluSMSAlicisiModel gelen)
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
