using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class HataYazismasiAYRINTIArama
    {
        public Int64? hataYasizmasiKimlik { get; set; }
        public Int64? i_hataBildirimiKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public Int64? i_yoneticiKimlik { get; set; }
        public string metin { get; set; }
        public bool? varmi { get; set; }
        public HataYazismasiAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<HataYazismasiAYRINTI> kosulu()
        {
            List<Predicate<HataYazismasiAYRINTI>> kosullar = new List<Predicate<HataYazismasiAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (hataYasizmasiKimlik != null)
                kosullar.Add(c => c.hataYasizmasiKimlik == hataYasizmasiKimlik);
            if (i_hataBildirimiKimlik != null)
                kosullar.Add(c => c.i_hataBildirimiKimlik == i_hataBildirimiKimlik);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (i_yoneticiKimlik != null)
                kosullar.Add(c => c.i_yoneticiKimlik == i_yoneticiKimlik);
            if (metin != null)
                kosullar.Add(c => c.metin == metin);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<HataYazismasiAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class HataYazismasiAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<HataYazismasiAYRINTI> ara(HataYazismasiAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.HataYazismasiAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.hataYasizmasiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<HataYazismasiAYRINTI> ara(params Predicate<HataYazismasiAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.HataYazismasiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.hataYasizmasiKimlik).ToList(); }
        }
        public static List<HataYazismasiAYRINTI> tamami(Varlik kime)
        {
            return kime.HataYazismasiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.hataYasizmasiKimlik).ToList();
        }
        public static List<HataYazismasiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<HataYazismasiAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static HataYazismasiAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            HataYazismasiAYRINTI kayit = kime.HataYazismasiAYRINTIs.FirstOrDefault(p => p.hataYasizmasiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
