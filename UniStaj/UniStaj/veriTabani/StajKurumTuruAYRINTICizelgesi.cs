using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajKurumTuruAYRINTIArama
    {
        public Int32? stajKurumTurukimlik { get; set; }
        public string? stajKurumTurAdi { get; set; }
        public bool? e_argeFirmasiMi { get; set; }
        public string? argeFirmasiMi { get; set; }
        public bool? varmi { get; set; }
        public StajKurumTuruAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajKurumTuruAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajKurumTuruAYRINTI>(P => P.varmi == true);
            if (stajKurumTurukimlik != null)
                predicate = predicate.And(x => x.stajKurumTurukimlik == stajKurumTurukimlik);
            if (stajKurumTurAdi != null)
                predicate = predicate.And(x => x.stajKurumTurAdi != null && x.stajKurumTurAdi.Contains(stajKurumTurAdi));
            if (e_argeFirmasiMi != null)
                predicate = predicate.And(x => x.e_argeFirmasiMi == e_argeFirmasiMi);
            if (argeFirmasiMi != null)
                predicate = predicate.And(x => x.argeFirmasiMi != null && x.argeFirmasiMi.Contains(argeFirmasiMi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajKurumTuruAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajKurumTuruAYRINTI> sonuc = await vari.StajKurumTuruAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajKurumTuruAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajKurumTuruAYRINTI? sonuc = await vari.StajKurumTuruAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajKurumTuruAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajKurumTuruAYRINTI>> ara(params Expression<Func<StajKurumTuruAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajKurumTuruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajKurumTuruAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajKurumTuruAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajKurumTurukimlik)
                   .ToListAsync();
        }



        public static async Task<StajKurumTuruAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajKurumTuruAYRINTI? kayit = await kime.StajKurumTuruAYRINTIs.FirstOrDefaultAsync(p => p.stajKurumTurukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajKurumTuruAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajKurumTuruAYRINTI? kayit = kime.StajKurumTuruAYRINTIs.FirstOrDefault(p => p.stajKurumTurukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

