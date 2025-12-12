using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class DuyuruAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<DuyuruAYRINTI> ara(params Predicate<DuyuruAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.DuyuruAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.duyurukimlik).ToList();
            }
        }


        public static List<DuyuruAYRINTI> tamami(Varlik kime)
        {
            return kime.DuyuruAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.duyurukimlik).ToList();
        }


        public static List<DuyuruAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<DuyuruAYRINTI> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static DuyuruAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            DuyuruAYRINTI kayit = kime.DuyuruAYRINTIs.FirstOrDefault(p => p.duyurukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

