using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GunlukGuvenlikAnahtariCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<GunlukGuvenlikAnahtari> ara(params Predicate<GunlukGuvenlikAnahtari>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.GunlukGuvenlikAnahtaris.ToList().FindAll(kosul).OrderByDescending(p => p.gunlukGuvenlikAnahtariKimlik).ToList();
            }
        }


        public static List<GunlukGuvenlikAnahtari> tamami(Varlik kime)
        {
            return kime.GunlukGuvenlikAnahtaris.Where(p => p.varmi == true).OrderByDescending(p => p.gunlukGuvenlikAnahtariKimlik).ToList();
        }


        public static List<GunlukGuvenlikAnahtari> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<GunlukGuvenlikAnahtari> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static GunlukGuvenlikAnahtari tekliCek(Int32 kimlik, Varlik kime)
        {
            GunlukGuvenlikAnahtari kayit = kime.GunlukGuvenlikAnahtaris.FirstOrDefault(p => p.gunlukGuvenlikAnahtariKimlik == kimlik);
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
        public static void kaydet(GunlukGuvenlikAnahtari yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.gunlukGuvenlikAnahtariKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.GunlukGuvenlikAnahtaris.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.GunlukGuvenlikAnahtaris.FirstOrDefault(p => p.gunlukGuvenlikAnahtariKimlik == yeni.gunlukGuvenlikAnahtariKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(GunlukGuvenlikAnahtari kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.GunlukGuvenlikAnahtaris.FirstOrDefault(p => p.gunlukGuvenlikAnahtariKimlik == kimi.gunlukGuvenlikAnahtariKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


