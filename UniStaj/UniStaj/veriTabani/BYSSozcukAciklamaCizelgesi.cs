using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class BYSSozcukAciklamaArama
    {
        public Int64? bysSozcukAciklamaKimlik { get; set; }
        public Int32? i_bysSozcukKimlik { get; set; }
        public Int32? i_dilKimlik { get; set; }
        public string ifade { get; set; }
        public bool? varmi { get; set; }
        public BYSSozcukAciklamaArama()
        {
            varmi = true;
        }
        public Predicate<BYSSozcukAciklama> kosulu()
        {
            List<Predicate<BYSSozcukAciklama>> kosullar = new List<Predicate<BYSSozcukAciklama>>();
            kosullar.Add(c => c.varmi == true);
            if (bysSozcukAciklamaKimlik != null)
                kosullar.Add(c => c.bysSozcukAciklamaKimlik == bysSozcukAciklamaKimlik);
            if (i_bysSozcukKimlik != null)
                kosullar.Add(c => c.i_bysSozcukKimlik == i_bysSozcukKimlik);
            if (i_dilKimlik != null)
                kosullar.Add(c => c.i_dilKimlik == i_dilKimlik);
            if (ifade != null)
                kosullar.Add(c => c.ifade == ifade);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<BYSSozcukAciklama> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class BYSSozcukAciklamaCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<BYSSozcukAciklama> ara(BYSSozcukAciklamaArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.BYSSozcukAciklamas.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.bysSozcukAciklamaKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<BYSSozcukAciklama> ara(params Predicate<BYSSozcukAciklama>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.BYSSozcukAciklamas.ToList().FindAll(kosul).OrderByDescending(p => p.bysSozcukAciklamaKimlik).ToList(); }
        }
        public static List<BYSSozcukAciklama> tamami(Varlik kime)
        {
            return kime.BYSSozcukAciklamas.Where(p => p.varmi == true).OrderByDescending(p => p.bysSozcukAciklamaKimlik).ToList();
        }
        public static List<BYSSozcukAciklama> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<BYSSozcukAciklama> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static BYSSozcukAciklama tekliCek(Int64 kimlik, Varlik kime)
        {
            BYSSozcukAciklama kayit = kime.BYSSozcukAciklamas.FirstOrDefault(p => p.bysSozcukAciklamaKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(BYSSozcukAciklama yeni, ref Varlik kime)
        {
            if (yeni.bysSozcukAciklamaKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.BYSSozcukAciklamas.Add(yeni);
            }
            else
            {
                var bulunan = kime.BYSSozcukAciklamas.FirstOrDefault(p => p.bysSozcukAciklamaKimlik == yeni.bysSozcukAciklamaKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(BYSSozcukAciklama yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.BYSSozcukAciklamas.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(BYSSozcukAciklama yeni, Varlik kime)
        {
            var bulunan = kime.BYSSozcukAciklamas.FirstOrDefault(p => p.bysSozcukAciklamaKimlik == yeni.bysSozcukAciklamaKimlik);
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
        public static void kaydet(BYSSozcukAciklama yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.bysSozcukAciklamaKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.BYSSozcukAciklamas.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.BYSSozcukAciklamas.FirstOrDefault(p => p.bysSozcukAciklamaKimlik == yeni.bysSozcukAciklamaKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(BYSSozcukAciklama kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.BYSSozcukAciklamas.FirstOrDefault(p => p.bysSozcukAciklamaKimlik == kimi.bysSozcukAciklamaKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
