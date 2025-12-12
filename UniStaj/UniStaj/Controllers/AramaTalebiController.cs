using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class AramaTalebiController : Sayfa
    {
        public ActionResult Cek(Models.AramaTalebiModel modeli)
        {
            var nedir = modeli.ayrintiliAra(this);
            return basariBildirimi("/AramaTalebi?id=" + nedir.kodu);
        }
        public async Task<IActionResult> Index(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "AramaTalebi");
            gorunumAyari("", "", "Ana Sayfa", "/", "/AramaTalebi/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos() == true)
            {
                Models.AramaTalebiModel modeli = new Models.AramaTalebiModel();
                if (string.IsNullOrEmpty(id))
                    modeli.veriCekKosut(mevcutKullanici());
                else
                    modeli.kosulaGoreCek(mevcutKullanici(), id);
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "AramaTalebi");
            gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/AramaTalebi/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("AramaTalebi", yetkiTuru))
            {
                Models.AramaTalebiModel modeli = new Models.AramaTalebiModel();
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
                    Models.AramaTalebiModel modeli = new Models.AramaTalebiModel();
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
        public async Task<ActionResult> Kaydet(Models.AramaTalebiModel gelen)
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
