using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class FotografAYRINTIArama
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
        public FotografAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<FotografAYRINTI> kosulu()
        {
            List<Predicate<FotografAYRINTI>> kosullar = new List<Predicate<FotografAYRINTI>>();
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
            Predicate<FotografAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class FotografAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<FotografAYRINTI> ara(FotografAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.FotografAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.fotografKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<FotografAYRINTI> ara(params Predicate<FotografAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.FotografAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.fotografKimlik).ToList(); }
        }
        public static List<FotografAYRINTI> tamami(Varlik kime)
        {
            return kime.FotografAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.fotografKimlik).ToList();
        }
        public static List<FotografAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<FotografAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static FotografAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            FotografAYRINTI kayit = kime.FotografAYRINTIs.FirstOrDefault(p => p.fotografKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
