using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class hataArama
    {
        public Int32? hataKimlik { get; set; }
        public string ifadesi { get; set; }
        public DateTime? tarih { get; set; }
        public bool? varmi { get; set; }
        public hataArama()
        {
            varmi = true;
        }
        public Predicate<Hata> kosulu()
        {
            List<Predicate<Hata>> kosullar = new List<Predicate<Hata>>();
            kosullar.Add(c => c.varmi == true);
            if (hataKimlik != null)
                kosullar.Add(c => c.hataKimlik == hataKimlik);
            if (ifadesi != null)
                kosullar.Add(c => c.ifadesi == ifadesi);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<Hata> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class hataCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<Hata> ara(hataArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.hatas.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.hataKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Hata> ara(params Predicate<Hata>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.hatas.ToList().FindAll(kosul).OrderByDescending(p => p.hataKimlik).ToList(); }
        }
        public static List<Hata> tamami(Varlik kime)
        {
            return kime.hatas.Where(p => p.varmi == true).OrderByDescending(p => p.hataKimlik).ToList();
        }
        public static List<Hata> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<Hata> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static Hata tekliCek(Int32 kimlik, Varlik kime)
        {
            Hata kayit = kime.hatas.FirstOrDefault(p => p.hataKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(Hata yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.hatas.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(Hata yeni, Varlik kime)
        {
            var bulunan = kime.hatas.FirstOrDefault(p => p.hataKimlik == yeni.hataKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(Hata yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.hataKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.hatas.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.hatas.FirstOrDefault(p => p.hataKimlik == yeni.hataKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(Hata kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.hatas.FirstOrDefault(p => p.hataKimlik == kimi.hataKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
