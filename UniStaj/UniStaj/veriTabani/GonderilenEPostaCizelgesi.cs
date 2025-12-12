using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GonderilenEPostaArama
    {
        public Int64? gonderilenEPostaKimlik { get; set; }
        public string adres { get; set; }
        public string baslik { get; set; }
        public Int32? i_ePostaTuruKimlik { get; set; }
        public DateTime? gonderimTarihi { get; set; }
        public string metin { get; set; }
        public bool? varmi { get; set; }
        public Int64? y_topluGonderimKimlik { get; set; }
        public string tcKimlikNo { get; set; }
        public string kisiAdi { get; set; }
        public GonderilenEPostaArama()
        {
            varmi = true;
        }
        public Predicate<GonderilenEPosta> kosulu()
        {
            List<Predicate<GonderilenEPosta>> kosullar = new List<Predicate<GonderilenEPosta>>();
            kosullar.Add(c => c.varmi == true);
            if (gonderilenEPostaKimlik != null)
                kosullar.Add(c => c.gonderilenEPostaKimlik == gonderilenEPostaKimlik);
            if (adres != null)
                kosullar.Add(c => c.adres == adres);
            if (baslik != null)
                kosullar.Add(c => c.baslik == baslik);
            if (i_ePostaTuruKimlik != null)
                kosullar.Add(c => c.i_ePostaTuruKimlik == i_ePostaTuruKimlik);
            if (gonderimTarihi != null)
                kosullar.Add(c => c.gonderimTarihi == gonderimTarihi);
            if (metin != null)
                kosullar.Add(c => c.metin == metin);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (y_topluGonderimKimlik != null)
                kosullar.Add(c => c.y_topluGonderimKimlik == y_topluGonderimKimlik);
            if (tcKimlikNo != null)
                kosullar.Add(c => c.tcKimlikNo == tcKimlikNo);
            if (kisiAdi != null)
                kosullar.Add(c => c.kisiAdi == kisiAdi);
            Predicate<GonderilenEPosta> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class GonderilenEPostaCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<GonderilenEPosta> ara(GonderilenEPostaArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.GonderilenEPostas.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.gonderilenEPostaKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<GonderilenEPosta> ara(params Predicate<GonderilenEPosta>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.GonderilenEPostas.ToList().FindAll(kosul).OrderByDescending(p => p.gonderilenEPostaKimlik).ToList(); }
        }
        public static List<GonderilenEPosta> tamami(Varlik kime)
        {
            return kime.GonderilenEPostas.Where(p => p.varmi == true).OrderByDescending(p => p.gonderilenEPostaKimlik).ToList();
        }
        public static List<GonderilenEPosta> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<GonderilenEPosta> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static GonderilenEPosta tekliCek(Int64 kimlik, Varlik kime)
        {
            GonderilenEPosta kayit = kime.GonderilenEPostas.FirstOrDefault(p => p.gonderilenEPostaKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(GonderilenEPosta yeni, ref Varlik kime)
        {
            if (yeni.gonderilenEPostaKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.GonderilenEPostas.Add(yeni);
            }
            else
            {
                var bulunan = kime.GonderilenEPostas.FirstOrDefault(p => p.gonderilenEPostaKimlik == yeni.gonderilenEPostaKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(GonderilenEPosta yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.GonderilenEPostas.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(GonderilenEPosta yeni, Varlik kime)
        {
            var bulunan = kime.GonderilenEPostas.FirstOrDefault(p => p.gonderilenEPostaKimlik == yeni.gonderilenEPostaKimlik);
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
        public static void kaydet(GonderilenEPosta yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.gonderilenEPostaKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.GonderilenEPostas.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.GonderilenEPostas.FirstOrDefault(p => p.gonderilenEPostaKimlik == yeni.gonderilenEPostaKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(GonderilenEPosta kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.GonderilenEPostas.FirstOrDefault(p => p.gonderilenEPostaKimlik == kimi.gonderilenEPostaKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }


        public static async Task kaydetKos(GonderilenEPosta yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.gonderilenEPostaKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.GonderilenEPostas.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                GonderilenEPosta? bulunan = await vari.GonderilenEPostas.FirstOrDefaultAsync(p => p.gonderilenEPostaKimlik == yeni.gonderilenEPostaKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }

    }
}
