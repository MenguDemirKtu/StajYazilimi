using UniStaj.veri;

namespace UniStaj
{
    public class Ikazlar
    {
        public static string hicKayitSecilmemis(int dil)
        {
            return Ayar.sozcuk("Hiç kayıt seçilmemiş.", dil);
        }
        public static string degeriSecilmelidir(int dil)
        {
            return Ayar.sozcuk("{0} değeri seçilmelidir", dil);
        }
        public static string basariylaSilindi(int dil)
        {
            return Ayar.sozcuk("Seçtiğiniz kayıtlar başarıyla silindi", dil);
        }
        public static string islemiYapmayaYetkinizYok(int dil)
        {
            return Ayar.sozcuk("İşlemi yapmak için yetkiniz yok.", dil);
        }

        public static string yeniKayitEkle(string bilesenAdi, int dil)
        {
            string ifade = Ayar.sozcuk("Yeni {0} ekle", dil);


            return String.Format(ifade, Ayar.sozcuk(bilesenAdi, dil));
        }

        private static string adliBilesenBasariylaKaydedildi(int dil)
        {
            return Ayar.sozcuk("{0} adlı {1} başarıyla kaydedildi", dil);
        }

        /// <summary>
        /// Bileşenin başarıyla kaydedildiği
        /// </summary>
        /// <param name="kimi"></param>
        /// <param name="dil"></param>
        /// <returns></returns>
        public static string bilesenKaydedildi(Bilesen kimi, int dil)
        {
            return "Başarıyla kaydedildi";
        }
    }
}
