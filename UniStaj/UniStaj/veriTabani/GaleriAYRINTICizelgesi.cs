using Microsoft.EntityFrameworkCore; //;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GaleriAYRINTIArama
    {
        public Int32? galeriKimlik { get; set; }
        public string? galeriAdi { get; set; }
        public string? galeriUrl { get; set; }
        public string? ilgiliCizelge { get; set; }
        public Int64? ilgiliKimlik { get; set; }
        public Int32? genislik { get; set; }
        public Int32? yukseklik { get; set; }
        public bool? varmi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public GaleriAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<GaleriAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<GaleriAYRINTI>(P => P.varmi == true);
            if (galeriKimlik != null)
                predicate = predicate.And(x => x.galeriKimlik == galeriKimlik);
            if (galeriAdi != null)
                predicate = predicate.And(x => x.galeriAdi != null && x.galeriAdi.Contains(galeriAdi));
            if (galeriUrl != null)
                predicate = predicate.And(x => x.galeriUrl != null && x.galeriUrl.Contains(galeriUrl));
            if (ilgiliCizelge != null)
                predicate = predicate.And(x => x.ilgiliCizelge != null && x.ilgiliCizelge.Contains(ilgiliCizelge));
            if (ilgiliKimlik != null)
                predicate = predicate.And(x => x.ilgiliKimlik == ilgiliKimlik);
            if (genislik != null)
                predicate = predicate.And(x => x.genislik == genislik);
            if (yukseklik != null)
                predicate = predicate.And(x => x.yukseklik == yukseklik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            List<GaleriAYRINTI> sonuc = new List<GaleriAYRINTI>();
            sonuc = await vari.GaleriAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class GaleriAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<GaleriAYRINTI> ara(params Predicate<GaleriAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.GaleriAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.galeriKimlik).ToList();
            }
        }


        public static List<GaleriAYRINTI> tamami(Varlik kime)
        {
            return kime.GaleriAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.galeriKimlik).ToList();
        }
        public static async Task<GaleriAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            GaleriAYRINTI? kayit = await kime.GaleriAYRINTIs.FirstOrDefaultAsync(p => p.galeriKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static GaleriAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            GaleriAYRINTI? kayit = kime.GaleriAYRINTIs.FirstOrDefault(p => p.galeriKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

