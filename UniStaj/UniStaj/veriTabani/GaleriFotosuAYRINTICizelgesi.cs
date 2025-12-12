using LinqKit;
using Microsoft.EntityFrameworkCore; //;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GaleriFotosuAYRINTIArama
    {
        public Int32? galeriFotosuKimlik { get; set; }
        public Int32? i_galeriKimlik { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public bool? e_gosterimdemi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public string? galeriAdi { get; set; }
        public string? galeriUrl { get; set; }
        public string? fotosu { get; set; }
        public string? baslik { get; set; }
        public GaleriFotosuAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<GaleriFotosuAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<GaleriFotosuAYRINTI>(P => P.varmi == true);
            if (galeriFotosuKimlik != null)
                predicate = predicate.And(x => x.galeriFotosuKimlik == galeriFotosuKimlik);
            if (i_galeriKimlik != null)
                predicate = predicate.And(x => x.i_galeriKimlik == i_galeriKimlik);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (e_gosterimdemi != null)
                predicate = predicate.And(x => x.e_gosterimdemi == e_gosterimdemi);
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (galeriAdi != null)
                predicate = predicate.And(x => x.galeriAdi != null && x.galeriAdi.Contains(galeriAdi));
            if (galeriUrl != null)
                predicate = predicate.And(x => x.galeriUrl != null && x.galeriUrl.Contains(galeriUrl));
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (baslik != null)
                predicate = predicate.And(x => x.baslik != null && x.baslik.Contains(baslik));
            List<GaleriFotosuAYRINTI> sonuc = new List<GaleriFotosuAYRINTI>();
            sonuc = await vari.GaleriFotosuAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class GaleriFotosuAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<GaleriFotosuAYRINTI> ara(params Predicate<GaleriFotosuAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.GaleriFotosuAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.galeriFotosuKimlik).ToList();
            }
        }


        public static List<GaleriFotosuAYRINTI> tamami(Varlik kime)
        {
            return kime.GaleriFotosuAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.galeriFotosuKimlik).ToList();
        }
        public static async Task<GaleriFotosuAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            GaleriFotosuAYRINTI? kayit = await kime.GaleriFotosuAYRINTIs.FirstOrDefaultAsync(p => p.galeriFotosuKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static GaleriFotosuAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            GaleriFotosuAYRINTI? kayit = kime.GaleriFotosuAYRINTIs.FirstOrDefault(p => p.galeriFotosuKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

