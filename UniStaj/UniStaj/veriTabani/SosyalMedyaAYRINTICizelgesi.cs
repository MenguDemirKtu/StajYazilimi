using LinqKit;
using Microsoft.EntityFrameworkCore; //;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SosyalMedyaAYRINTIArama
    {
        public Int32? sosyalMedyakimlik { get; set; }
        public string? sosyamMedyaAdi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public string? adres { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public SosyalMedyaAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<SosyalMedyaAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SosyalMedyaAYRINTI>(P => P.varmi == true);
            if (sosyalMedyakimlik != null)
                predicate = predicate.And(x => x.sosyalMedyakimlik == sosyalMedyakimlik);
            if (sosyamMedyaAdi != null)
                predicate = predicate.And(x => x.sosyamMedyaAdi != null && x.sosyamMedyaAdi.Contains(sosyamMedyaAdi));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (adres != null)
                predicate = predicate.And(x => x.adres != null && x.adres.Contains(adres));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SosyalMedyaAYRINTI> sonuc = new List<SosyalMedyaAYRINTI>();
            sonuc = await vari.SosyalMedyaAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SosyalMedyaAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SosyalMedyaAYRINTI> ara(params Predicate<SosyalMedyaAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SosyalMedyaAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.sosyalMedyakimlik).ToList();
            }
        }


        public static List<SosyalMedyaAYRINTI> tamami(Varlik kime)
        {
            return kime.SosyalMedyaAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.sosyalMedyakimlik).ToList();
        }
        public static async Task<SosyalMedyaAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SosyalMedyaAYRINTI? kayit = await kime.SosyalMedyaAYRINTIs.FirstOrDefaultAsync(p => p.sosyalMedyakimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SosyalMedyaAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            SosyalMedyaAYRINTI? kayit = kime.SosyalMedyaAYRINTIs.FirstOrDefault(p => p.sosyalMedyakimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

