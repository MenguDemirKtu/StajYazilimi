using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class UlkeAYRINTIArama
    {
        public Int32? ulkeKimlik { get; set; }
        public string? ulkeAdi { get; set; }
        public string? ingilizceAdi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public UlkeAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<UlkeAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<UlkeAYRINTI>(P => P.varmi == true);
            if (ulkeKimlik != null)
                predicate = predicate.And(x => x.ulkeKimlik == ulkeKimlik);
            if (ulkeAdi != null)
                predicate = predicate.And(x => x.ulkeAdi != null && x.ulkeAdi.Contains(ulkeAdi));
            if (ingilizceAdi != null)
                predicate = predicate.And(x => x.ingilizceAdi != null && x.ingilizceAdi.Contains(ingilizceAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<UlkeAYRINTI> sonuc = new List<UlkeAYRINTI>();
            sonuc = await vari.UlkeAYRINTIs
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class UlkeAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<UlkeAYRINTI>> ara(params Expression<Func<UlkeAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<UlkeAYRINTI>> ara(veri.Varlik vari, params Expression<Func<UlkeAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.UlkeAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.ulkeKimlik)
                   .ToListAsync();
        }



        public static async Task<UlkeAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            UlkeAYRINTI? kayit = await kime.UlkeAYRINTIs.FirstOrDefaultAsync(p => p.ulkeKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static UlkeAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            UlkeAYRINTI? kayit = kime.UlkeAYRINTIs.FirstOrDefault(p => p.ulkeKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

