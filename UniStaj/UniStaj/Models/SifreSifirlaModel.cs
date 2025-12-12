using Microsoft.EntityFrameworkCore;
using UniStaj.GenelIslemler;
using UniStaj.veri;

namespace UniStaj.Models
{
    public class sifreSifirlaModel
    {
        public KullaniciAYRINTI? kisi { get; set; }
        public async Task kullaniciCekKos(string id)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kisi = await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kodu == id);
            }
        }


        public async Task sifirlaKos()
        {
            using (veri.Varlik vari = new Varlik())
            {
                Kullanici? kim = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kisi.kullaniciKimlik);
                if (kim == null)
                    throw new Exception("Kullanıc bulunamadı");


                kim.kodu = Guid.NewGuid().ToString();
                kim.e_sifreDegisecekmi = true;
                kim.telefon = kisi.telefon;
                kim.sifre = GuvenlikIslemi.sifrele(Guid.NewGuid().ToString().Substring(0, 5));
                await veriTabani.KullaniciCizelgesi.kaydetKos(kim, vari, false);
                kisi = await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciKimlik == kim.kullaniciKimlik);
                await GenelIslemler.SMSIslemi.sifreAnimsatma(vari, kisi, kim.telefon);

            }
        }

    }
}
