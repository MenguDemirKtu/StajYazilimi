using Microsoft.AspNetCore.Mvc;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class YuklenenDosyalarController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "YuklenenDosyalar");
            gorunumAyari("", "", "Ana Sayfa", "/", "/YuklenenDosyalar/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos() == true)
            {
                Models.YuklenenDosyalarModel modeli = new Models.YuklenenDosyalarModel();
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "YuklenenDosyalar");
            gorunumAyari("Yüklenen Dosyalar Kartı", "Yüklenen Dosyalar Kartı", "Ana Sayfa", "/", "/YuklenenDosyalar/", tanitim);
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Gorme;
            if (id > 0)
                yetkiTuru = enumref_YetkiTuru.Ekleme;
            else
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await yetkiVarmiKos("YuklenenDosyalar", yetkiTuru))
            {
                Models.YuklenenDosyalarModel modeli = new Models.YuklenenDosyalarModel();
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
                    uyariVer("Hiç kayıt seçilmemiş");
                List<string> kayitlar = id.Split(',').ToList();
                if (await yetkiVarmiKos("YuklenenDosyalar", enumref_YetkiTuru.Silme))
                {
                    for (int i = 0; i < kayitlar.Count; i++)
                    {
                        YuklenenDosyalar silinecek = YuklenenDosyalar.olustur(kayitlar[i]);
                        silinecek._sayfaAta(this);
                        silinecek.sil();
                    }
                    Models.YuklenenDosyalarModel modeli = new Models.YuklenenDosyalarModel();
                    modeli.veriCekKosut(mevcutKullanici());
                    return silindiBildirimi();
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
        public async Task<ActionResult> Kaydet(Models.YuklenenDosyalarModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                var kaydedilecek = gelen.kartVerisi;
                enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
                if (kaydedilecek._birincilAnahtar() > 0)
                    yetkiTuru = enumref_YetkiTuru.Guncelleme;
                if (await yetkiVarmiKos(kaydedilecek, yetkiTuru) == false)
                    throw new Exception("Bu işlemi yapmaya yetkiniz yok");

                using (veri.Varlik vari = new Varlik())
                {
                    kaydedilecek._kontrolEt(dilKimlik, vari);
                    kaydedilecek._sayfaAta(this);
                    kaydedilecek.kaydet(true);
                }
                return kaydedildiBildirimi(kaydedilecek);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
