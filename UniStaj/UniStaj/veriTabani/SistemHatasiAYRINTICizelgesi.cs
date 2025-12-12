using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SistemHatasiAYRINTIArama
    {
        public Int64? sistemHatasiKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public string sayfaBaslik { get; set; }
        public string aciklama { get; set; }
        public bool? varmi { get; set; }
        public SistemHatasiAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<SistemHatasiAYRINTI> kosulu()
        {
            List<Predicate<SistemHatasiAYRINTI>> kosullar = new List<Predicate<SistemHatasiAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (sistemHatasiKimlik != null)
                kosullar.Add(c => c.sistemHatasiKimlik == sistemHatasiKimlik);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (sayfaBaslik != null)
                kosullar.Add(c => c.sayfaBaslik == sayfaBaslik);
            if (aciklama != null)
                kosullar.Add(c => c.aciklama == aciklama);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<SistemHatasiAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class SistemHatasiAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<SistemHatasiAYRINTI> ara(SistemHatasiAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.SistemHatasiAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.sistemHatasiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SistemHatasiAYRINTI> ara(params Predicate<SistemHatasiAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.SistemHatasiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.sistemHatasiKimlik).ToList(); }
        }
        public static List<SistemHatasiAYRINTI> tamami(Varlik kime)
        {
            return kime.SistemHatasiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.sistemHatasiKimlik).ToList();
        }
        public static List<SistemHatasiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<SistemHatasiAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static SistemHatasiAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            SistemHatasiAYRINTI kayit = kime.SistemHatasiAYRINTIs.FirstOrDefault(p => p.sistemHatasiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
