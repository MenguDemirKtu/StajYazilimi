using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajyerAYRINTIArama
    {
        public Int32? stajyerkimlik { get; set; }
        public string? ogrenciNo { get; set; }
        public string? tcKimlikNo { get; set; }
        public Int32? i_stajBirimiKimlik { get; set; }
        public string? stajBirimAdi { get; set; }
        public string? stajyerAdi { get; set; }
        public string? stajyerSoyadi { get; set; }
        public Int32? sinifi { get; set; }
        public string? telefon { get; set; }
        public string? ePosta { get; set; }
        public bool? varmi { get; set; }
        public StajyerAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajyerAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajyerAYRINTI>(P => P.varmi == true);
            if (stajyerkimlik != null)
                predicate = predicate.And(x => x.stajyerkimlik == stajyerkimlik);
            if (ogrenciNo != null)
                predicate = predicate.And(x => x.ogrenciNo != null && x.ogrenciNo.Contains(ogrenciNo));
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (i_stajBirimiKimlik != null)
                predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik);
            if (stajBirimAdi != null)
                predicate = predicate.And(x => x.stajBirimAdi != null && x.stajBirimAdi.Contains(stajBirimAdi));
            if (stajyerAdi != null)
                predicate = predicate.And(x => x.stajyerAdi != null && x.stajyerAdi.Contains(stajyerAdi));
            if (stajyerSoyadi != null)
                predicate = predicate.And(x => x.stajyerSoyadi != null && x.stajyerSoyadi.Contains(stajyerSoyadi));
            if (sinifi != null)
                predicate = predicate.And(x => x.sinifi == sinifi);
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajyerAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajyerAYRINTI> sonuc = await vari.StajyerAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajyerAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajyerAYRINTI? sonuc = await vari.StajyerAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajyerAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajyerAYRINTI>> ara(params Expression<Func<StajyerAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajyerAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajyerAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajyerAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajyerkimlik)
                   .ToListAsync();
        }



        public static async Task<StajyerAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajyerAYRINTI? kayit = await kime.StajyerAYRINTIs.FirstOrDefaultAsync(p => p.stajyerkimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajyerAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajyerAYRINTI? kayit = kime.StajyerAYRINTIs.FirstOrDefault(p => p.stajyerkimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

