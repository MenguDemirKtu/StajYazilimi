using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SistemHatasiArama
    {
        public Int64? sistemHatasiKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public string sayfaBaslik { get; set; }
        public string aciklama { get; set; }
        public bool? varmi { get; set; }
        public string kodu { get; set; }
        public string tanitim { get; set; }
        public SistemHatasiArama()
        {
            varmi = true;
        }
        public Predicate<SistemHatasi> kosulu()
        {
            List<Predicate<SistemHatasi>> kosullar = new List<Predicate<SistemHatasi>>();
            kosullar.Add(c => c.varmi == true);
            if (sistemHatasiKimlik != null)
                kosullar.Add(c => c.sistemHatasiKimlik == sistemHatasiKimlik);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (sayfaBaslik != null)
                kosullar.Add(c => c.sayfaBaslik == sayfaBaslik);
            if (aciklama != null)
                kosullar.Add(c => c.aciklama == aciklama);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (kodu != null)
                kosullar.Add(c => c.kodu == kodu);
            if (tanitim != null)
                kosullar.Add(c => c.tanitim == tanitim);
            Predicate<SistemHatasi> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class SistemHatasiCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<SistemHatasi> ara(SistemHatasiArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.SistemHatasis.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.sistemHatasiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SistemHatasi> ara(params Predicate<SistemHatasi>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new Varlik())
            {
                return vari.SistemHatasis.ToList().FindAll(kosul).OrderByDescending(p => p.sistemHatasiKimlik).ToList();
            }
        }
        public static List<SistemHatasi> tamami(Varlik kime)
        {
            return kime.SistemHatasis.Where(p => p.varmi == true).OrderByDescending(p => p.sistemHatasiKimlik).ToList();
        }
        public static List<SistemHatasi> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<SistemHatasi> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static SistemHatasi tekliCek(Int64 kimlik, Varlik kime)
        {
            SistemHatasi kayit = kime.SistemHatasis.FirstOrDefault(p => p.sistemHatasiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(SistemHatasi yeni, ref Varlik kime)
        {
            if (yeni.sistemHatasiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SistemHatasis.Add(yeni);
            }
            else
            {
                var bulunan = kime.SistemHatasis.FirstOrDefault(p => p.sistemHatasiKimlik == yeni.sistemHatasiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(SistemHatasi yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.SistemHatasis.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(SistemHatasi yeni, Varlik kime)
        {
            var bulunan = kime.SistemHatasis.FirstOrDefault(p => p.sistemHatasiKimlik == yeni.sistemHatasiKimlik);
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
        public static void kaydet(SistemHatasi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sistemHatasiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SistemHatasis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.SistemHatasis.FirstOrDefault(p => p.sistemHatasiKimlik == yeni.sistemHatasiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(SistemHatasi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SistemHatasis.FirstOrDefault(p => p.sistemHatasiKimlik == kimi.sistemHatasiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
