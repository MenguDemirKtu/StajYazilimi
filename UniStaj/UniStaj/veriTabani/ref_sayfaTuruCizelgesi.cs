using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_sayfaTuruArama
    {
        public Int32? sayfaTuruKimlik { get; set; }
        public string sayfaTuru { get; set; }
        public ref_sayfaTuruArama()
        {
        }
        public Predicate<ref_sayfaTuru> kosulu()
        {
            List<Predicate<ref_sayfaTuru>> kosullar = new List<Predicate<ref_sayfaTuru>>();
            if (sayfaTuruKimlik != null)
                kosullar.Add(c => c.sayfaTuruKimlik == sayfaTuruKimlik);
            if (sayfaTuru != null)
                kosullar.Add(c => c.sayfaTuru == sayfaTuru);
            Predicate<ref_sayfaTuru> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class ref_sayfaTuruCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<ref_sayfaTuru> ara(ref_sayfaTuruArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.ref_sayfaTurus.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.sayfaTuruKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_sayfaTuru> ara(params Predicate<ref_sayfaTuru>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.ref_sayfaTurus.ToList().FindAll(kosul).OrderByDescending(p => p.sayfaTuruKimlik).ToList(); }
        }
        public static List<ref_sayfaTuru> tamami(Varlik kime)
        {
            return kime.ref_sayfaTurus.ToList();
        }
        public static List<ref_sayfaTuru> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<ref_sayfaTuru> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static ref_sayfaTuru tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_sayfaTuru kayit = kime.ref_sayfaTurus.FirstOrDefault(p => p.sayfaTuruKimlik == kimlik);
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(ref_sayfaTuru yeni, ref Varlik kime)
        {
            if (yeni.sayfaTuruKimlik <= 0)
            {
                kime.ref_sayfaTurus.Add(yeni);
            }
            else
            {
                var bulunan = kime.ref_sayfaTurus.FirstOrDefault(p => p.sayfaTuruKimlik == yeni.sayfaTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(ref_sayfaTuru yeni, Varlik kime)
        {
            kime.ref_sayfaTurus.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(ref_sayfaTuru yeni, Varlik kime)
        {
            var bulunan = kime.ref_sayfaTurus.FirstOrDefault(p => p.sayfaTuruKimlik == yeni.sayfaTuruKimlik);
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
        public static void kaydet(ref_sayfaTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sayfaTuruKimlik <= 0)
            {
                kime.ref_sayfaTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.ref_sayfaTurus.FirstOrDefault(p => p.sayfaTuruKimlik == yeni.sayfaTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(ref_sayfaTuru kimi, Varlik kime)
        {
        }
    }
}
