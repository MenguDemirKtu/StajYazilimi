using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.Models
{
    public class KimlikDogrulaModel : ModelTabani

    {
        public KullaniciAYRINTI kisi { get; set; }
        public string kod { get; set; }
        public async Task veriCekKos(Yonetici kim)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                KullaniciAYRINTI? acan = await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciKimlik == kim.kullaniciKimlik);
                if (acan == null)
                {
                    throw new Exception("Uyun kullanıcı bulunamadı");
                }
                kisi = acan;
            }
        }


        public async Task kodDenetimi(Yonetici kim)
        {
            using (veri.Varlik vari = new Varlik())
            {
                KullaniciAYRINTI? acan = await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciKimlik == kim.kullaniciKimlik);
                if (acan == null)
                    throw new Exception("Yanlış kullanıcı bilgisi");
                if (kod != acan.ciftOnayKodu)
                    throw new Exception("Yanlış doğrulama kodu");

                Random rg = new Random();
                Kullanici? tekli = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kim.kullaniciKimlik);
                if (tekli != null)
                {
                    tekli.ciftOnayKodu = rg.Next(100000, 900000).ToString();
                    await veriTabani.KullaniciCizelgesi.kaydetKos(tekli, vari, false);
                }

            }
        }
        public KimlikDogrulaModel()
        {
            kisi = new KullaniciAYRINTI();
            kod = "";
        }
    }
}
