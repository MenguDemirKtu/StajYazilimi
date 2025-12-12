using Microsoft.AspNetCore.Mvc;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class SistemHatasiController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "SistemHatasi");
            gorunumAyari("", "", "Ana Sayfa", "/", "/SistemHatasi/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.SistemHatasiModel modeli = new Models.SistemHatasiModel();
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "SistemHatasi");
            gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/SistemHatasi/", tanitim);
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Gorme;
            if (id > 0)
                yetkiTuru = enumref_YetkiTuru.Ekleme;
            else
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await yetkiVarmiKos("SistemHatasi", yetkiTuru))
            {
                Models.SistemHatasiModel modeli = new Models.SistemHatasiModel();
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
                if (id == null)
                    throw new ArgumentException("Yanlış tanım");
                List<string> kayitlar = id.Split(',').ToList();
                if (await yetkiVarmiKos("SistemHatasi", enumref_YetkiTuru.Silme))
                {
                    for (int i = 0; i < kayitlar.Count; i++)
                    {
                        SistemHatasi silinecek = SistemHatasi.olustur(kayitlar[i]);
                        silinecek._sayfaAta(this);
                        silinecek.sil();
                    }
                    Models.SistemHatasiModel modeli = new Models.SistemHatasiModel();
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
        public async Task<ActionResult> Kaydet(Models.SistemHatasiModel gelen)
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
                    if (kaydedilecek == null)
                        throw new Exception("Kaydedilecek eleman bulunamadı");
                    kaydedilecek.aciklama = Sayfa.dosyaKonumDuzelt(kaydedilecek.aciklama ?? "", Genel.yazilimAyari);
                    kaydedilecek.tanitim = Sayfa.dosyaKonumDuzelt(kaydedilecek.tanitim ?? "", Genel.yazilimAyari);
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
