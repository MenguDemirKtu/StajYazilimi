using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class TopluEPostaAlicisiAYRINTIArama
    {
        public Int64? topluEPostaAlicisiKimlik { get; set; }
        public Int32? i_topluSMSGonderimKimlik { get; set; }
        public string? metin { get; set; }
        public string? aliciTanimi { get; set; }
        public bool? varmi { get; set; }
        public string? ePostaAdresi { get; set; }
        public TopluEPostaAlicisiAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<TopluEPostaAlicisiAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<TopluEPostaAlicisiAYRINTI>(P => P.varmi == true);
            if (topluEPostaAlicisiKimlik != null)
                predicate = predicate.And(x => x.topluEPostaAlicisiKimlik == topluEPostaAlicisiKimlik);
            if (i_topluSMSGonderimKimlik != null)
                predicate = predicate.And(x => x.i_topluSMSGonderimKimlik == i_topluSMSGonderimKimlik);
            if (metin != null)
                predicate = predicate.And(x => x.metin.Contains(metin));
            if (aliciTanimi != null)
                predicate = predicate.And(x => x.aliciTanimi.Contains(aliciTanimi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (ePostaAdresi != null)
                predicate = predicate.And(x => x.ePostaAdresi.Contains(ePostaAdresi));
            List<TopluEPostaAlicisiAYRINTI> sonuc = new List<TopluEPostaAlicisiAYRINTI>();
            sonuc = await vari.TopluEPostaAlicisiAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class TopluEPostaAlicisiAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<TopluEPostaAlicisiAYRINTI> ara(params Predicate<TopluEPostaAlicisiAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.TopluEPostaAlicisiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.topluEPostaAlicisiKimlik).ToList();
            }
        }


        public static List<TopluEPostaAlicisiAYRINTI> tamami(Varlik kime)
        {
            return kime.TopluEPostaAlicisiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.topluEPostaAlicisiKimlik).ToList();
        }
        public static async Task<TopluEPostaAlicisiAYRINTI?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            TopluEPostaAlicisiAYRINTI? kayit = await kime.TopluEPostaAlicisiAYRINTIs.FirstOrDefaultAsync(p => p.topluEPostaAlicisiKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static TopluEPostaAlicisiAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            TopluEPostaAlicisiAYRINTI kayit = kime.TopluEPostaAlicisiAYRINTIs.FirstOrDefault(p => p.topluEPostaAlicisiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

