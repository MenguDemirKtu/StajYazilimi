using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_CinsiyetCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_Cinsiyet> ara(params Predicate<ref_Cinsiyet>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ref_Cinsiyets.ToList().FindAll(kosul).OrderByDescending(p => p.CinsiyetKimlik).ToList();
            }
        }


        public static List<ref_Cinsiyet> tamami(Varlik kime)
        {
            return kime.ref_Cinsiyets.ToList();
        }


        public static List<ref_Cinsiyet> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<ref_Cinsiyet> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static ref_Cinsiyet tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_Cinsiyet kayit = kime.ref_Cinsiyets.FirstOrDefault(p => p.CinsiyetKimlik == kimlik);
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(ref_Cinsiyet yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.CinsiyetKimlik <= 0)
            {
                kime.ref_Cinsiyets.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_Cinsiyets.FirstOrDefault(p => p.CinsiyetKimlik == yeni.CinsiyetKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_Cinsiyet kimi, Varlik kime)
        {

        }
    }
}


