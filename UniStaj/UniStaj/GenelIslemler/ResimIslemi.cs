using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using UniStaj.veri;

namespace UniStaj.GenelIslemler
{
    public class ResimIslemi
    {
        public static void boyutlandir(string inputPath, string outStream, int width, int height, int kalite)
        {
            using (Image image = Image.Load(inputPath))
            {
                image.Mutate(x => x.Resize(width, height, KnownResamplers.Lanczos3));
                image.Save(outStream);
                var webPEncoder = new SixLabors.ImageSharp.Formats.Webp.WebpEncoder()
                {
                    Quality = kalite
                };

                image.Save(outStream, webPEncoder);

            }
        }
        public static void mansetKaydet(veri.Varlik vari, Fotograf siradaki, string _environmentWebRootPath, string cizelgeAdi, long ilgiliKimlik, ResimAyariAYRINTI ayar)
        {
            if (siradaki.e_boyutlandirildimi == true)
                return;

            string fileName = siradaki.konum;
            fileName = fileName.Replace("GeciciDosyalar\\", "").Trim();
            fileName = fileName.Replace("GeciciDosyalar", "").Trim();
            fileName = fileName.Replace(".png", ".webp");
            fileName = fileName.Replace(".jpg", ".webp");
            fileName = fileName.Replace(".jpeg", ".webp");
            fileName = fileName.Replace(".JPEG", ".webp");
            fileName = fileName.Replace(".PNG", ".webp");
            fileName = fileName.Replace(".JPG", ".webp");
            fileName = fileName.Replace(".JPG", ".webp");

            var eskiKonum = Path.Combine(_environmentWebRootPath, siradaki.konum);
            string yeni = Path.Combine(_environmentWebRootPath, ayar.dizinAdi, fileName);

            string yeniOrta = Path.Combine(_environmentWebRootPath, ayar.dizinAdi + "\\Orta", fileName);
            string yeniKucuk = Path.Combine(_environmentWebRootPath, ayar.dizinAdi + "\\Kucuk", fileName);
            string yeniEnKucuk = Path.Combine(_environmentWebRootPath, ayar.dizinAdi + "\\EnKucuk", fileName);

            siradaki.ekAciklama = eskiKonum + " DOSYA YOK";

            eskiKonum = eskiKonum.Replace("\\", "/");
            FileInfo dosya = new FileInfo(eskiKonum);
            if (dosya.Exists)
            {
                ResimIslemi.boyutlandir(eskiKonum, yeni, ayar.genislik.Value, ayar.yukseklik.Value, ayar.kalite.Value);
                if (ayar.e_genislik2Varmi == true)
                    ResimIslemi.boyutlandir(eskiKonum, yeniOrta, ayar.genislik2.Value, ayar.yukseklik2.Value, ayar.kalite.Value);
                if (ayar.e_genislik3Varmi == true)
                    ResimIslemi.boyutlandir(eskiKonum, yeniKucuk, ayar.genislik3.Value, ayar.yukseklik3.Value, ayar.kalite.Value);
                if (ayar.e_genislik4Varmi == true)
                    ResimIslemi.boyutlandir(eskiKonum, yeniEnKucuk, ayar.genislik4.Value, ayar.yukseklik4.Value, ayar.kalite.Value);


                siradaki.konum = ayar.dizinAdi + "\\" + fileName;
                siradaki.e_boyutlandirildimi = true;
                siradaki.genislik = ayar.genislik.Value;
                siradaki.yukseklik = ayar.yukseklik.Value; ;
                siradaki.ilgiliCizelge = cizelgeAdi;
                siradaki.ilgiliKimlik = ilgiliKimlik;
                siradaki.ekAciklama = "Dosya bulundu";
                dosya.Delete();
                siradaki.ekAciklama = "Dosya bulundu ve silindi";
            }
            siradaki.ekAciklama += "düzenlendi";
            siradaki.duzenlemeTarihi = DateTime.Now;
            veriTabani.FotografCizelgesi.kaydet(siradaki, vari, false);

            //string sorgu = "update Fotograf set konum = REPLACE(konum, 'MansetFotosu\\MansetFotosu\\', 'MansetFotosu\\') where ilgiliCizelge ='Haber' and ilgiliKimlik = " + haberi.haberKimlik.ToString();
            //SqlIslemi.islemYap(vari, sorgu);

        }

    }
}
