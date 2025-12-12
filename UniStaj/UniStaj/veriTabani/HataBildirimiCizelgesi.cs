using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class HataBildirimiArama
    {
        public Int32? hataBildirimiKimlik { get; set; }
        public Int32? i_yoneticiKimlik { get; set; }
        public string baslik { get; set; }
        public DateTime? tarih { get; set; }
        public bool? varmi { get; set; }
        public Byte? e_goruldumu { get; set; }
        public string hataAlinanSayfa { get; set; }
        public string hataAciklamasi { get; set; }
        public string kodu { get; set; }
        public HataBildirimiArama()
        {
            varmi = true;
        }
        public Predicate<HataBildirimi> kosulu()
        {
            List<Predicate<HataBildirimi>> kosullar = new List<Predicate<HataBildirimi>>();
            kosullar.Add(c => c.varmi == true);
            if (hataBildirimiKimlik != null)
                kosullar.Add(c => c.hataBildirimiKimlik == hataBildirimiKimlik);
            if (i_yoneticiKimlik != null)
                kosullar.Add(c => c.i_yoneticiKimlik == i_yoneticiKimlik);
            if (baslik != null)
                kosullar.Add(c => c.baslik == baslik);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (e_goruldumu != null)
                kosullar.Add(c => c.e_goruldumu == e_goruldumu);
            if (hataAlinanSayfa != null)
                kosullar.Add(c => c.hataAlinanSayfa == hataAlinanSayfa);
            if (hataAciklamasi != null)
                kosullar.Add(c => c.hataAciklamasi == hataAciklamasi);
            if (kodu != null)
                kosullar.Add(c => c.kodu == kodu);
            Predicate<HataBildirimi> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class HataBildirimiCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<HataBildirimi> ara(HataBildirimiArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.HataBildirimis.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.hataBildirimiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<HataBildirimi> ara(params Predicate<HataBildirimi>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.HataBildirimis.ToList().FindAll(kosul).OrderByDescending(p => p.hataBildirimiKimlik).ToList(); }
        }
        public static List<HataBildirimi> tamami(Varlik kime)
        {
            return kime.HataBildirimis.Where(p => p.varmi == true).OrderByDescending(p => p.hataBildirimiKimlik).ToList();
        }
        public static List<HataBildirimi> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<HataBildirimi> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static HataBildirimi tekliCek(Int32 kimlik, Varlik kime)
        {
            HataBildirimi kayit = kime.HataBildirimis.FirstOrDefault(p => p.hataBildirimiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(HataBildirimi yeni, ref Varlik kime)
        {
            if (yeni.hataBildirimiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.HataBildirimis.Add(yeni);
            }
            else
            {
                var bulunan = kime.HataBildirimis.FirstOrDefault(p => p.hataBildirimiKimlik == yeni.hataBildirimiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(HataBildirimi yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.HataBildirimis.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(HataBildirimi yeni, Varlik kime)
        {
            var bulunan = kime.HataBildirimis.FirstOrDefault(p => p.hataBildirimiKimlik == yeni.hataBildirimiKimlik);
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
        public static void kaydet(HataBildirimi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.oncelik == null)
                yeni.oncelik = 100;
            if (yeni.hataBildirimiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.HataBildirimis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.HataBildirimis.FirstOrDefault(p => p.hataBildirimiKimlik == yeni.hataBildirimiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(HataBildirimi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.HataBildirimis.FirstOrDefault(p => p.hataBildirimiKimlik == kimi.hataBildirimiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
