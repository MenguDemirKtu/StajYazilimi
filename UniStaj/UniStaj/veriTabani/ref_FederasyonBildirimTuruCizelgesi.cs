using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_FederasyonBildirimTuruArama
    {
        public Int32? federasyonBildirimTuruKimlik { get; set; }
        public string federasyonBildirimTuruAdi { get; set; }
        public ref_FederasyonBildirimTuruArama()
        {
        }
        public Predicate<ref_FederasyonBildirimTuru> kosulu()
        {
            List<Predicate<ref_FederasyonBildirimTuru>> kosullar = new List<Predicate<ref_FederasyonBildirimTuru>>();
            if (federasyonBildirimTuruKimlik != null)
                kosullar.Add(c => c.federasyonBildirimTuruKimlik == federasyonBildirimTuruKimlik);
            if (federasyonBildirimTuruAdi != null)
                kosullar.Add(c => c.federasyonBildirimTuruAdi == federasyonBildirimTuruAdi);
            Predicate<ref_FederasyonBildirimTuru> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class ref_FederasyonBildirimTuruCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<ref_FederasyonBildirimTuru> ara(ref_FederasyonBildirimTuruArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.ref_FederasyonBildirimTurus.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.federasyonBildirimTuruKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_FederasyonBildirimTuru> ara(params Predicate<ref_FederasyonBildirimTuru>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.ref_FederasyonBildirimTurus.ToList().FindAll(kosul).OrderByDescending(p => p.federasyonBildirimTuruKimlik).ToList(); }
        }
        public static List<ref_FederasyonBildirimTuru> tamami(Varlik kime)
        {
            return kime.ref_FederasyonBildirimTurus.ToList();
        }
        public static List<ref_FederasyonBildirimTuru> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<ref_FederasyonBildirimTuru> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static ref_FederasyonBildirimTuru tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_FederasyonBildirimTuru kayit = kime.ref_FederasyonBildirimTurus.FirstOrDefault(p => p.federasyonBildirimTuruKimlik == kimlik);
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(ref_FederasyonBildirimTuru yeni, ref Varlik kime)
        {
            if (yeni.federasyonBildirimTuruKimlik <= 0)
            {
                kime.ref_FederasyonBildirimTurus.Add(yeni);
            }
            else
            {
                var bulunan = kime.ref_FederasyonBildirimTurus.FirstOrDefault(p => p.federasyonBildirimTuruKimlik == yeni.federasyonBildirimTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(ref_FederasyonBildirimTuru yeni, Varlik kime)
        {
            kime.ref_FederasyonBildirimTurus.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(ref_FederasyonBildirimTuru yeni, Varlik kime)
        {
            var bulunan = kime.ref_FederasyonBildirimTurus.FirstOrDefault(p => p.federasyonBildirimTuruKimlik == yeni.federasyonBildirimTuruKimlik);
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
        public static void kaydet(ref_FederasyonBildirimTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.federasyonBildirimTuruKimlik <= 0)
            {
                kime.ref_FederasyonBildirimTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.ref_FederasyonBildirimTurus.FirstOrDefault(p => p.federasyonBildirimTuruKimlik == yeni.federasyonBildirimTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(ref_FederasyonBildirimTuru kimi, Varlik kime)
        {
        }
    }
}
