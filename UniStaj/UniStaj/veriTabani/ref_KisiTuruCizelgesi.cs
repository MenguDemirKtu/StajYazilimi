using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_KisiTuruCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_KisiTuru> ara(params Predicate<ref_KisiTuru>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ref_KisiTurus.ToList().FindAll(kosul).OrderByDescending(p => p.KisiTuruKimlik).ToList();
            }
        }


        public static List<ref_KisiTuru> tamami(Varlik kime)
        {
            return kime.ref_KisiTurus.ToList();
        }


        public static List<ref_KisiTuru> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<ref_KisiTuru> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static ref_KisiTuru tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_KisiTuru kayit = kime.ref_KisiTurus.FirstOrDefault(p => p.KisiTuruKimlik == kimlik);
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(ref_KisiTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.KisiTuruKimlik <= 0)
            {
                kime.ref_KisiTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_KisiTurus.FirstOrDefault(p => p.KisiTuruKimlik == yeni.KisiTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_KisiTuru kimi, Varlik kime)
        {

        }
    }
}


