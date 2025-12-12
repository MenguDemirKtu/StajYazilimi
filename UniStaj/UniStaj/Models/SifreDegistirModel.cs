using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.Models
{
    public class SifreDegistirModel : ModelTabani
    {
        public KullaniciAYRINTI? kullaniciBilgisi { get; set; }
        public SifreDegisikligi kartVerisi { get; set; }


        public SifreDegistirModel()
        {
            kartVerisi = new SifreDegisikligi();
            kullaniciBilgisi = new KullaniciAYRINTI();
        }
        public async Task veriCekKos(Yonetici kim)
        {
            kullanan = kim;
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullaniciBilgisi = await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciKimlik == kim.kullaniciKimlik);
                kartVerisi = new SifreDegisikligi();
                kartVerisi.i_kullaniciKimlik = kim.kullaniciKimlik;
                kartVerisi.tarih = DateTime.Now;
            }

        }

        // 2Ab5345435345345!
        public async Task kontrolEtKos(Yonetici kim)
        {
            using (veri.Varlik vari = new Varlik())
            {
                Kullanici? kime = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kim.kullaniciKimlik);
                if (kime != null)
                    if (kime.sifre != GenelIslemler.GuvenlikIslemi.sifrele(this.kartVerisi.eskiSifre))
                        throw new Exception("Eski şifreniz kayıtlarla eşleşmiyor");

                if (kartVerisi.yeniSifre != kartVerisi.yeniSifreTekrar)
                    throw new Exception("Girdiğiniz şifreler birbiriyle eşleşmiyor");
            }

        }
        public async Task kaydetKos(Yonetici kim)
        {
            kullanan = kim;
            using (veri.Varlik vari = new Varlik())
            {
                Kullanici? kime = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kim.kullaniciKimlik);
                if (kime != null)
                {
                    kartVerisi.tarih = DateTime.Now;
                    kartVerisi.sifreDegisikligiKimlik = 0;
                    kartVerisi.yeniSifre = GenelIslemler.GuvenlikIslemi.sifrele(kartVerisi.yeniSifre);
                    kartVerisi.eskiSifre = GenelIslemler.GuvenlikIslemi.sifrele(kartVerisi.eskiSifre);
                    kime.sifre = kartVerisi.yeniSifre;
                    kime.e_sifreDegisecekmi = false;
                    kime.sonSifreDegistirmeTarihi = DateTime.Now;
                    await kartVerisi.kaydetKos(vari, false);
                    await veriTabani.KullaniciCizelgesi.kaydetKos(kime, vari, false);
                }
            }
        }
    }
}
