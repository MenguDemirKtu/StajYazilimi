using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class AramaTalebiAYRINTIArama
    {
        public Int32? aramaTalebiKimlik { get; set; }
        public string? kodu { get; set; }
        public DateTime? tarih { get; set; }
        public string? talepAyrintisi { get; set; }
        public bool? varmi { get; set; }
        public AramaTalebiAYRINTIArama()
        {
            varmi = true;
        }

        public Predicate<AramaTalebiAYRINTI> kosulu()
        {
            List<Predicate<AramaTalebiAYRINTI>> kosullar = new List<Predicate<AramaTalebiAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (aramaTalebiKimlik != null)
                kosullar.Add(c => c.aramaTalebiKimlik == aramaTalebiKimlik);
            if (kodu != null)
                kosullar.Add(c => c.kodu == kodu);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (talepAyrintisi != null)
                kosullar.Add(c => c.talepAyrintisi == talepAyrintisi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<AramaTalebiAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
        public static List<AramaTalebiAYRINTI> ara(veri.Varlik vari, AramaTalebiAYRINTIArama aramaParametresi)
        {
            return vari.AramaTalebiAYRINTIs.ToList().FindAll(aramaParametresi.kosulu()).OrderByDescending(p => p.aramaTalebiKimlik).ToList();
        }

    }


    public class AramaTalebiAYRINTICizelgesi
    {

        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<AramaTalebiAYRINTI> ara(params Predicate<AramaTalebiAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.AramaTalebiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.aramaTalebiKimlik).ToList();
            }
        }


        public static List<AramaTalebiAYRINTI> tamami(Varlik kime)
        {
            return kime.AramaTalebiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.aramaTalebiKimlik).ToList();
        }


        public static List<AramaTalebiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<AramaTalebiAYRINTI> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static AramaTalebiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            AramaTalebiAYRINTI kayit = kime.AramaTalebiAYRINTIs.FirstOrDefault(p => p.aramaTalebiKimlik == kimlik) ?? new AramaTalebiAYRINTI();
            if (kayit != null)
                if (kayit.varmi != true)
                    throw new Exception("Kayıt bulunamadı");
            return kayit ?? new AramaTalebiAYRINTI();
        }
    }
}

