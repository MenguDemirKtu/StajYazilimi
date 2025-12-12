using Microsoft.AspNetCore.Mvc;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class HataBildirimiController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "HataBildirimi");
            gorunumAyari("", "", "Ana Sayfa", "/", "/HataBildirimi/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.HataBildirimiModel modeli = new Models.HataBildirimiModel();
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

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "HataBildirimi");
            gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/HataBildirimi/", tanitim);
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Gorme;
            if (id > 0)
                yetkiTuru = enumref_YetkiTuru.Ekleme;
            else
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await yetkiVarmiKos("HataBildirimi", yetkiTuru))
            {
                Models.HataBildirimiModel modeli = new Models.HataBildirimiModel();
                modeli.veriCek(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }

        public async Task<ActionResult> Ariza(string id)
        {
            if (!oturumAcildimi())
                return OturumAcilmadi();


            string[] alt = id.Split(',');
            int sayfaKimlik = Convert.ToInt32(alt[0]);
            WebSayfasi saytfa = WebSayfasi.olustur(sayfaKimlik);


            string tanitim = await Genel.dokumKisaAciklamaKos(this, "HataBildirimi");
            gorunumAyari("Kartı", "Kartı", "Ana Sayfa", "/", "/HataBildirimi/", tanitim);
            Models.HataBildirimiModel modeli = new Models.HataBildirimiModel();
            modeli.veriCek(mevcutKullanici(), 0);
            modeli.kartVerisi.hataAlinanSayfa = saytfa.sayfaBasligi + " / " + saytfa.hamAdresi;
            modeli.kartVerisi.e_goruldumu = 0;
            modeli.kartVerisi.i_yoneticiKimlik = mevcutKullanici().kullaniciKimlik;
            modeli.kartVerisi.tarih = DateTime.Now;
            return View(modeli);

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
                if (await yetkiVarmiKos("HataBildirimi", enumref_YetkiTuru.Silme))
                {
                    for (int i = 0; i < kayitlar.Count; i++)
                    {
                        HataBildirimi silinecek = HataBildirimi.olustur(kayitlar[i]);
                        silinecek.sil();
                    }
                    Models.HataBildirimiModel modeli = new Models.HataBildirimiModel();
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

        private void bildirimVer(HataBildirimi neyi)
        {
            HataBildirimiAYRINTI bildirim = HataBildirimiAYRINTI.olustur(neyi.hataBildirimiKimlik);
            veri.Varlik vari = new Varlik();
            List<FederasyonBildirimGondermeAyariAYRINTI> gidecekler = FederasyonBildirimGondermeAyariAYRINTI.ara(p => p.i_federasyonBildirimTuruKimlik == (int)enumref_FederasyonBildirimTuru.Hata_Bildirimi && p.e_gecerlimi == true);
            EPostaIslemi.KullanciHataBildirimindeBulundu(gidecekler, bildirim);

            //for (int i = 0; i < gidecekler.Count; i++)
            //{
            //    var siradaki = gidecekler[i];
            //    if (string.IsNullOrEmpty(siradaki.ePostaAdresi))
            //        continue;

            //}
        }

        private void bildirimGonder(HataBildirimi kaydedilecek)
        {

            Thread myNewThread = new Thread(() => Worker(kaydedilecek));
            myNewThread.Start();

        }
        private void Worker(HataBildirimi kaydedilecek)
        {
            try
            {
                bildirimVer(kaydedilecek);
                HataYazismasi yeni = new HataYazismasi();
                yeni.metin = kaydedilecek.hataAciklamasi;
                yeni.tarih = kaydedilecek.tarih;
                yeni.i_yoneticiKimlik = mevcutKullanici().kullaniciKimlik;
                yeni.varmi = true;
                yeni.i_hataBildirimiKimlik = kaydedilecek.hataBildirimiKimlik;
                yeni.kaydet(false, false);
            }
            catch
            {

            }

        }

        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.HataBildirimiModel gelen)
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
                kaydedilecek.oncelik = 10;

                using (veri.Varlik vari = new Varlik())
                {
                    kaydedilecek._kontrolEt(dilKimlik, vari);
                    kaydedilecek._sayfaAta(this);
                    kaydedilecek.kaydet(true);
                    if (yetkiTuru == enumref_YetkiTuru.Ekleme)
                        Worker(kaydedilecek);

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
