using LinqKit;
using Microsoft.EntityFrameworkCore; // 
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
            varmi = true;
        }

        public async Task<List<RolAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<RolAYRINTI>(P => P.varmi == true);
            if (rolKimlik != null)
                predicate = predicate.And(x => x.rolKimlik == rolKimlik);
            if (rolAdi != null)
                predicate = predicate.And(x => x.rolAdi.Contains(rolAdi));
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim.Contains(tanitim));
            if (e_gecerlimi != null)
                predicate = predicate.And(x => x.e_gecerlimi == e_gecerlimi);
            if (gecerlimi != null)
                predicate = predicate.And(x => x.gecerlimi.Contains(gecerlimi));
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
                predicate = predicate.And(x => x.kodu.Contains(kodu));
            List<RolAYRINTI> sonuc = new List<RolAYRINTI>();
            sonuc = await vari.RolAYRINTIs
            .Where(predicate)
            .ToListAsync();
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
        public static List<RolAYRINTI> ara(params Predicate<RolAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.RolAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.rolKimlik).ToList();
            }
        }


        public static List<RolAYRINTI> tamami(Varlik kime)
        {
            return kime.RolAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.rolKimlik).ToList();
        }
        public static async Task<RolAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            RolAYRINTI? kayit = await kime.RolAYRINTIs.FirstOrDefaultAsync(p => p.rolKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static RolAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            RolAYRINTI kayit = kime.RolAYRINTIs.FirstOrDefault(p => p.rolKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

