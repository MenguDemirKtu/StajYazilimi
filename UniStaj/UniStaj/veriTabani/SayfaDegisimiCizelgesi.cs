using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SayfaDegisimiArama
    {
        public Int32? sayfaDegisimiKimlik { get; set; }
        public string ad { get; set; }
        public bool? varmi { get; set; }
        public SayfaDegisimiArama()
        {
            ad = "";
            varmi = true;
        }
        public Predicate<SayfaDegisimi> kosulu()
        {
            List<Predicate<SayfaDegisimi>> kosullar = new List<Predicate<SayfaDegisimi>>();
            kosullar.Add(c => c.varmi == true);
            if (sayfaDegisimiKimlik != null)
                kosullar.Add(c => c.sayfaDegisimiKimlik == sayfaDegisimiKimlik);
            if (ad != null)
                kosullar.Add(c => c.ad == ad);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<SayfaDegisimi> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class SayfaDegisimiCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<SayfaDegisimi> ara(SayfaDegisimiArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.SayfaDegisimis.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.sayfaDegisimiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SayfaDegisimi> ara(params Predicate<SayfaDegisimi>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.SayfaDegisimis.ToList().FindAll(kosul).OrderByDescending(p => p.sayfaDegisimiKimlik).ToList(); }
        }
        public static List<SayfaDegisimi> tamami(Varlik kime)
        {
            return kime.SayfaDegisimis.Where(p => p.varmi == true).OrderByDescending(p => p.sayfaDegisimiKimlik).ToList();
        }
        public static List<SayfaDegisimi> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<SayfaDegisimi> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static SayfaDegisimi? tekliCek(Int32 kimlik, Varlik kime)
        {
            SayfaDegisimi? kayit = null;
            kayit = kime.SayfaDegisimis.FirstOrDefault(p => p.sayfaDegisimiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(SayfaDegisimi yeni, ref Varlik kime)
        {
            if (yeni.sayfaDegisimiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SayfaDegisimis.Add(yeni);
            }
            else
            {
                var bulunan = kime.SayfaDegisimis.FirstOrDefault(p => p.sayfaDegisimiKimlik == yeni.sayfaDegisimiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(SayfaDegisimi yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.SayfaDegisimis.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(SayfaDegisimi yeni, Varlik kime)
        {
            var bulunan = kime.SayfaDegisimis.FirstOrDefault(p => p.sayfaDegisimiKimlik == yeni.sayfaDegisimiKimlik);
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
        public static void kaydet(SayfaDegisimi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sayfaDegisimiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SayfaDegisimis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.SayfaDegisimis.FirstOrDefault(p => p.sayfaDegisimiKimlik == yeni.sayfaDegisimiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(SayfaDegisimi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SayfaDegisimis.FirstOrDefault(p => p.sayfaDegisimiKimlik == kimi.sayfaDegisimiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
