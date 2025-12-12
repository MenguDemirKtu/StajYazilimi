using Microsoft.AspNetCore.Mvc;
namespace UniStaj.Controllers
{
    public class StajBirimYetkilisiBirimiController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.StajBirimYetkilisiBirimiModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/StajBirimYetkilisiBirimi?id=" + nedir.kodu);
        }
        public async Task<ActionResult> Index(string id)
        {
            try
            {
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBirimYetkilisiBirimi");
                gorunumAyari("", "", "Ana Sayfa", "/", "/StajBirimYetkilisiBirimi/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.StajBirimYetkilisiBirimiModel modeli = new Models.StajBirimYetkilisiBirimiModel();
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
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBirimYetkilisiBirimi");
                gorunumAyari("Staj Birim Yetkilisi Birimi Kartı", "Staj Birim Yetkilisi Birimi Kartı", "Ana Sayfa", "/", "/StajBirimYetkilisiBirimi/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("StajBirimYetkilisiBirimi", yetkiTuru))
                {
                    Models.StajBirimYetkilisiBirimiModel modeli = new Models.StajBirimYetkilisiBirimiModel();
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
                    Models.StajBirimYetkilisiBirimiModel modeli = new Models.StajBirimYetkilisiBirimiModel();
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
        public async Task<ActionResult> Kaydet(Models.StajBirimYetkilisiBirimiModel gelen)
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
