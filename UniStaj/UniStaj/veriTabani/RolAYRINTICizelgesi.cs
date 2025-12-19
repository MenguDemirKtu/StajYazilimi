using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class RolAYRINTIArama
    {
        public Int32? rolKimlik { get; set; }
        public string? rolAdi { get; set; }
        public string? tanitim { get; set; }
        public bool? e_gecerlimi { get; set; }
        public string? gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public bool? e_varsayilanmi { get; set; }
        public Int32? i_varsayilanOlduguKullaniciTuruKimlik { get; set; }
        public bool? e_rolIslemiIcinmi { get; set; }
        public Int32? i_rolIslemiKimlik { get; set; }
        public string? kodu { get; set; }
        public RolAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<RolAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<RolAYRINTI>(P => P.varmi == true);
            if (rolKimlik != null)
                predicate = predicate.And(x => x.rolKimlik == rolKimlik);
            if (rolAdi != null)
                predicate = predicate.And(x => x.rolAdi != null && x.rolAdi.Contains(rolAdi));
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim != null && x.tanitim.Contains(tanitim));
            if (e_gecerlimi != null)
                predicate = predicate.And(x => x.e_gecerlimi == e_gecerlimi);
            if (gecerlimi != null)
                predicate = predicate.And(x => x.gecerlimi != null && x.gecerlimi.Contains(gecerlimi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (e_varsayilanmi != null)
                predicate = predicate.And(x => x.e_varsayilanmi == e_varsayilanmi);
            if (i_varsayilanOlduguKullaniciTuruKimlik != null)
                predicate = predicate.And(x => x.i_varsayilanOlduguKullaniciTuruKimlik == i_varsayilanOlduguKullaniciTuruKimlik);
            if (e_rolIslemiIcinmi != null)
                predicate = predicate.And(x => x.e_rolIslemiIcinmi == e_rolIslemiIcinmi);
            if (i_rolIslemiKimlik != null)
                predicate = predicate.And(x => x.i_rolIslemiKimlik == i_rolIslemiKimlik);
            if (kodu != null)
                predicate = predicate.And(x => x.kodu != null && x.kodu.Contains(kodu));
            return predicate;

        }
        public async Task<List<RolAYRINTI>> cek(veri.Varlik vari)
        {
            List<RolAYRINTI> sonuc = await vari.RolAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<RolAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            RolAYRINTI? sonuc = await vari.RolAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class RolAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<RolAYRINTI>> ara(params Expression<Func<RolAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<RolAYRINTI>> ara(veri.Varlik vari, params Expression<Func<RolAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.RolAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.rolKimlik)
                   .ToListAsync();
        }



        public static async Task<RolAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            RolAYRINTI? kayit = await kime.RolAYRINTIs.FirstOrDefaultAsync(p => p.rolKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static RolAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            RolAYRINTI? kayit = kime.RolAYRINTIs.FirstOrDefault(p => p.rolKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

