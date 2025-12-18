using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBirimYetkilisiAYRINTIArama
    {
        public Int32? stajBirimYetkilisikimlik { get; set; }
        public string? tcKimlikNo { get; set; }
        public string? ad { get; set; }
        public string? soyad { get; set; }
        public string? telefon { get; set; }
        public string? ePosta { get; set; }
        public bool? varmi { get; set; }
        public StajBirimYetkilisiAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBirimYetkilisiAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBirimYetkilisiAYRINTI>(P => P.varmi == true);
            if (stajBirimYetkilisikimlik != null)
                predicate = predicate.And(x => x.stajBirimYetkilisikimlik == stajBirimYetkilisikimlik);
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (ad != null)
                predicate = predicate.And(x => x.ad != null && x.ad.Contains(ad));
            if (soyad != null)
                predicate = predicate.And(x => x.soyad != null && x.soyad.Contains(soyad));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajBirimYetkilisiAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajBirimYetkilisiAYRINTI> sonuc = await vari.StajBirimYetkilisiAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBirimYetkilisiAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBirimYetkilisiAYRINTI? sonuc = await vari.StajBirimYetkilisiAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBirimYetkilisiAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBirimYetkilisiAYRINTI>> ara(params Expression<Func<StajBirimYetkilisiAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBirimYetkilisiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimYetkilisiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBirimYetkilisiAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajBirimYetkilisikimlik)
                   .ToListAsync();
        }



        public static async Task<StajBirimYetkilisiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBirimYetkilisiAYRINTI? kayit = await kime.StajBirimYetkilisiAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimYetkilisikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajBirimYetkilisiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBirimYetkilisiAYRINTI? kayit = kime.StajBirimYetkilisiAYRINTIs.FirstOrDefault(p => p.stajBirimYetkilisikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

