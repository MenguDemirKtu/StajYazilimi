using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class BYSSozcukAYRINTIArama
    {
        public Int32? bysSozcukKimlik { get; set; }
        public string sozcuk { get; set; }
        public string parametreler { get; set; }
        public bool? varmi { get; set; }
        public BYSSozcukAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<BYSSozcukAYRINTI> kosulu()
        {
            List<Predicate<BYSSozcukAYRINTI>> kosullar = new List<Predicate<BYSSozcukAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (bysSozcukKimlik != null)
                kosullar.Add(c => c.bysSozcukKimlik == bysSozcukKimlik);
            if (sozcuk != null)
                kosullar.Add(c => c.sozcuk == sozcuk);
            if (parametreler != null)
                kosullar.Add(c => c.parametreler == parametreler);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<BYSSozcukAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class BYSSozcukAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<BYSSozcukAYRINTI> ara(BYSSozcukAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.BYSSozcukAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.bysSozcukKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<BYSSozcukAYRINTI> ara(params Predicate<BYSSozcukAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.BYSSozcukAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.bysSozcukKimlik).ToList(); }
        }
        public static List<BYSSozcukAYRINTI> tamami(Varlik kime)
        {
            return kime.BYSSozcukAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.bysSozcukKimlik).ToList();
        }
        public static List<BYSSozcukAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<BYSSozcukAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static BYSSozcukAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            BYSSozcukAYRINTI kayit = kime.BYSSozcukAYRINTIs.FirstOrDefault(p => p.bysSozcukKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
