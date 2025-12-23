using Microsoft.AspNetCore.Mvc;
namespace UniStaj.Controllers
{
    public class StajBirimAsamasiController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.StajBirimAsamasiModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/StajBirimAsamasi?id=" + nedir.kodu);
        }
        public async Task<ActionResult> Index(string id)
        {
            try
            {
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBirimAsamasi");
                gorunumAyari("", "", "Ana Sayfa", "/", "/StajBirimAsamasi/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.StajBirimAsamasiModel modeli = new Models.StajBirimAsamasiModel();
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
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBirimAsamasi");
                gorunumAyari("Staj Birim Aşaması Kartı", "Staj Birim Aşaması Kartı", "Ana Sayfa", "/", "/StajBirimAsamasi/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("StajBirimAsamasi", yetkiTuru))
                {
                    Models.StajBirimAsamasiModel modeli = new Models.StajBirimAsamasiModel();
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
                    Models.StajBirimAsamasiModel modeli = new Models.StajBirimAsamasiModel();
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
        public async Task<ActionResult> Kaydet(Models.StajBirimAsamasiModel gelen)
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

        [HttpPost]
        public async Task<ActionResult> View(string id)
        {
            try
            {

                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBirimAsamasi");
                gorunumAyari("", "", "Ana Sayfa", "/", "/StajBirimAsamasiDüzenleme/", tanitim);

                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.StajBirimAsamasiModel modeli = new Models.StajBirimAsamasiModel();
                    await modeli.birimKodunaGoreCek(mevcutKullanici(), id);
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
