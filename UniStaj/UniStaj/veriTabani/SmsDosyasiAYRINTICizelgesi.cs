using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SmsDosyasiAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SmsDosyasiAYRINTI> ara(params Predicate<SmsDosyasiAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SmsDosyasiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.smsDosyasiKimlik).ToList();
            }
        }


        public static List<SmsDosyasiAYRINTI> tamami(Varlik kime)
        {
            return kime.SmsDosyasiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.smsDosyasiKimlik).ToList();
        }


        public static List<SmsDosyasiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<SmsDosyasiAYRINTI> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static SmsDosyasiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            SmsDosyasiAYRINTI kayit = kime.SmsDosyasiAYRINTIs.FirstOrDefault(p => p.smsDosyasiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

