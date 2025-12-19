using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBirimiTurleriAYRINTIArama
    {
        public Int32? stajBirimiTurlerikimlik { get; set; }
        public Int32? i_stajBirimiKimlik { get; set; }
        public string? stajBirimAdi { get; set; }
        public Int32? i_stajTuruKimlik { get; set; }
        public string? stajTuruAdi { get; set; }
        public Int32? gunu { get; set; }
        public string? siniflari { get; set; }
        public string? ekAciklama { get; set; }
        public bool? varmi { get; set; }
        public StajBirimiTurleriAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBirimiTurleriAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBirimiTurleriAYRINTI>(P => P.varmi == true);
            if (stajBirimiTurlerikimlik != null)
                predicate = predicate.And(x => x.stajBirimiTurlerikimlik == stajBirimiTurlerikimlik);
            if (i_stajBirimiKimlik != null)
                predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik);
            if (stajBirimAdi != null)
                predicate = predicate.And(x => x.stajBirimAdi != null && x.stajBirimAdi.Contains(stajBirimAdi));
            if (i_stajTuruKimlik != null)
                predicate = predicate.And(x => x.i_stajTuruKimlik == i_stajTuruKimlik);
            if (stajTuruAdi != null)
                predicate = predicate.And(x => x.stajTuruAdi != null && x.stajTuruAdi.Contains(stajTuruAdi));
            if (gunu != null)
                predicate = predicate.And(x => x.gunu == gunu);
            if (siniflari != null)
                predicate = predicate.And(x => x.siniflari != null && x.siniflari.Contains(siniflari));
            if (ekAciklama != null)
                predicate = predicate.And(x => x.ekAciklama != null && x.ekAciklama.Contains(ekAciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajBirimiTurleriAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajBirimiTurleriAYRINTI> sonuc = await vari.StajBirimiTurleriAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBirimiTurleriAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBirimiTurleriAYRINTI? sonuc = await vari.StajBirimiTurleriAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBirimiTurleriAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBirimiTurleriAYRINTI>> ara(params Expression<Func<StajBirimiTurleriAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBirimiTurleriAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimiTurleriAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBirimiTurleriAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajBirimiTurlerikimlik)
                   .ToListAsync();
        }



        public static async Task<StajBirimiTurleriAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBirimiTurleriAYRINTI? kayit = await kime.StajBirimiTurleriAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimiTurlerikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajBirimiTurleriAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBirimiTurleriAYRINTI? kayit = kime.StajBirimiTurleriAYRINTIs.FirstOrDefault(p => p.stajBirimiTurlerikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

