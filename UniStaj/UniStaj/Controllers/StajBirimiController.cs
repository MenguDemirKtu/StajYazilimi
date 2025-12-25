using Microsoft.AspNetCore.Mvc;
namespace UniStaj.Controllers
{
    public class StajBirimiController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.StajBirimiModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/StajBirimi?id=" + nedir.kodu);
        }
        public async Task<ActionResult> Index(string id)
        {
            try
            {
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBirimi");
                gorunumAyari("", "", "Ana Sayfa", "/", "/StajBirimi/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.StajBirimiModel modeli = new Models.StajBirimiModel();
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
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBirimi");
                gorunumAyari("Staj Birimi Kartı", "Staj Birimi Kartı", "Ana Sayfa", "/", "/StajBirimi/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("StajBirimi", yetkiTuru))
                {
                    Models.StajBirimiModel modeli = new Models.StajBirimiModel();
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
                    Models.StajBirimiModel modeli = new Models.StajBirimiModel();
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
        public async Task<ActionResult> Kaydet(Models.StajBirimiModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                await gelen.yetkiKontrolu(this);
                await gelen.kaydetKos(this);
                return basariBildirimi(gelen.kartVerisi, dilKimlik, "/StajBirimi/StajTuruBelirle?kod=" + gelen.kartVerisi.kodu);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }


        [HttpPost]
        public async Task<ActionResult> turleriKaydet(Models.StajBirimiModel gelen)
        {
            try
            {

                if (!oturumAcildimi())
                    return OturumAcilmadi();

                await gelen.turleriKaydet();
                return basariBildirimi("Başarıyla kaydedildi");
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }

        public async Task<ActionResult> stajTuruBelirle(string kod)
        {
            try
            {
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBirimi");
                gorunumAyari("", "", "Ana Sayfa", "/", "/StajBirimi/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.StajBirimiModel modeli = new Models.StajBirimiModel();
                    await modeli.stajTurleriCek(mevcutKullanici(), kod);
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
    }
}
