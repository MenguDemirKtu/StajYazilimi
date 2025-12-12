using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class FotografArama
    {
        public Int64? fotografKimlik { get; set; }
        public string ilgiliCizelge { get; set; }
        public Int64? ilgiliKimlik { get; set; }
        public string konum { get; set; }
        public DateTime? yuklemeTarihi { get; set; }
        public bool? varmi { get; set; }
        public Int32? genislik { get; set; }
        public Int32? yukseklik { get; set; }
        public bool? e_sabitmi { get; set; }
        public FotografArama()
        {
            varmi = true;
        }
        public Predicate<Fotograf> kosulu()
        {
            List<Predicate<Fotograf>> kosullar = new List<Predicate<Fotograf>>();
            kosullar.Add(c => c.varmi == true);
            if (fotografKimlik != null)
                kosullar.Add(c => c.fotografKimlik == fotografKimlik);
            if (ilgiliCizelge != null)
                kosullar.Add(c => c.ilgiliCizelge == ilgiliCizelge);
            if (ilgiliKimlik != null)
                kosullar.Add(c => c.ilgiliKimlik == ilgiliKimlik);
            if (konum != null)
                kosullar.Add(c => c.konum == konum);
            if (yuklemeTarihi != null)
                kosullar.Add(c => c.yuklemeTarihi == yuklemeTarihi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (genislik != null)
                kosullar.Add(c => c.genislik == genislik);
            if (yukseklik != null)
                kosullar.Add(c => c.yukseklik == yukseklik);
            if (e_sabitmi != null)
                kosullar.Add(c => c.e_sabitmi == e_sabitmi);
            Predicate<Fotograf> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class FotografCizelgesi
    {
        public static async Task kaydetKos(Fotograf yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.fotografKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Fotografs.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Fotograf? bulunan = await vari.Fotografs.FirstOrDefaultAsync(p => p.fotografKimlik == yeni.fotografKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }

        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<Fotograf> ara(FotografArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.Fotografs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.fotografKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Fotograf> ara(params Predicate<Fotograf>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.Fotografs.ToList().FindAll(kosul).OrderByDescending(p => p.fotografKimlik).ToList(); }
        }
        public static List<Fotograf> tamami(Varlik kime)
        {
            return kime.Fotografs.Where(p => p.varmi == true).OrderByDescending(p => p.fotografKimlik).ToList();
        }
        public static List<Fotograf> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<Fotograf> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static Fotograf tekliCek(Int64 kimlik, Varlik kime)
        {
            Fotograf kayit = kime.Fotografs.FirstOrDefault(p => p.fotografKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(Fotograf yeni, ref Varlik kime)
        {
            if (yeni.fotografKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Fotografs.Add(yeni);
            }
            else
            {
                var bulunan = kime.Fotografs.FirstOrDefault(p => p.fotografKimlik == yeni.fotografKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(Fotograf yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.Fotografs.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(Fotograf yeni, Varlik kime)
        {
            var bulunan = kime.Fotografs.FirstOrDefault(p => p.fotografKimlik == yeni.fotografKimlik);
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
        public static void kaydet(Fotograf yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.fotografKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Fotografs.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.Fotografs.FirstOrDefault(p => p.fotografKimlik == yeni.fotografKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(Fotograf kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Fotografs.FirstOrDefault(p => p.fotografKimlik == kimi.fotografKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
