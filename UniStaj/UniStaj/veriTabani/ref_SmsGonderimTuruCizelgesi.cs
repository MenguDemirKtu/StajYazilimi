using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_SmsGonderimTuruCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_SmsGonderimTuru> ara(params Predicate<ref_SmsGonderimTuru>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ref_SmsGonderimTurus.ToList().FindAll(kosul).OrderByDescending(p => p.SmsGonderimTuruKimlik).ToList();
            }
        }


        public static List<ref_SmsGonderimTuru> tamami(Varlik kime)
        {
            return kime.ref_SmsGonderimTurus.ToList();
        }


        public static List<ref_SmsGonderimTuru> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<ref_SmsGonderimTuru> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static ref_SmsGonderimTuru tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_SmsGonderimTuru kayit = kime.ref_SmsGonderimTurus.FirstOrDefault(p => p.SmsGonderimTuruKimlik == kimlik);
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(ref_SmsGonderimTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.SmsGonderimTuruKimlik <= 0)
            {
                kime.ref_SmsGonderimTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_SmsGonderimTurus.FirstOrDefault(p => p.SmsGonderimTuruKimlik == yeni.SmsGonderimTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_SmsGonderimTuru kimi, Varlik kime)
        {

        }
    }
}


