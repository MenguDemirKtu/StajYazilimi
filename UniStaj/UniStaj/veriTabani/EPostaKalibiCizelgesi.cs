using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class EPostaKalibiArama
    {
        public Int32? ePostaKalibiKimlik { get; set; }
        public string kalipBasligi { get; set; }
        public Int32? i_ePostaTuruKimlik { get; set; }
        public string kalip { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public EPostaKalibiArama()
        {
            varmi = true;
        }
        public Predicate<EPostaKalibi> kosulu()
        {
            List<Predicate<EPostaKalibi>> kosullar = new List<Predicate<EPostaKalibi>>();
            kosullar.Add(c => c.varmi == true);
            if (ePostaKalibiKimlik != null)
                kosullar.Add(c => c.ePostaKalibiKimlik == ePostaKalibiKimlik);
            if (kalipBasligi != null)
                kosullar.Add(c => c.kalipBasligi == kalipBasligi);
            if (i_ePostaTuruKimlik != null)
                kosullar.Add(c => c.i_ePostaTuruKimlik == i_ePostaTuruKimlik);
            if (kalip != null)
                kosullar.Add(c => c.kalip == kalip);
            if (e_gecerlimi != null)
                kosullar.Add(c => c.e_gecerlimi == e_gecerlimi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<EPostaKalibi> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class EPostaKalibiCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<EPostaKalibi> ara(EPostaKalibiArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.EPostaKalibis.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.ePostaKalibiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<EPostaKalibi> ara(params Predicate<EPostaKalibi>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.EPostaKalibis.ToList().FindAll(kosul).OrderByDescending(p => p.ePostaKalibiKimlik).ToList(); }
        }
        public static List<EPostaKalibi> tamami(Varlik kime)
        {
            return kime.EPostaKalibis.Where(p => p.varmi == true).OrderByDescending(p => p.ePostaKalibiKimlik).ToList();
        }
        public static List<EPostaKalibi> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<EPostaKalibi> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static EPostaKalibi tekliCek(Int32 kimlik, Varlik kime)
        {
            EPostaKalibi kayit = kime.EPostaKalibis.FirstOrDefault(p => p.ePostaKalibiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(EPostaKalibi yeni, ref Varlik kime)
        {
            if (yeni.ePostaKalibiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.EPostaKalibis.Add(yeni);
            }
            else
            {
                var bulunan = kime.EPostaKalibis.FirstOrDefault(p => p.ePostaKalibiKimlik == yeni.ePostaKalibiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(EPostaKalibi yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.EPostaKalibis.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(EPostaKalibi yeni, Varlik kime)
        {
            var bulunan = kime.EPostaKalibis.FirstOrDefault(p => p.ePostaKalibiKimlik == yeni.ePostaKalibiKimlik);
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
        public static void kaydet(EPostaKalibi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.ePostaKalibiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.EPostaKalibis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.EPostaKalibis.FirstOrDefault(p => p.ePostaKalibiKimlik == yeni.ePostaKalibiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(EPostaKalibi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.EPostaKalibis.FirstOrDefault(p => p.ePostaKalibiKimlik == kimi.ePostaKalibiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
