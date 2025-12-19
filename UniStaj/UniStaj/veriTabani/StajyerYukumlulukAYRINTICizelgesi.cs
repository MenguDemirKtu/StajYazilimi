using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajyerYukumlulukAYRINTIArama
    {
        public Int32? stajyerYukumlulukkimlik { get; set; }
        public Int32? i_stajyerKimlik { get; set; }
        public string? ogrenciNo { get; set; }
        public Int32? i_stajTuruKimlik { get; set; }
        public string? stajTuruAdi { get; set; }
        public Int32? gunSayisi { get; set; }
        public string? siniflar { get; set; }
        public Int32? yaptigiGunSayis { get; set; }
        public Int32? kabulEdilenGunSayisi { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public StajyerYukumlulukAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajyerYukumlulukAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajyerYukumlulukAYRINTI>(P => P.varmi == true);
            if (stajyerYukumlulukkimlik != null)
                predicate = predicate.And(x => x.stajyerYukumlulukkimlik == stajyerYukumlulukkimlik);
            if (i_stajyerKimlik != null)
                predicate = predicate.And(x => x.i_stajyerKimlik == i_stajyerKimlik);
            if (ogrenciNo != null)
                predicate = predicate.And(x => x.ogrenciNo != null && x.ogrenciNo.Contains(ogrenciNo));
            if (i_stajTuruKimlik != null)
                predicate = predicate.And(x => x.i_stajTuruKimlik == i_stajTuruKimlik);
            if (stajTuruAdi != null)
                predicate = predicate.And(x => x.stajTuruAdi != null && x.stajTuruAdi.Contains(stajTuruAdi));
            if (gunSayisi != null)
                predicate = predicate.And(x => x.gunSayisi == gunSayisi);
            if (siniflar != null)
                predicate = predicate.And(x => x.siniflar != null && x.siniflar.Contains(siniflar));
            if (yaptigiGunSayis != null)
                predicate = predicate.And(x => x.yaptigiGunSayis == yaptigiGunSayis);
            if (kabulEdilenGunSayisi != null)
                predicate = predicate.And(x => x.kabulEdilenGunSayisi == kabulEdilenGunSayisi);
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajyerYukumlulukAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajyerYukumlulukAYRINTI> sonuc = await vari.StajyerYukumlulukAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajyerYukumlulukAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajyerYukumlulukAYRINTI? sonuc = await vari.StajyerYukumlulukAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajyerYukumlulukAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajyerYukumlulukAYRINTI>> ara(params Expression<Func<StajyerYukumlulukAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajyerYukumlulukAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajyerYukumlulukAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajyerYukumlulukAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajyerYukumlulukkimlik)
                   .ToListAsync();
        }



        public static async Task<StajyerYukumlulukAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajyerYukumlulukAYRINTI? kayit = await kime.StajyerYukumlulukAYRINTIs.FirstOrDefaultAsync(p => p.stajyerYukumlulukkimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajyerYukumlulukAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajyerYukumlulukAYRINTI? kayit = kime.StajyerYukumlulukAYRINTIs.FirstOrDefault(p => p.stajyerYukumlulukkimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

