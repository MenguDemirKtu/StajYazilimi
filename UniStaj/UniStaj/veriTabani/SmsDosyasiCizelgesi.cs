using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SmsDosyasiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SmsDosyasi> ara(params Predicate<SmsDosyasi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SmsDosyasis.ToList().FindAll(kosul).OrderByDescending(p => p.smsDosyasiKimlik).ToList();
            }
        }


        public static List<SmsDosyasi> tamami(Varlik kime)
        {
            return kime.SmsDosyasis.Where(p => p.varmi == true).OrderByDescending(p => p.smsDosyasiKimlik).ToList();
        }


        public static List<SmsDosyasi> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<SmsDosyasi> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static SmsDosyasi tekliCek(Int32 kimlik, Varlik kime)
        {
            SmsDosyasi kayit = kime.SmsDosyasis.FirstOrDefault(p => p.smsDosyasiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(SmsDosyasi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.smsDosyasiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SmsDosyasis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.SmsDosyasis.FirstOrDefault(p => p.smsDosyasiKimlik == yeni.smsDosyasiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(SmsDosyasi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SmsDosyasis.FirstOrDefault(p => p.smsDosyasiKimlik == kimi.smsDosyasiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


