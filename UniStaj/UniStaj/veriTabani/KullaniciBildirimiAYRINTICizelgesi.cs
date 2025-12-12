using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KullaniciBildirimiAYRINTIArama
    {
        public Int64? kullaniciBildirimiKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public string kullaniciAdi { get; set; }
        public string bildirimBasligi { get; set; }
        public string bildirimTanitimi { get; set; }
        public DateTime? tarihi { get; set; }
        public string ayrintisi { get; set; }
        public Byte? e_goruldumu { get; set; }
        public string goruldumu { get; set; }
        public DateTime? gorulmeTarihi { get; set; }
        public bool? varmi { get; set; }
        public string url { get; set; }
        public string kodu { get; set; }
        public KullaniciBildirimiAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<KullaniciBildirimiAYRINTI> kosulu()
        {
            List<Predicate<KullaniciBildirimiAYRINTI>> kosullar = new List<Predicate<KullaniciBildirimiAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (kullaniciBildirimiKimlik != null)
                kosullar.Add(c => c.kullaniciBildirimiKimlik == kullaniciBildirimiKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (kullaniciAdi != null)
                kosullar.Add(c => c.kullaniciAdi == kullaniciAdi);
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
            if (goruldumu != null)
                kosullar.Add(c => c.goruldumu == goruldumu);
            if (gorulmeTarihi != null)
                kosullar.Add(c => c.gorulmeTarihi == gorulmeTarihi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (url != null)
                kosullar.Add(c => c.url == url);
            if (kodu != null)
                kosullar.Add(c => c.kodu == kodu);
            Predicate<KullaniciBildirimiAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KullaniciBildirimiAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KullaniciBildirimiAYRINTI> ara(KullaniciBildirimiAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciBildirimiAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.kullaniciBildirimiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KullaniciBildirimiAYRINTI> ara(params Predicate<KullaniciBildirimiAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciBildirimiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.kullaniciBildirimiKimlik).ToList(); }
        }
        public static List<KullaniciBildirimiAYRINTI> tamami(Varlik kime)
        {
            return kime.KullaniciBildirimiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.kullaniciBildirimiKimlik).ToList();
        }
        public static List<KullaniciBildirimiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KullaniciBildirimiAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KullaniciBildirimiAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciBildirimiAYRINTI kayit = kime.KullaniciBildirimiAYRINTIs.FirstOrDefault(p => p.kullaniciBildirimiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
