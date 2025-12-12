using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class DuyuruController : Sayfa
    {
        public async Task<ActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Duyuru");
            gorunumAyari("", "", "Ana Sayfa", "/", "/Duyuru/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.DuyuruModel modeli = new Models.DuyuruModel();
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
            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Duyuru");
            gorunumAyari("Duyuru Kartı", "Duyuru Kartı", "Ana Sayfa", "/", "/Duyuru/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("Duyuru", yetkiTuru))
            {
                Models.DuyuruModel modeli = new Models.DuyuruModel();
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
                    Models.DuyuruModel modeli = new Models.DuyuruModel();
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
        public async Task<ActionResult> Kaydet(Models.DuyuruModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.yetkiKontrolu(this);
                await gelen.kaydet(this);
                return basariBildirimi(gelen.kartVerisi, dilKimlik);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
