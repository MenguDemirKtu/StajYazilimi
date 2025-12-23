using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBirimAsamasiAYRINTIArama
    {
        public Int32? stajBirimAsamasikimlik { get; set; }
        public Int32? i_stajBirimiKimlik { get; set; }
        public string? stajBirimAdi { get; set; }
        public Int32? i_stajAsamasiKimlik { get; set; }
        public string? StajAsamasiAdi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? e_gecerliMi { get; set; }
        public string? gecerliMi { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public StajBirimAsamasiAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBirimAsamasiAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBirimAsamasiAYRINTI>(P => P.varmi == true);
            if (stajBirimAsamasikimlik != null)
                predicate = predicate.And(x => x.stajBirimAsamasikimlik == stajBirimAsamasikimlik);
            if (i_stajBirimiKimlik != null)
                predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik);
            if (stajBirimAdi != null)
                predicate = predicate.And(x => x.stajBirimAdi != null && x.stajBirimAdi.Contains(stajBirimAdi));
            if (i_stajAsamasiKimlik != null)
                predicate = predicate.And(x => x.i_stajAsamasiKimlik == i_stajAsamasiKimlik);
            if (StajAsamasiAdi != null)
                predicate = predicate.And(x => x.StajAsamasiAdi != null && x.StajAsamasiAdi.Contains(StajAsamasiAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (e_gecerliMi != null)
                predicate = predicate.And(x => x.e_gecerliMi == e_gecerliMi);
            if (gecerliMi != null)
                predicate = predicate.And(x => x.gecerliMi != null && x.gecerliMi.Contains(gecerliMi));
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajBirimAsamasiAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajBirimAsamasiAYRINTI> sonuc = await vari.StajBirimAsamasiAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBirimAsamasiAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBirimAsamasiAYRINTI? sonuc = await vari.StajBirimAsamasiAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBirimAsamasiAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBirimAsamasiAYRINTI>> ara(params Expression<Func<StajBirimAsamasiAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBirimAsamasiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimAsamasiAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBirimAsamasiAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajBirimAsamasikimlik)
                   .ToListAsync();
        }



        public static async Task<StajBirimAsamasiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBirimAsamasiAYRINTI? kayit = await kime.StajBirimAsamasiAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimAsamasikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajBirimAsamasiAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBirimAsamasiAYRINTI? kayit = kime.StajBirimAsamasiAYRINTIs.FirstOrDefault(p => p.stajBirimAsamasikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

