using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_KullaniciTuruCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_KullaniciTuru> ara(params Predicate<ref_KullaniciTuru>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ref_KullaniciTurus.ToList().FindAll(kosul).OrderByDescending(p => p.kullaniciTuruKimlik).ToList();
            }
        }


        public static List<ref_KullaniciTuru> tamami(Varlik kime)
        {
            return kime.ref_KullaniciTurus.ToList();
        }


        public static List<ref_KullaniciTuru> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<ref_KullaniciTuru> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static ref_KullaniciTuru tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_KullaniciTuru kayit = kime.ref_KullaniciTurus.FirstOrDefault(p => p.kullaniciTuruKimlik == kimlik);
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(ref_KullaniciTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.kullaniciTuruKimlik <= 0)
            {
                kime.ref_KullaniciTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_KullaniciTurus.FirstOrDefault(p => p.kullaniciTuruKimlik == yeni.kullaniciTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_KullaniciTuru kimi, Varlik kime)
        {

        }
    }
}


