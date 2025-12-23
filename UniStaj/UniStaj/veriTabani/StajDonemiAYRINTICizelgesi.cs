using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajDonemiAYRINTIArama
    {
        public Int32? stajDonemikimlik { get; set; }
        public string? stajDonemAdi { get; set; }
        public string? tanim { get; set; }
        public DateTime? baslangic { get; set; }
        public DateTime? bitis { get; set; }
        public bool? e_gecerliDonemmi { get; set; }
        public string? gecerliDonemmi { get; set; }
        public bool? varmi { get; set; }
        public StajDonemiAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajDonemiAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajDonemiAYRINTI>(P => P.varmi == true);
            if (stajDonemikimlik != null)
                predicate = predicate.And(x => x.stajDonemikimlik == stajDonemikimlik);
            if (stajDonemAdi != null)
                predicate = predicate.And(x => x.stajDonemAdi != null && x.stajDonemAdi.Contains(stajDonemAdi));
            if (tanim != null)
                predicate = predicate.And(x => x.tanim != null && x.tanim.Contains(tanim));
            if (baslangic != null)
                predicate = predicate.And(x => x.baslangic == baslangic);
            if (bitis != null)
                predicate = predicate.And(x => x.bitis == bitis);
            if (e_gecerliDonemmi != null)
                predicate = predicate.And(x => x.e_gecerliDonemmi == e_gecerliDonemmi);
            if (gecerliDonemmi != null)
                predicate = predicate.And(x => x.gecerliDonemmi != null && x.gecerliDonemmi.Contains(gecerliDonemmi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajDonemiAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajDonemiAYRINTI> sonuc = await vari.StajDonemiAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajDonemiAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajDonemiAYRINTI? sonuc = await vari.StajDonemiAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajDonemiAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajDonemiAYRINTI>> ara(params Expression<Func<StajDonemiAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajDonemiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajDonemiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajDonemiAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajDonemikimlik)
                   .ToListAsync();
        }



        public static async Task<StajDonemiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajDonemiAYRINTI? kayit = await kime.StajDonemiAYRINTIs.FirstOrDefaultAsync(p => p.stajDonemikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajDonemiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajDonemiAYRINTI? kayit = kime.StajDonemiAYRINTIs.FirstOrDefault(p => p.stajDonemikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

