using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KullaniciBildirimiArama
    {
        public Int64? kullaniciBildirimiKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public string bildirimBasligi { get; set; }
        public string bildirimTanitimi { get; set; }
        public DateTime? tarihi { get; set; }
        public string ayrintisi { get; set; }
        public Byte? e_goruldumu { get; set; }
        public DateTime? gorulmeTarihi { get; set; }
        public bool? varmi { get; set; }
        public string url { get; set; }
        public string kodu { get; set; }
        public KullaniciBildirimiArama()
        {
            varmi = true;
        }
        public Predicate<KullaniciBildirimi> kosulu()
        {
            List<Predicate<KullaniciBildirimi>> kosullar = new List<Predicate<KullaniciBildirimi>>();
            kosullar.Add(c => c.varmi == true);
            if (kullaniciBildirimiKimlik != null)
                kosullar.Add(c => c.kullaniciBildirimiKimlik == kullaniciBildirimiKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (bildirimBasligi != null)
                kosullar.Add(c => c.bildirimBasligi == bildirimBasligi);
            if (bildirimTanitimi != null)
                kosullar.Add(c => c.bildirimTanitimi == bildirimTanitimi);
            if (tarihi != null)
                kosullar.Add(c => c.tarihi == tarihi);
            if (ayrintisi != null)
                kosullar.Add(c => c.ayrintisi == ayrintisi);
            if (e_goruldumu != null)
                kosullar.Add(c => c.e_goruldumu == e_goruldumu);
            if (gorulmeTarihi != null)
                kosullar.Add(c => c.gorulmeTarihi == gorulmeTarihi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (url != null)
                kosullar.Add(c => c.url == url);
            if (kodu != null)
                kosullar.Add(c => c.kodu == kodu);
            Predicate<KullaniciBildirimi> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KullaniciBildirimiCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KullaniciBildirimi> ara(KullaniciBildirimiArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciBildirimis.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.kullaniciBildirimiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KullaniciBildirimi> ara(params Predicate<KullaniciBildirimi>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciBildirimis.ToList().FindAll(kosul).OrderByDescending(p => p.kullaniciBildirimiKimlik).ToList(); }
        }
        public static List<KullaniciBildirimi> tamami(Varlik kime)
        {
            return kime.KullaniciBildirimis.Where(p => p.varmi == true).OrderByDescending(p => p.kullaniciBildirimiKimlik).ToList();
        }
        public static List<KullaniciBildirimi> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KullaniciBildirimi> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KullaniciBildirimi tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciBildirimi kayit = kime.KullaniciBildirimis.FirstOrDefault(p => p.kullaniciBildirimiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(KullaniciBildirimi yeni, ref Varlik kime)
        {
            if (yeni.kullaniciBildirimiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.KullaniciBildirimis.Add(yeni);
            }
            else
            {
                var bulunan = kime.KullaniciBildirimis.FirstOrDefault(p => p.kullaniciBildirimiKimlik == yeni.kullaniciBildirimiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(KullaniciBildirimi yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.KullaniciBildirimis.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(KullaniciBildirimi yeni, Varlik kime)
        {
            var bulunan = kime.KullaniciBildirimis.FirstOrDefault(p => p.kullaniciBildirimiKimlik == yeni.kullaniciBildirimiKimlik);
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
        public static void kaydet(KullaniciBildirimi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.kullaniciBildirimiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.KullaniciBildirimis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.KullaniciBildirimis.FirstOrDefault(p => p.kullaniciBildirimiKimlik == yeni.kullaniciBildirimiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(KullaniciBildirimi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.KullaniciBildirimis.FirstOrDefault(p => p.kullaniciBildirimiKimlik == kimi.kullaniciBildirimiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
