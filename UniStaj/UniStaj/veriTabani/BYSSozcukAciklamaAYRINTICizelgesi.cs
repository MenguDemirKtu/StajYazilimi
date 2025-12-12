using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class BYSSozcukAciklamaAYRINTIArama
    {
        public Int64? bysSozcukAciklamaKimlik { get; set; }
        public Int32? i_bysSozcukKimlik { get; set; }
        public string ifade { get; set; }
        public bool? varmi { get; set; }
        public Int32? i_dilKimlik { get; set; }
        public string dilAdi { get; set; }
        public string sozcuk { get; set; }
        public Int32? bysSozcukKimlik { get; set; }
        public BYSSozcukAciklamaAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<BYSSozcukAciklamaAYRINTI> kosulu()
        {
            List<Predicate<BYSSozcukAciklamaAYRINTI>> kosullar = new List<Predicate<BYSSozcukAciklamaAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (bysSozcukAciklamaKimlik != null)
                kosullar.Add(c => c.bysSozcukAciklamaKimlik == bysSozcukAciklamaKimlik);
            if (i_bysSozcukKimlik != null)
                kosullar.Add(c => c.i_bysSozcukKimlik == i_bysSozcukKimlik);
            if (ifade != null)
                kosullar.Add(c => c.ifade == ifade);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (i_dilKimlik != null)
                kosullar.Add(c => c.i_dilKimlik == i_dilKimlik);
            if (dilAdi != null)
                kosullar.Add(c => c.dilAdi == dilAdi);
            if (sozcuk != null)
                kosullar.Add(c => c.sozcuk == sozcuk);
            if (bysSozcukKimlik != null)
                kosullar.Add(c => c.bysSozcukKimlik == bysSozcukKimlik);
            Predicate<BYSSozcukAciklamaAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class BYSSozcukAciklamaAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<BYSSozcukAciklamaAYRINTI> ara(BYSSozcukAciklamaAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.BYSSozcukAciklamaAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.bysSozcukAciklamaKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<BYSSozcukAciklamaAYRINTI> ara(params Predicate<BYSSozcukAciklamaAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.BYSSozcukAciklamaAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.bysSozcukAciklamaKimlik).ToList(); }
        }
        public static List<BYSSozcukAciklamaAYRINTI> tamami(Varlik kime)
        {
            return kime.BYSSozcukAciklamaAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.bysSozcukAciklamaKimlik).ToList();
        }
        public static List<BYSSozcukAciklamaAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<BYSSozcukAciklamaAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static BYSSozcukAciklamaAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            BYSSozcukAciklamaAYRINTI kayit = kime.BYSSozcukAciklamaAYRINTIs.FirstOrDefault(p => p.bysSozcukAciklamaKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
