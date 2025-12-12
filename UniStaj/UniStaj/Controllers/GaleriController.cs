using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;

namespace UniStaj.Controllers
{
    public class GaleriController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.GaleriModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/Galeri?id=" + nedir.kodu);
        }

        //  uniVtKullanici
        //  Ek9843Nbv!453zl

        public async Task<ActionResult> duzenle(string kod)
        {
            try
            {
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "GaleriDuzenle");
                gorunumAyari("", "", "Ana Sayfa", "/", "/GaleriDuzenle/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.GaleriModel modeli = new Models.GaleriModel();
                    await modeli.duzenle(mevcutKullanici(), kod);
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
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "Galeri");
                gorunumAyari("", "", "Ana Sayfa", "/", "/Galeri/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.GaleriModel modeli = new Models.GaleriModel();
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

        public async Task<ActionResult> fotoSil(long kimlik)
        {
            try
            {
                GaleriModel model = new GaleriModel();
                await model.fotoSilKos(this, kimlik);
                return basariBildirimi("Başarıyla silindi");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }
        public async Task<ActionResult> Kart(long id)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                string tanitim = "....";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "Galeri");
                gorunumAyari("Galeri Kartı", "Galeri Kartı", "Ana Sayfa", "/", "/Galeri/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("Galeri", yetkiTuru))
                {
                    Models.GaleriModel modeli = new Models.GaleriModel();
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
                    Models.GaleriModel modeli = new Models.GaleriModel();
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
        public async Task<ActionResult> SiraBelirle(Models.GaleriModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.yetkiKontrolu(this);
                await gelen.siraBelirleKos(this);
                return basariBildirimi(gelen.kartVerisi, dilKimlik);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.GaleriModel gelen)
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
