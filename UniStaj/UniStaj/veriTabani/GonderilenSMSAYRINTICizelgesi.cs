using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GonderilenSMSAYRINTIArama
    {
        public Int64? gonderilenSMSKimlik { get; set; }
        public string telefon { get; set; }
        public string baslik { get; set; }
        public string adSoyAd { get; set; }
        public string metin { get; set; }
        public DateTime? tarih { get; set; }
        public bool? varmi { get; set; }
        public Int64? y_topluGonderimKimlik { get; set; }
        public string tcKimlikNo { get; set; }
        public GonderilenSMSAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<GonderilenSMSAYRINTI> kosulu()
        {
            List<Predicate<GonderilenSMSAYRINTI>> kosullar = new List<Predicate<GonderilenSMSAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (gonderilenSMSKimlik != null)
                kosullar.Add(c => c.gonderilenSMSKimlik == gonderilenSMSKimlik);
            if (telefon != null)
                kosullar.Add(c => c.telefon == telefon);
            if (baslik != null)
                kosullar.Add(c => c.baslik == baslik);
            if (adSoyAd != null)
                kosullar.Add(c => c.adSoyAd == adSoyAd);
            if (metin != null)
                kosullar.Add(c => c.metin == metin);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (y_topluGonderimKimlik != null)
                kosullar.Add(c => c.y_topluGonderimKimlik == y_topluGonderimKimlik);
            if (tcKimlikNo != null)
                kosullar.Add(c => c.tcKimlikNo == tcKimlikNo);
            Predicate<GonderilenSMSAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class GonderilenSMSAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<GonderilenSMSAYRINTI> ara(GonderilenSMSAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.GonderilenSMSAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.gonderilenSMSKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<GonderilenSMSAYRINTI> ara(params Predicate<GonderilenSMSAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.GonderilenSMSAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.gonderilenSMSKimlik).ToList(); }
        }
        public static List<GonderilenSMSAYRINTI> tamami(Varlik kime)
        {
            return kime.GonderilenSMSAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.gonderilenSMSKimlik).ToList();
        }
        public static List<GonderilenSMSAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<GonderilenSMSAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static GonderilenSMSAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            GonderilenSMSAYRINTI kayit = kime.GonderilenSMSAYRINTIs.FirstOrDefault(p => p.gonderilenSMSKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
