using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class TopluSMSAlicisiAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<TopluSMSAlicisiAYRINTI> ara(params Predicate<TopluSMSAlicisiAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.TopluSMSAlicisiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.toplSMSAlicisiKimlik).ToList();
            }
        }


        public static List<TopluSMSAlicisiAYRINTI> tamami(Varlik kime)
        {
            return kime.TopluSMSAlicisiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.toplSMSAlicisiKimlik).ToList();
        }


        public static List<TopluSMSAlicisiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }


        public static List<TopluSMSAlicisiAYRINTI> cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return tamami(vari);
            }
        }


        public static TopluSMSAlicisiAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            TopluSMSAlicisiAYRINTI kayit = kime.TopluSMSAlicisiAYRINTIs.FirstOrDefault(p => p.toplSMSAlicisiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

