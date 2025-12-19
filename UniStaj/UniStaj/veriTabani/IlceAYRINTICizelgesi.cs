using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class IlceAYRINTIArama
    {
        public Int32? ilcekimlik { get; set; }
        public string? ilceAdi { get; set; }
        public Int32? i_sehirKimlik { get; set; }
        public bool? varmi { get; set; }
        public IlceAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<IlceAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<IlceAYRINTI>(P => P.varmi == true);
            if (ilcekimlik != null)
                predicate = predicate.And(x => x.ilcekimlik == ilcekimlik);
            if (ilceAdi != null)
                predicate = predicate.And(x => x.ilceAdi != null && x.ilceAdi.Contains(ilceAdi));
            if (i_sehirKimlik != null)
                predicate = predicate.And(x => x.i_sehirKimlik == i_sehirKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<IlceAYRINTI> sonuc = new List<IlceAYRINTI>();
            sonuc = await vari.IlceAYRINTIs
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class IlceAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<IlceAYRINTI>> ara(params Expression<Func<IlceAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<IlceAYRINTI>> ara(veri.Varlik vari, params Expression<Func<IlceAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.IlceAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.ilcekimlik)
                   .ToListAsync();
        }



        public static async Task<IlceAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            IlceAYRINTI? kayit = await kime.IlceAYRINTIs.FirstOrDefaultAsync(p => p.ilcekimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static IlceAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            IlceAYRINTI? kayit = kime.IlceAYRINTIs.FirstOrDefault(p => p.ilcekimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

