using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class TopluSmsGonderimAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<TopluSmsGonderimAYRINTI> ara(params Predicate<TopluSmsGonderimAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.TopluSmsGonderimAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.topluSMSGonderimKimlik).ToList();
            }
        }


        public static List<TopluSmsGonderimAYRINTI> tamami(Varlik kime)
        {
            return kime.TopluSmsGonderimAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.topluSMSGonderimKimlik).ToList();
        }


        public static List<TopluSmsGonderimAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<TopluSmsGonderimAYRINTI> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static TopluSmsGonderimAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            TopluSmsGonderimAYRINTI kayit = kime.TopluSmsGonderimAYRINTIs.FirstOrDefault(p => p.topluSMSGonderimKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

