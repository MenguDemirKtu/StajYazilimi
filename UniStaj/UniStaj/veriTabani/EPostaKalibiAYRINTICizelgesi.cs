using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class EPostaKalibiAYRINTIArama
    {
        public Int32? ePostaKalibiKimlik { get; set; }
        public string kalipBasligi { get; set; }
        public Int32? i_ePostaTuruKimlik { get; set; }
        public string ePostaTuru { get; set; }
        public string kalip { get; set; }
        public bool? e_gecerlimi { get; set; }
        public string gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public EPostaKalibiAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<EPostaKalibiAYRINTI> kosulu()
        {
            List<Predicate<EPostaKalibiAYRINTI>> kosullar = new List<Predicate<EPostaKalibiAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (ePostaKalibiKimlik != null)
                kosullar.Add(c => c.ePostaKalibiKimlik == ePostaKalibiKimlik);
            if (kalipBasligi != null)
                kosullar.Add(c => c.kalipBasligi == kalipBasligi);
            if (i_ePostaTuruKimlik != null)
                kosullar.Add(c => c.i_ePostaTuruKimlik == i_ePostaTuruKimlik);
            if (ePostaTuru != null)
                kosullar.Add(c => c.ePostaTuru == ePostaTuru);
            if (kalip != null)
                kosullar.Add(c => c.kalip == kalip);
            if (e_gecerlimi != null)
                kosullar.Add(c => c.e_gecerlimi == e_gecerlimi);
            if (gecerlimi != null)
                kosullar.Add(c => c.gecerlimi == gecerlimi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<EPostaKalibiAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class EPostaKalibiAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<EPostaKalibiAYRINTI> ara(EPostaKalibiAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.EPostaKalibiAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.ePostaKalibiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<EPostaKalibiAYRINTI> ara(params Predicate<EPostaKalibiAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.EPostaKalibiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.ePostaKalibiKimlik).ToList(); }
        }
        public static List<EPostaKalibiAYRINTI> tamami(Varlik kime)
        {
            return kime.EPostaKalibiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.ePostaKalibiKimlik).ToList();
        }
        public static List<EPostaKalibiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<EPostaKalibiAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static EPostaKalibiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            EPostaKalibiAYRINTI kayit = kime.EPostaKalibiAYRINTIs.FirstOrDefault(p => p.ePostaKalibiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
