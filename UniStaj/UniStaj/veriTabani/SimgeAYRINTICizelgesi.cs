using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SimgeAYRINTIArama
    {
        public Int32? simgekimlik { get; set; }
        public string? baslik { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? fotosu { get; set; }
        public bool? varmi { get; set; }
        public SimgeAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<SimgeAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SimgeAYRINTI>(P => P.varmi == true);
            if (simgekimlik != null)
                predicate = predicate.And(x => x.simgekimlik == simgekimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik != null && x.baslik.Contains(baslik));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (fotosu != null)
                predicate = predicate.And(x => x.fotosu != null && x.fotosu.Contains(fotosu));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SimgeAYRINTI> sonuc = new List<SimgeAYRINTI>();
            sonuc = await vari.SimgeAYRINTIs
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class SimgeAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<SimgeAYRINTI>> ara(params Expression<Func<SimgeAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<SimgeAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SimgeAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.SimgeAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.simgekimlik)
                   .ToListAsync();
        }



        public static async Task<SimgeAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SimgeAYRINTI? kayit = await kime.SimgeAYRINTIs.FirstOrDefaultAsync(p => p.simgekimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SimgeAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            SimgeAYRINTI? kayit = kime.SimgeAYRINTIs.FirstOrDefault(p => p.simgekimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

