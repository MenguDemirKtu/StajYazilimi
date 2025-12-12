
using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class KullaniciController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.KullaniciModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/Kullanici?id=" + nedir.kodu);
        }
        public async Task<IActionResult> sifirla(sifreSifirlaModel modeli)
        {
            try
            {
                await modeli.sifirlaKos();
                return basariBildirimi("Şifre sıfırlama SMS'i başarıyla gönderildi");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }
        public async Task<IActionResult> sifreSifirla(string id)
        {
            try
            {

                string tanitim = await Genel.dokumKisaAciklamaKos(this, "Kullanici");
                gorunumAyari("", "", "Ana Sayfa", "/", "/Kullanici/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    sifreSifirlaModel modeli = new sifreSifirlaModel();
                    await modeli.kullaniciCekKos(id);
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
        public async Task<ActionResult> Index(string id)
        {
            try
            {
                string tanitim = await Genel.dokumKisaAciklamaKos(this, "Kullanici");
                gorunumAyari("", "", "Ana Sayfa", "/", "/Kullanici/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.KullaniciModel modeli = new Models.KullaniciModel();
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
                string tanitim = await Genel.dokumKisaAciklamaKos(this, "Kullanici");
                gorunumAyari("Kullanıcı Kartı", "Kullanıcı Kartı", "Ana Sayfa", "/", "/Kullanici/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("Kullanici", yetkiTuru))
                {
                    Models.KullaniciModel modeli = new Models.KullaniciModel();
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
                    Models.KullaniciModel modeli = new Models.KullaniciModel();
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
        public async Task<ActionResult> Kaydet(Models.KullaniciModel gelen)
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
