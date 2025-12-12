using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class YazilimAyariController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.YazilimAyariModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/YazilimAyari?id=" + nedir.kodu);
        }
        public async Task<ActionResult> Index(string id)
        {
            try
            {
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "YazilimAyari");
                gorunumAyari("", "", "Ana Sayfa", "/", "/YazilimAyari/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.YazilimAyariModel modeli = new Models.YazilimAyariModel();
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
            catch (Exception ex)
            {
                return await HataSayfasiKosut(ex);
            }
        }
        public async Task<ActionResult> Kart(long id)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                string tanitim = "....";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "YazilimAyari");
                gorunumAyari("Yazılım Ayarı Kartı", "Yazılım Ayarı Kartı", "Ana Sayfa", "/", "/YazilimAyari/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("YazilimAyari", yetkiTuru))
                {
                    Models.YazilimAyariModel modeli = new Models.YazilimAyariModel();
                    await modeli.veriCekKos(mevcutKullanici(), id);
                    return View(modeli);
                }
                else
                {
                    return YetkiYok();
                }
            }
            catch (Exception ex)
            {
                return await HataSayfasiKosut(ex);
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
                    Models.YazilimAyariModel modeli = new Models.YazilimAyariModel();
                    await modeli.silKos(this, id ?? "", mevcutKullanici());
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
        public async Task<ActionResult> Kaydet(Models.YazilimAyariModel gelen)
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
