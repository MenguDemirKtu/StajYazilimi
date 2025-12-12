using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class TopluEPostaAlicisiController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.TopluEPostaAlicisiModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/TopluEPostaAlicisi?id=" + nedir.kodu);
        }
        public async Task<ActionResult> Index(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "TopluEPostaAlicisi");
            gorunumAyari("", "", "Ana Sayfa", "/", "/TopluEPostaAlicisi/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.TopluEPostaAlicisiModel modeli = new Models.TopluEPostaAlicisiModel();
                if (string.IsNullOrEmpty(id))
                    await modeli.veriCekKos(mevcutKullanici());
                else
                    await modeli.kosulaGoreCek(mevcutKullanici(), id);
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "TopluEPostaAlicisi");
            gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/TopluEPostaAlicisi/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("TopluEPostaAlicisi", yetkiTuru))
            {
                Models.TopluEPostaAlicisiModel modeli = new Models.TopluEPostaAlicisiModel();
                await modeli.veriCekKos(mevcutKullanici(), id);
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
                    Models.TopluEPostaAlicisiModel modeli = new Models.TopluEPostaAlicisiModel();
                    await modeli.silKos(this, id, mevcutKullanici());
                    await modeli.veriCekKos(mevcutKullanici());
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
        public async Task<ActionResult> Kaydet(Models.TopluEPostaAlicisiModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.yetkiKontrolu(this);
                await gelen.kaydetKos(this);
                return basariBildirimi(gelen.kartVerisi, dilKimlik);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
