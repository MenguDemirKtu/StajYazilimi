using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class BelgeTuruCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<BelgeTuru> ara(params Predicate<BelgeTuru>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.BelgeTurus.ToList().FindAll(kosul).OrderByDescending(p => p.belgeTurukimlik).ToList();
            }
        }


        public static List<BelgeTuru> tamami(Varlik kime)
        {
            return kime.BelgeTurus.Where(p => p.varmi == true).OrderByDescending(p => p.belgeTurukimlik).ToList();
        }


        public static List<BelgeTuru> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<BelgeTuru> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static BelgeTuru tekliCek(Int32 kimlik, Varlik kime)
        {
            BelgeTuru kayit = kime.BelgeTurus.FirstOrDefault(p => p.belgeTurukimlik == kimlik);
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
        public static void kaydet(BelgeTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.belgeTurukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.BelgeTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.BelgeTurus.FirstOrDefault(p => p.belgeTurukimlik == yeni.belgeTurukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(BelgeTuru kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.BelgeTurus.FirstOrDefault(p => p.belgeTurukimlik == kimi.belgeTurukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


