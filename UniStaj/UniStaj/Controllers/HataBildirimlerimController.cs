using Microsoft.AspNetCore.Mvc;
using UniStaj.Models;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class HataBildirimlerimController : Sayfa
    {
        public async Task<IActionResult> Index()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Bolge");
            gorunumAyari("", "", "Ana Sayfa", "/", "/Bolge/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();

            Models.HataBildirimlerimModel modeli = new Models.HataBildirimlerimModel();
            await modeli.veriCekKos(mevcutKullanici());
            return View(modeli);
        }

        public async Task<ActionResult> tumBildirimler()
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Bolge");
            gorunumAyari("", "", "Ana Sayfa", "/", "/Bolge/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();

            var tur = mevcutKullanici()._turu();

            if (tur == enumref_KullaniciTuru.Sistem_Yoneticisi || tur == enumref_KullaniciTuru.Yazilimci)
            {
                Models.HataBildirimlerimModel modeli = new Models.HataBildirimlerimModel();
                modeli.tumCozulmemisler(mevcutKullanici());
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArizaKapat(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Bolge");
            gorunumAyari("", "", "Ana Sayfa", "/", "/Bolge/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();

            HataBildirimlerimModel model = new HataBildirimlerimModel();
            model.veriCek(mevcutKullanici(), id);
            var bildirim = model.anaBildirim;

            if (bildirim != null)
            {
                bildirim.i_hataBildirimDurumuKimlik = (int)enumref_HataBildirimDurumu.Sorun_cozuldu;
                bildirim.oncelik = 0;
                bildirim.kaydet(true);
            }
            return basariBildirimi("Hata bildirimi başarıyla çözüldü olarak işaretlendi.");
        }

        public async Task<ActionResult> Yazisma(string id)
        {
            string tanitim = await Genel.dokumKisaAciklamaKos(this, "Bolge");
            gorunumAyari("", "", "Ana Sayfa", "/HataBildirimlerim/tumBildirimler/", "/Bolge/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();

            Models.HataBildirimlerimModel modeli = new Models.HataBildirimlerimModel();
            modeli.yazismaCek(mevcutKullanici(), id);
            return View(modeli);
        }

        private void federasyonaBilgiVer(HataBildirimiAYRINTI bildirim)
        {
            veri.Varlik vari = new Varlik();
            List<FederasyonBildirimGondermeAyariAYRINTI> gidecekler = FederasyonBildirimGondermeAyariAYRINTI.ara(p => p.i_federasyonBildirimTuruKimlik == (int)enumref_FederasyonBildirimTuru.Hata_Bildirimi && p.e_gecerlimi == true);
            EPostaIslemi.KullanciHataBildirimindeBulundu(gidecekler, bildirim);
        }

        private void sporcuyaBilgiVer(HataBildirimi ilkBildirim, HataYazismasi yazisma)
        {
            try
            {
                KullaniciBildirimi bildirim = new KullaniciBildirimi();
                bildirim.i_kullaniciKimlik = (int)(ilkBildirim.i_yoneticiKimlik ?? 0);
                bildirim.bildirimBasligi = "Hata Bildirimine Yanıt";
                bildirim.tarihi = DateTime.Now;
                bildirim.bildirimTanitimi = ilkBildirim.baslik + " başlıklı hata bildirimine yeni yanıt yazıldı.";
                bildirim.e_goruldumu = 0;
                bildirim.gorulmeTarihi = null;
                bildirim.varmi = true;
                bildirim.kodu = null;
                bildirim.url = "/HataBildirimlerim/Yazisma/?id=" + ilkBildirim.kodu;
                bildirim.kaydet(false, false);
            }
            catch
            {

            }

            try
            {
                Kullanici alici = Kullanici.olustur(ilkBildirim.i_yoneticiKimlik ?? -1);
                EPostaIslemi.kullancininHataBildirimineYanitVerildi(yazisma, ilkBildirim, alici);
            }
            catch
            {

            }
        }

        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.HataBildirimlerimModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                bool bildirimVerilecekmi = false;
                using (veri.Varlik vari = new Varlik())
                {
                    HataYazismasi kaydedilecek = new HataYazismasi();
                    kaydedilecek.metin = gelen.aciklama;
                    kaydedilecek.i_yoneticiKimlik = mevcutKullanici().kullaniciKimlik;
                    kaydedilecek.tarih = DateTime.Now;
                    kaydedilecek.i_hataBildirimiKimlik = gelen.i_hataBildirimiKimlik;
                    if (kaydedilecek.metin != null)
                        kaydedilecek.metin = Sayfa.dosyaKonumDuzelt(kaydedilecek.metin, Genel.yazilimAyari);
                    kaydedilecek._kullaniciAta(mevcutKullanici());
                    kaydedilecek._kontrolEt(dilKimlik, vari);

                    await vari.HataYazismasis.AddAsync(kaydedilecek);
                    await vari.SaveChangesAsync();





                    HataBildirimi bildirm = HataBildirimi.olustur(kaydedilecek.i_hataBildirimiKimlik);
                    bildirm.i_sonYazaKullaniciKimlik = kaydedilecek.i_yoneticiKimlik;

                    if (kaydedilecek.i_yoneticiKimlik == bildirm.i_yoneticiKimlik)
                    {
                        bildirm.oncelik = 8;
                        bildirm.i_hataBildirimDurumuKimlik = (int)enumref_HataBildirimDurumu.Kisi_yanit_verdi;
                        bildirimVerilecekmi = true;
                    }
                    else
                    {
                        bildirm.oncelik = 5;
                        bildirm.i_hataBildirimDurumuKimlik = (int)enumref_HataBildirimDurumu.Yetkili_yanit_verdi;
                    }

                    bildirm.kaydet(false, false);
                    if (bildirimVerilecekmi)
                    {
                        HataBildirimiAYRINTI bildirimAyrinti = new HataBildirimiAYRINTI();
                        bildirimAyrinti.hataAciklamasi = kaydedilecek.metin;
                        federasyonaBilgiVer(bildirimAyrinti);
                    }
                    else
                    {
                        sporcuyaBilgiVer(bildirm, kaydedilecek);
                    }
                    return kaydedildiBildirimi(kaydedilecek);
                }
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}