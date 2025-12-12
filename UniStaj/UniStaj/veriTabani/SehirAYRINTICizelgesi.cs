using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SehirAYRINTIArama
    {
        public Int32? sehirKimlik { get; set; }
        public string? sehirAdi { get; set; }
        public string? telefonKodu { get; set; }
        public string? plakaKodu { get; set; }
        public bool? varmi { get; set; }
        public SehirAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<SehirAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SehirAYRINTI>(P => P.varmi == true);
            if (sehirKimlik != null)
                predicate = predicate.And(x => x.sehirKimlik == sehirKimlik);
            if (sehirAdi != null)
                predicate = predicate.And(x => x.sehirAdi != null && x.sehirAdi.Contains(sehirAdi));
            if (telefonKodu != null)
                predicate = predicate.And(x => x.telefonKodu != null && x.telefonKodu.Contains(telefonKodu));
            if (plakaKodu != null)
                predicate = predicate.And(x => x.plakaKodu != null && x.plakaKodu.Contains(plakaKodu));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SehirAYRINTI> sonuc = new List<SehirAYRINTI>();
            sonuc = await vari.SehirAYRINTIs
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class SehirAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<SehirAYRINTI>> ara(params Expression<Func<SehirAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<SehirAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SehirAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SehirAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.sehirKimlik)
                   .ToListAsync();
        }



        public static async Task<SehirAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SehirAYRINTI? kayit = await kime.SehirAYRINTIs.FirstOrDefaultAsync(p => p.sehirKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SehirAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            SehirAYRINTI? kayit = kime.SehirAYRINTIs.FirstOrDefault(p => p.sehirKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

