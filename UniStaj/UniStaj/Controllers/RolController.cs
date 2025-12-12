using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class RolController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.RolModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/Rol?id=" + nedir.kodu);
        }
        public async Task<ActionResult> Index(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Rol");
            gorunumAyari("", "", "Ana Sayfa", "/", "/Rol/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.RolModel modeli = new Models.RolModel();
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Rol");
            gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/Rol/", tanitim);
            enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
            if (await yetkiVarmiKos("Rol", yetkiTuru))
            {
                Models.RolModel modeli = new Models.RolModel();
                await modeli.veriCekKos(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Sil(string? id)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (id == null)
                    uyariVer(Ikazlar.hicKayitSecilmemis(dilKimlik));
                if (await yetkiVarmiKos("Ogrenci", enumref_YetkiTuru.Silme))
                {
                    Models.RolModel modeli = new Models.RolModel();
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
        public async Task<ActionResult> Kaydet(Models.RolModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.yetkiKontrolu(this);
                await gelen.kaydetKos(this);
                return basariBildirimi(gelen.kartVerisi, dilKimlik, "/RolWebSayfasiIzni/Kart?id=" + gelen.kartVerisi.kodu);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
