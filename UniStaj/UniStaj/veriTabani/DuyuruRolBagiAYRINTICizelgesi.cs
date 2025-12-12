using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class DuyuruRolBagiAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<DuyuruRolBagiAYRINTI> ara(params Predicate<DuyuruRolBagiAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.DuyuruRolBagiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.duyuruRolBagiKimlik).ToList();
            }
        }


        public static List<DuyuruRolBagiAYRINTI> tamami(Varlik kime)
        {
            return kime.DuyuruRolBagiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.duyuruRolBagiKimlik).ToList();
        }


        public static List<DuyuruRolBagiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<DuyuruRolBagiAYRINTI> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static DuyuruRolBagiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            DuyuruRolBagiAYRINTI kayit = kime.DuyuruRolBagiAYRINTIs.FirstOrDefault(p => p.duyuruRolBagiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

