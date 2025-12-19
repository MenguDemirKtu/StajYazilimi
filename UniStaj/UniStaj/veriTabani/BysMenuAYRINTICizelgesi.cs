using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class BysMenuAYRINTIArama
    {
        public Int32? bysMenuKimlik { get; set; }
        public string? bysMenuAdi { get; set; }
        public string? bysMenuBicim { get; set; }
        public Int32? i_ustMenuKimlik { get; set; }
        public Int32? i_webSayfasiKimlik { get; set; }
        public string? bysMenuUrl { get; set; }
        public bool? varmi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? e_modulSayfasimi { get; set; }
        public Int32? i_modulKimlik { get; set; }
        public bool? e_gosterilsimmi { get; set; }
        public bool? e_anaKullaniciGorsunmu { get; set; }
        public BysMenuAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<BysMenuAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<BysMenuAYRINTI>(P => P.varmi == true);
            if (bysMenuKimlik != null)
                predicate = predicate.And(x => x.bysMenuKimlik == bysMenuKimlik);
            if (bysMenuAdi != null)
                predicate = predicate.And(x => x.bysMenuAdi != null && x.bysMenuAdi.Contains(bysMenuAdi));
            if (bysMenuBicim != null)
                predicate = predicate.And(x => x.bysMenuBicim != null && x.bysMenuBicim.Contains(bysMenuBicim));
            if (i_ustMenuKimlik != null)
                predicate = predicate.And(x => x.i_ustMenuKimlik == i_ustMenuKimlik);
            if (i_webSayfasiKimlik != null)
                predicate = predicate.And(x => x.i_webSayfasiKimlik == i_webSayfasiKimlik);
            if (bysMenuUrl != null)
                predicate = predicate.And(x => x.bysMenuUrl != null && x.bysMenuUrl.Contains(bysMenuUrl));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (e_modulSayfasimi != null)
                predicate = predicate.And(x => x.e_modulSayfasimi == e_modulSayfasimi);
            if (i_modulKimlik != null)
                predicate = predicate.And(x => x.i_modulKimlik == i_modulKimlik);
            if (e_gosterilsimmi != null)
                predicate = predicate.And(x => x.e_gosterilsimmi == e_gosterilsimmi);
            if (e_anaKullaniciGorsunmu != null)
                predicate = predicate.And(x => x.e_anaKullaniciGorsunmu == e_anaKullaniciGorsunmu);
            List<BysMenuAYRINTI> sonuc = new List<BysMenuAYRINTI>();
            sonuc = await vari.BysMenuAYRINTIs
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class BysMenuAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<BysMenuAYRINTI>> ara(params Expression<Func<BysMenuAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<BysMenuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<BysMenuAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.BysMenuAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.bysMenuKimlik)
                   .ToListAsync();
        }



        public static async Task<BysMenuAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            BysMenuAYRINTI? kayit = await kime.BysMenuAYRINTIs.FirstOrDefaultAsync(p => p.bysMenuKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static BysMenuAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            BysMenuAYRINTI? kayit = kime.BysMenuAYRINTIs.FirstOrDefault(p => p.bysMenuKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

