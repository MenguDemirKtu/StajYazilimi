using Microsoft.AspNetCore.Mvc;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class BYSSozcukAciklamaController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "BYSSozcukAciklama");
            gorunumAyari("", "", "Ana Sayfa", "/", "/BYSSozcukAciklama/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.BYSSozcukAciklamaModel modeli = new Models.BYSSozcukAciklamaModel();
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "BYSSozcukAciklama");
            gorunumAyari("BYS Sözcük Açıklaması Kartı", "BYS Sözcük Açıklaması Kartı", "Ana Sayfa", "/", "/BYSSozcukAciklama/", tanitim);
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Gorme;
            if (id > 0)
                yetkiTuru = enumref_YetkiTuru.Ekleme;
            else
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await yetkiVarmiKos("BYSSozcukAciklama", yetkiTuru))
            {
                Models.BYSSozcukAciklamaModel modeli = new Models.BYSSozcukAciklamaModel();
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
                if (await yetkiVarmiKos("BYSSozcukAciklama", enumref_YetkiTuru.Silme))
                {
                    for (int i = 0; i < kayitlar.Count; i++)
                    {
                        BYSSozcukAciklama silinecek = BYSSozcukAciklama.olustur(kayitlar[i]);
                        silinecek._sayfaAta(this);
                        silinecek.sil();
                    }
                    Models.BYSSozcukAciklamaModel modeli = new Models.BYSSozcukAciklamaModel();
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
        public async Task<ActionResult> Kaydet(Models.BYSSozcukAciklamaModel gelen)
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
