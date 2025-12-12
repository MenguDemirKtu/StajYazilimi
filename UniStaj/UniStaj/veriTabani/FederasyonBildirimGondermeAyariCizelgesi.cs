using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class FederasyonBildirimGondermeAyariArama
    {
        public Int32? federasyonBildirimGondermeAyariKimlik { get; set; }
        public Int64? i_kullaniciKimlik { get; set; }
        public Int32? i_federasyonBildirimTuruKimlik { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public FederasyonBildirimGondermeAyariArama()
        {
            varmi = true;
        }
        public Predicate<FederasyonBildirimGondermeAyari> kosulu()
        {
            List<Predicate<FederasyonBildirimGondermeAyari>> kosullar = new List<Predicate<FederasyonBildirimGondermeAyari>>();
            kosullar.Add(c => c.varmi == true);
            if (federasyonBildirimGondermeAyariKimlik != null)
                kosullar.Add(c => c.federasyonBildirimGondermeAyariKimlik == federasyonBildirimGondermeAyariKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (i_federasyonBildirimTuruKimlik != null)
                kosullar.Add(c => c.i_federasyonBildirimTuruKimlik == i_federasyonBildirimTuruKimlik);
            if (e_gecerlimi != null)
                kosullar.Add(c => c.e_gecerlimi == e_gecerlimi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<FederasyonBildirimGondermeAyari> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class FederasyonBildirimGondermeAyariCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<FederasyonBildirimGondermeAyari> ara(FederasyonBildirimGondermeAyariArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.FederasyonBildirimGondermeAyaris.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.federasyonBildirimGondermeAyariKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<FederasyonBildirimGondermeAyari> ara(params Predicate<FederasyonBildirimGondermeAyari>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.FederasyonBildirimGondermeAyaris.ToList().FindAll(kosul).OrderByDescending(p => p.federasyonBildirimGondermeAyariKimlik).ToList(); }
        }
        public static List<FederasyonBildirimGondermeAyari> tamami(Varlik kime)
        {
            return kime.FederasyonBildirimGondermeAyaris.Where(p => p.varmi == true).OrderByDescending(p => p.federasyonBildirimGondermeAyariKimlik).ToList();
        }
        public static List<FederasyonBildirimGondermeAyari> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<FederasyonBildirimGondermeAyari> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static FederasyonBildirimGondermeAyari tekliCek(Int32 kimlik, Varlik kime)
        {
            FederasyonBildirimGondermeAyari kayit = kime.FederasyonBildirimGondermeAyaris.FirstOrDefault(p => p.federasyonBildirimGondermeAyariKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(FederasyonBildirimGondermeAyari yeni, ref Varlik kime)
        {
            if (yeni.federasyonBildirimGondermeAyariKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.FederasyonBildirimGondermeAyaris.Add(yeni);
            }
            else
            {
                var bulunan = kime.FederasyonBildirimGondermeAyaris.FirstOrDefault(p => p.federasyonBildirimGondermeAyariKimlik == yeni.federasyonBildirimGondermeAyariKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(FederasyonBildirimGondermeAyari yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.FederasyonBildirimGondermeAyaris.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(FederasyonBildirimGondermeAyari yeni, Varlik kime)
        {
            var bulunan = kime.FederasyonBildirimGondermeAyaris.FirstOrDefault(p => p.federasyonBildirimGondermeAyariKimlik == yeni.federasyonBildirimGondermeAyariKimlik);
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
        public static void kaydet(FederasyonBildirimGondermeAyari yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.federasyonBildirimGondermeAyariKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.FederasyonBildirimGondermeAyaris.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.FederasyonBildirimGondermeAyaris.FirstOrDefault(p => p.federasyonBildirimGondermeAyariKimlik == yeni.federasyonBildirimGondermeAyariKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(FederasyonBildirimGondermeAyari kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.FederasyonBildirimGondermeAyaris.FirstOrDefault(p => p.federasyonBildirimGondermeAyariKimlik == kimi.federasyonBildirimGondermeAyariKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
