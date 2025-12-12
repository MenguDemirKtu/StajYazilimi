using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class DuyuruCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Duyuru> ara(params Predicate<Duyuru>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.Duyurus.ToList().FindAll(kosul).OrderByDescending(p => p.duyurukimlik).ToList();
            }
        }


        public static List<Duyuru> tamami(Varlik kime)
        {
            return kime.Duyurus.Where(p => p.varmi == true).OrderByDescending(p => p.duyurukimlik).ToList();
        }


        public static List<Duyuru> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<Duyuru> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static Duyuru tekliCek(Int32 kimlik, Varlik kime)
        {
            Duyuru kayit = kime.Duyurus.FirstOrDefault(p => p.duyurukimlik == kimlik);
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
        public static void kaydet(Duyuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.duyurukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Duyurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Duyurus.FirstOrDefault(p => p.duyurukimlik == yeni.duyurukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Duyuru kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Duyurus.FirstOrDefault(p => p.duyurukimlik == kimi.duyurukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


