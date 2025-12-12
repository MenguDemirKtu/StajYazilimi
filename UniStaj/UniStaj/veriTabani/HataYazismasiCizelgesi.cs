using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class HataYazismasiArama
    {
        public Int64? hataYasizmasiKimlik { get; set; }
        public Int64? i_hataBildirimiKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public Int64? i_yoneticiKimlik { get; set; }
        public string metin { get; set; }
        public bool? varmi { get; set; }
        public HataYazismasiArama()
        {
            varmi = true;
        }
        public Predicate<HataYazismasi> kosulu()
        {
            List<Predicate<HataYazismasi>> kosullar = new List<Predicate<HataYazismasi>>();
            kosullar.Add(c => c.varmi == true);
            if (hataYasizmasiKimlik != null)
                kosullar.Add(c => c.hataYasizmasiKimlik == hataYasizmasiKimlik);
            if (i_hataBildirimiKimlik != null)
                kosullar.Add(c => c.i_hataBildirimiKimlik == i_hataBildirimiKimlik);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (i_yoneticiKimlik != null)
                kosullar.Add(c => c.i_yoneticiKimlik == i_yoneticiKimlik);
            if (metin != null)
                kosullar.Add(c => c.metin == metin);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<HataYazismasi> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class HataYazismasiCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<HataYazismasi> ara(HataYazismasiArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.HataYazismasis.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.hataYasizmasiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<HataYazismasi> ara(params Predicate<HataYazismasi>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.HataYazismasis.ToList().FindAll(kosul).OrderByDescending(p => p.hataYasizmasiKimlik).ToList(); }
        }
        public static List<HataYazismasi> tamami(Varlik kime)
        {
            return kime.HataYazismasis.Where(p => p.varmi == true).OrderByDescending(p => p.hataYasizmasiKimlik).ToList();
        }
        public static List<HataYazismasi> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<HataYazismasi> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static HataYazismasi tekliCek(Int64 kimlik, Varlik kime)
        {
            HataYazismasi kayit = kime.HataYazismasis.FirstOrDefault(p => p.hataYasizmasiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(HataYazismasi yeni, ref Varlik kime)
        {
            if (yeni.hataYasizmasiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.HataYazismasis.Add(yeni);
            }
            else
            {
                var bulunan = kime.HataYazismasis.FirstOrDefault(p => p.hataYasizmasiKimlik == yeni.hataYasizmasiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(HataYazismasi yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.HataYazismasis.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(HataYazismasi yeni, Varlik kime)
        {
            var bulunan = kime.HataYazismasis.FirstOrDefault(p => p.hataYasizmasiKimlik == yeni.hataYasizmasiKimlik);
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
        public static void kaydet(HataYazismasi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.hataYasizmasiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.HataYazismasis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.HataYazismasis.FirstOrDefault(p => p.hataYasizmasiKimlik == yeni.hataYasizmasiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(HataYazismasi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.HataYazismasis.FirstOrDefault(p => p.hataYasizmasiKimlik == kimi.hataYasizmasiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
