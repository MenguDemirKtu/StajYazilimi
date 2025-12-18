using Microsoft.AspNetCore.Mvc;
using UniStaj.veriTabani;
namespace UniStaj.Controllers
{
    public class StajBasvurusuController : Sayfa
    {
        public async Task<ActionResult> Cek(Models.StajBasvurusuModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/StajBasvurusu?id=" + nedir.kodu);
        }
        public async Task<ActionResult> Index(string id)
        {
            try
            {
                string tanitim = "...";
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBasvurusu");
                gorunumAyari("", "", "Ana Sayfa", "/", "/StajBasvurusu/", tanitim);
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (await yetkiVarmiKos())
                {
                    Models.StajBasvurusuModel modeli = new Models.StajBasvurusuModel();
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
                return HataSayfasi(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> firmaBilgisiCek(string vergiNo)
        {
            try
            {
                using (var vari = new veri.Varlik())
                {

                    StajKurumuAYRINTIArama arama = new StajKurumuAYRINTIArama();
                    arama.vergiNo = vergiNo;
                    var sonuc = await arama.bul(vari);


                    if (sonuc == null)
                        return Json(new { success = false, message = "Firma bulunamadı. Lütfen bilgilerini ekleyiniz." });

                    return Json(new { success = true, bilgi = sonuc });
                }
            }
            catch (Exception ex)
            {
                return hataBildirimi(ex);
            }
        }


        [HttpGet]
        public async Task<JsonResult> stajyerStajTurleriniCek(int stajyerId)
        {
            try
            {
                using (var vari = new veri.Varlik())
                {
                    StajyerYukumlulukAYRINTIArama arama = new StajyerYukumlulukAYRINTIArama();
                    arama.i_stajyerKimlik = stajyerId;
                    var sonuc = await arama.cek(vari);
                    if (sonuc == null)
                        return Json(new { success = false, message = "Yapılması gereken staj bulunamadı." });
                    return Json(new { success = true, bilgi = sonuc });
                }
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
                tanitim = await Genel.dokumKisaAciklamaKos(this, "StajBasvurusu");
                gorunumAyari("Staj Başvurusu Kartı", "Staj Başvurusu Kartı", "Ana Sayfa", "/", "/StajBasvurusu/", tanitim);
                enumref_YetkiTuru yetkiTuru = yetkiTuruBelirle(id);
                if (await yetkiVarmiKos("StajBasvurusu", yetkiTuru))
                {
                    Models.StajBasvurusuModel modeli = new Models.StajBasvurusuModel();
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
                return HataSayfasi(ex);
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
                    Models.StajBasvurusuModel modeli = new Models.StajBasvurusuModel();
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
        public async Task<ActionResult> Kaydet(Models.StajBasvurusuModel gelen)
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
