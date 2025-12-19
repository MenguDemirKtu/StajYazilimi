using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajKurumuAYRINTIArama
    {
        public Int32? stajKurumukimlik { get; set; }
        public string? stajKurumAdi { get; set; }
        public Int32? i_stajkurumturukimlik { get; set; }
        public string? stajKurumTurAdi { get; set; }
        public string? hizmetAlani { get; set; }
        public string? webAdresi { get; set; }
        public string? vergiNo { get; set; }
        public string? ibanNo { get; set; }
        public Int32? calisanSayisi { get; set; }
        public string? adresi { get; set; }
        public string? telNo { get; set; }
        public string? ePosta { get; set; }
        public string? faks { get; set; }
        public bool? e_karaListedeMi { get; set; }
        public string? karaListedeMi { get; set; }
        public bool? varmi { get; set; }
        public StajKurumuAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajKurumuAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajKurumuAYRINTI>(P => P.varmi == true);
            if (stajKurumukimlik != null)
                predicate = predicate.And(x => x.stajKurumukimlik == stajKurumukimlik);
            if (stajKurumAdi != null)
                predicate = predicate.And(x => x.stajKurumAdi != null && x.stajKurumAdi.Contains(stajKurumAdi));
            if (i_stajkurumturukimlik != null)
                predicate = predicate.And(x => x.i_stajkurumturukimlik == i_stajkurumturukimlik);
            if (stajKurumTurAdi != null)
                predicate = predicate.And(x => x.stajKurumTurAdi != null && x.stajKurumTurAdi.Contains(stajKurumTurAdi));
            if (hizmetAlani != null)
                predicate = predicate.And(x => x.hizmetAlani != null && x.hizmetAlani.Contains(hizmetAlani));
            if (webAdresi != null)
                predicate = predicate.And(x => x.webAdresi != null && x.webAdresi.Contains(webAdresi));
            if (vergiNo != null)
                predicate = predicate.And(x => x.vergiNo != null && x.vergiNo.Contains(vergiNo));
            if (ibanNo != null)
                predicate = predicate.And(x => x.ibanNo != null && x.ibanNo.Contains(ibanNo));
            if (calisanSayisi != null)
                predicate = predicate.And(x => x.calisanSayisi == calisanSayisi);
            if (adresi != null)
                predicate = predicate.And(x => x.adresi != null && x.adresi.Contains(adresi));
            if (telNo != null)
                predicate = predicate.And(x => x.telNo != null && x.telNo.Contains(telNo));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (faks != null)
                predicate = predicate.And(x => x.faks != null && x.faks.Contains(faks));
            if (e_karaListedeMi != null)
                predicate = predicate.And(x => x.e_karaListedeMi == e_karaListedeMi);
            if (karaListedeMi != null)
                predicate = predicate.And(x => x.karaListedeMi != null && x.karaListedeMi.Contains(karaListedeMi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajKurumuAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajKurumuAYRINTI> sonuc = await vari.StajKurumuAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajKurumuAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajKurumuAYRINTI? sonuc = await vari.StajKurumuAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajKurumuAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajKurumuAYRINTI>> ara(params Expression<Func<StajKurumuAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajKurumuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajKurumuAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajKurumuAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajKurumukimlik)
                   .ToListAsync();
        }



        public static async Task<StajKurumuAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajKurumuAYRINTI? kayit = await kime.StajKurumuAYRINTIs.FirstOrDefaultAsync(p => p.stajKurumukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajKurumuAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajKurumuAYRINTI? kayit = kime.StajKurumuAYRINTIs.FirstOrDefault(p => p.stajKurumukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

