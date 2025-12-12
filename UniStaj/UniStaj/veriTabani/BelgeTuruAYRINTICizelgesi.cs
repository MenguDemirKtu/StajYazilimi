using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class BelgeTuruAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<BelgeTuruAYRINTI> ara(params Predicate<BelgeTuruAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.BelgeTuruAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.belgeTurukimlik).ToList();
            }
        }


        public static List<BelgeTuruAYRINTI> tamami(Varlik kime)
        {
            return kime.BelgeTuruAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.belgeTurukimlik).ToList();
        }


        public static List<BelgeTuruAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<BelgeTuruAYRINTI> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static BelgeTuruAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            BelgeTuruAYRINTI kayit = kime.BelgeTuruAYRINTIs.FirstOrDefault(p => p.belgeTurukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

