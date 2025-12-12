using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class DuyuruRolBagiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<DuyuruRolBagi> ara(params Predicate<DuyuruRolBagi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.DuyuruRolBagis.ToList().FindAll(kosul).OrderByDescending(p => p.duyuruRolBagiKimlik).ToList();
            }
        }


        public static List<DuyuruRolBagi> tamami(Varlik kime)
        {
            return kime.DuyuruRolBagis.Where(p => p.varmi == true).OrderByDescending(p => p.duyuruRolBagiKimlik).ToList();
        }


        public static List<DuyuruRolBagi> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<DuyuruRolBagi> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static DuyuruRolBagi tekliCek(Int32 kimlik, Varlik kime)
        {
            DuyuruRolBagi kayit = kime.DuyuruRolBagis.FirstOrDefault(p => p.duyuruRolBagiKimlik == kimlik);
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
        public static void kaydet(DuyuruRolBagi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.duyuruRolBagiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.DuyuruRolBagis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.DuyuruRolBagis.FirstOrDefault(p => p.duyuruRolBagiKimlik == yeni.duyuruRolBagiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(DuyuruRolBagi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.DuyuruRolBagis.FirstOrDefault(p => p.duyuruRolBagiKimlik == kimi.duyuruRolBagiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


