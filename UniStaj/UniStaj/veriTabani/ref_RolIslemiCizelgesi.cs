using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_RolIslemiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_RolIslemi> ara(params Predicate<ref_RolIslemi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ref_RolIslemis.ToList().FindAll(kosul).OrderByDescending(p => p.rolIslemiKimlik).ToList();
            }
        }


        public static List<ref_RolIslemi> tamami(Varlik kime)
        {
            return kime.ref_RolIslemis.ToList();
        }


        public static List<ref_RolIslemi> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<ref_RolIslemi> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static ref_RolIslemi tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_RolIslemi kayit = kime.ref_RolIslemis.FirstOrDefault(p => p.rolIslemiKimlik == kimlik);
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(ref_RolIslemi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.rolIslemiKimlik <= 0)
            {
                kime.ref_RolIslemis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_RolIslemis.FirstOrDefault(p => p.rolIslemiKimlik == yeni.rolIslemiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_RolIslemi kimi, Varlik kime)
        {

        }
    }
}


