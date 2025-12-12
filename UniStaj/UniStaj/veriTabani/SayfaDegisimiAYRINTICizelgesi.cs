using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SayfaDegisimiAYRINTIArama
    {
        public Int32? sayfaDegisimiKimlik { get; set; }
        public string ad { get; set; }
        public bool? varmi { get; set; }
        public SayfaDegisimiAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<SayfaDegisimiAYRINTI> kosulu()
        {
            List<Predicate<SayfaDegisimiAYRINTI>> kosullar = new List<Predicate<SayfaDegisimiAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (sayfaDegisimiKimlik != null)
                kosullar.Add(c => c.sayfaDegisimiKimlik == sayfaDegisimiKimlik);
            if (ad != null)
                kosullar.Add(c => c.ad == ad);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<SayfaDegisimiAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class SayfaDegisimiAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<SayfaDegisimiAYRINTI> ara(SayfaDegisimiAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.SayfaDegisimiAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.sayfaDegisimiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SayfaDegisimiAYRINTI> ara(params Predicate<SayfaDegisimiAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.SayfaDegisimiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.sayfaDegisimiKimlik).ToList(); }
        }
        public static List<SayfaDegisimiAYRINTI> tamami(Varlik kime)
        {
            return kime.SayfaDegisimiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.sayfaDegisimiKimlik).ToList();
        }
        public static List<SayfaDegisimiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<SayfaDegisimiAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static SayfaDegisimiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            SayfaDegisimiAYRINTI kayit = kime.SayfaDegisimiAYRINTIs.FirstOrDefault(p => p.sayfaDegisimiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
