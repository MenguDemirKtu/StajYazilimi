using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KullaniciWebSayfasiIzniArama
    {
        public Int64? kullaniciWebSayfasiIzniKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public Int32? i_webSayfasiKimlik { get; set; }
        public bool? e_gormeIzniVarmi { get; set; }
        public bool? e_eklemeIzniVarmi { get; set; }
        public bool? e_silmeIzniVarmi { get; set; }
        public bool? e_guncellemeIzniVarmi { get; set; }
        public bool? varmi { get; set; }
        public KullaniciWebSayfasiIzniArama()
        {
            varmi = true;
        }
        public Predicate<KullaniciWebSayfasiIzni> kosulu()
        {
            List<Predicate<KullaniciWebSayfasiIzni>> kosullar = new List<Predicate<KullaniciWebSayfasiIzni>>();
            kosullar.Add(c => c.varmi == true);
            if (kullaniciWebSayfasiIzniKimlik != null)
                kosullar.Add(c => c.kullaniciWebSayfasiIzniKimlik == kullaniciWebSayfasiIzniKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (i_webSayfasiKimlik != null)
                kosullar.Add(c => c.i_webSayfasiKimlik == i_webSayfasiKimlik);
            if (e_gormeIzniVarmi != null)
                kosullar.Add(c => c.e_gormeIzniVarmi == e_gormeIzniVarmi);
            if (e_eklemeIzniVarmi != null)
                kosullar.Add(c => c.e_eklemeIzniVarmi == e_eklemeIzniVarmi);
            if (e_silmeIzniVarmi != null)
                kosullar.Add(c => c.e_silmeIzniVarmi == e_silmeIzniVarmi);
            if (e_guncellemeIzniVarmi != null)
                kosullar.Add(c => c.e_guncellemeIzniVarmi == e_guncellemeIzniVarmi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<KullaniciWebSayfasiIzni> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KullaniciWebSayfasiIzniCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KullaniciWebSayfasiIzni> ara(KullaniciWebSayfasiIzniArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciWebSayfasiIznis.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.kullaniciWebSayfasiIzniKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KullaniciWebSayfasiIzni> ara(params Predicate<KullaniciWebSayfasiIzni>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciWebSayfasiIznis.ToList().FindAll(kosul).OrderByDescending(p => p.kullaniciWebSayfasiIzniKimlik).ToList(); }
        }
        public static List<KullaniciWebSayfasiIzni> tamami(Varlik kime)
        {
            return kime.KullaniciWebSayfasiIznis.Where(p => p.varmi == true).OrderByDescending(p => p.kullaniciWebSayfasiIzniKimlik).ToList();
        }
        public static List<KullaniciWebSayfasiIzni> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KullaniciWebSayfasiIzni> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KullaniciWebSayfasiIzni tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciWebSayfasiIzni kayit = kime.KullaniciWebSayfasiIznis.FirstOrDefault(p => p.kullaniciWebSayfasiIzniKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(KullaniciWebSayfasiIzni yeni, ref Varlik kime)
        {
            if (yeni.kullaniciWebSayfasiIzniKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.KullaniciWebSayfasiIznis.Add(yeni);
            }
            else
            {
                var bulunan = kime.KullaniciWebSayfasiIznis.FirstOrDefault(p => p.kullaniciWebSayfasiIzniKimlik == yeni.kullaniciWebSayfasiIzniKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(KullaniciWebSayfasiIzni yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.KullaniciWebSayfasiIznis.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(KullaniciWebSayfasiIzni yeni, Varlik kime)
        {
            var bulunan = kime.KullaniciWebSayfasiIznis.FirstOrDefault(p => p.kullaniciWebSayfasiIzniKimlik == yeni.kullaniciWebSayfasiIzniKimlik);
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
        public static void kaydet(KullaniciWebSayfasiIzni yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.kullaniciWebSayfasiIzniKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.KullaniciWebSayfasiIznis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.KullaniciWebSayfasiIznis.FirstOrDefault(p => p.kullaniciWebSayfasiIzniKimlik == yeni.kullaniciWebSayfasiIzniKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(KullaniciWebSayfasiIzni kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.KullaniciWebSayfasiIznis.FirstOrDefault(p => p.kullaniciWebSayfasiIzniKimlik == kimi.kullaniciWebSayfasiIzniKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
