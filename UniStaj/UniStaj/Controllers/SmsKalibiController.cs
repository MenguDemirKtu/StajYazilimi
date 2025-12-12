using Microsoft.AspNetCore.Mvc;

namespace UniStaj.Controllers
{
    public class SmsKalibiController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.SmsKalibiModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/SmsKalibi?id=" + nedir.kodu);
        }
        public async Task<ActionResult> Index(string id)
        {
            try
            {

                string tanitim = await Genel.dokumKisaAciklamaKos(this, "SmsKalibi");
                gorunumAyari("", "", "Ana Sayfa", "/", "/SmsKalibi/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.SmsKalibiModel modeli = new Models.SmsKalibiModel();
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
                string tanitim = await Genel.dokumKisaAciklamaKos(this, "SmsKalibi");
                gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/SmsKalibi/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("SmsKalibi", yetkiTuru))
                {
                    Models.SmsKalibiModel modeli = new Models.SmsKalibiModel();
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
                    Models.SmsKalibiModel modeli = new Models.SmsKalibiModel();
                    if (string.IsNullOrEmpty(id))
                        throw new Exception("Yanlış parametre");
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
        public async Task<ActionResult> Kaydet(Models.SmsKalibiModel gelen)
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
