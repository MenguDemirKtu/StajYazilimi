using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GonderilenEPostaAYRINTIArama
    {
        public Int64? gonderilenEPostaKimlik { get; set; }
        public string adres { get; set; }
        public string baslik { get; set; }
        public Int32? i_ePostaTuruKimlik { get; set; }
        public string ePostaTuru { get; set; }
        public DateTime? gonderimTarihi { get; set; }
        public string metin { get; set; }
        public bool? varmi { get; set; }
        public string tcKimlikNo { get; set; }
        public Int64? y_topluGonderimKimlik { get; set; }
        public string kisiAdi { get; set; }
        public GonderilenEPostaAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<GonderilenEPostaAYRINTI> kosulu()
        {
            List<Predicate<GonderilenEPostaAYRINTI>> kosullar = new List<Predicate<GonderilenEPostaAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (gonderilenEPostaKimlik != null)
                kosullar.Add(c => c.gonderilenEPostaKimlik == gonderilenEPostaKimlik);
            if (adres != null)
                kosullar.Add(c => c.adres == adres);
            if (baslik != null)
                kosullar.Add(c => c.baslik == baslik);
            if (i_ePostaTuruKimlik != null)
                kosullar.Add(c => c.i_ePostaTuruKimlik == i_ePostaTuruKimlik);
            if (ePostaTuru != null)
                kosullar.Add(c => c.ePostaTuru == ePostaTuru);
            if (gonderimTarihi != null)
                kosullar.Add(c => c.gonderimTarihi == gonderimTarihi);
            if (metin != null)
                kosullar.Add(c => c.metin == metin);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (tcKimlikNo != null)
                kosullar.Add(c => c.tcKimlikNo == tcKimlikNo);
            if (y_topluGonderimKimlik != null)
                kosullar.Add(c => c.y_topluGonderimKimlik == y_topluGonderimKimlik);
            if (kisiAdi != null)
                kosullar.Add(c => c.kisiAdi == kisiAdi);
            Predicate<GonderilenEPostaAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class GonderilenEPostaAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<GonderilenEPostaAYRINTI> ara(GonderilenEPostaAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.GonderilenEPostaAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.gonderilenEPostaKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<GonderilenEPostaAYRINTI> ara(params Predicate<GonderilenEPostaAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.GonderilenEPostaAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.gonderilenEPostaKimlik).ToList(); }
        }
        public static List<GonderilenEPostaAYRINTI> tamami(Varlik kime)
        {
            return kime.GonderilenEPostaAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.gonderilenEPostaKimlik).ToList();
        }
        public static List<GonderilenEPostaAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<GonderilenEPostaAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static GonderilenEPostaAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            GonderilenEPostaAYRINTI kayit = kime.GonderilenEPostaAYRINTIs.FirstOrDefault(p => p.gonderilenEPostaKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
