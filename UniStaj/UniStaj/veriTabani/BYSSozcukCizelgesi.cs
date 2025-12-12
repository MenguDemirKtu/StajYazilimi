using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class BYSSozcukArama
    {
        public Int32? bysSozcukKimlik { get; set; }
        public string sozcuk { get; set; }
        public string parametreler { get; set; }
        public bool? varmi { get; set; }
        public BYSSozcukArama()
        {
            varmi = true;
        }
        public Predicate<BYSSozcuk> kosulu()
        {
            List<Predicate<BYSSozcuk>> kosullar = new List<Predicate<BYSSozcuk>>();
            kosullar.Add(c => c.varmi == true);
            if (bysSozcukKimlik != null)
                kosullar.Add(c => c.bysSozcukKimlik == bysSozcukKimlik);
            if (sozcuk != null)
                kosullar.Add(c => c.sozcuk == sozcuk);
            if (parametreler != null)
                kosullar.Add(c => c.parametreler == parametreler);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<BYSSozcuk> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class BYSSozcukCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<BYSSozcuk> ara(BYSSozcukArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.BYSSozcuks.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.bysSozcukKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<BYSSozcuk> ara(params Predicate<BYSSozcuk>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.BYSSozcuks.ToList().FindAll(kosul).OrderByDescending(p => p.bysSozcukKimlik).ToList(); }
        }
        public static List<BYSSozcuk> tamami(Varlik kime)
        {
            return kime.BYSSozcuks.Where(p => p.varmi == true).OrderByDescending(p => p.bysSozcukKimlik).ToList();
        }
        public static List<BYSSozcuk> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<BYSSozcuk> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static BYSSozcuk tekliCek(Int32 kimlik, Varlik kime)
        {
            BYSSozcuk kayit = kime.BYSSozcuks.FirstOrDefault(p => p.bysSozcukKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(BYSSozcuk yeni, ref Varlik kime)
        {
            if (yeni.bysSozcukKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.BYSSozcuks.Add(yeni);
            }
            else
            {
                var bulunan = kime.BYSSozcuks.FirstOrDefault(p => p.bysSozcukKimlik == yeni.bysSozcukKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(BYSSozcuk yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.BYSSozcuks.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(BYSSozcuk yeni, Varlik kime)
        {
            var bulunan = kime.BYSSozcuks.FirstOrDefault(p => p.bysSozcukKimlik == yeni.bysSozcukKimlik);
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
        public static void kaydet(BYSSozcuk yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.bysSozcukKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.BYSSozcuks.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.BYSSozcuks.FirstOrDefault(p => p.bysSozcukKimlik == yeni.bysSozcukKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(BYSSozcuk kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.BYSSozcuks.FirstOrDefault(p => p.bysSozcukKimlik == kimi.bysSozcukKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
