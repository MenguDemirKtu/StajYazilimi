using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBasvurusuAYRINTIArama
    {
        public Int32? stajBasvurusukimlik { get; set; }
        public Int32? i_stajyerKimlik { get; set; }
        public string? ogrenciNo { get; set; }
        public bool? varmi { get; set; }
        public string? tcKimlikNo { get; set; }
        public Int32? i_stajBirimiKimlik { get; set; }
        public string? stajyerAdi { get; set; }
        public string? stajyerSoyadi { get; set; }
        public string? stajKurumAdi { get; set; }
        public Int32? i_stajkurumturukimlik { get; set; }
        public string? hizmetAlani { get; set; }
        public string? vergiNo { get; set; }
        public string? ibanNo { get; set; }
        public Int32? calisanSayisi { get; set; }
        public string? adresi { get; set; }
        public string? ePosta { get; set; }
        public string? telNo { get; set; }
        public string? faks { get; set; }
        public string? webAdresi { get; set; }
        public string? yetkilisi { get; set; }
        public string? yetkiliGorevi { get; set; }
        public string? yetkiliTel { get; set; }
        public Int32? i_stajTuruKimlik { get; set; }
        public DateTime? baslangic { get; set; }
        public DateTime? bitis { get; set; }
        public Int32? gunSayisi { get; set; }
        public Int32? sinifi { get; set; }
        public string? calismaGunleri { get; set; }
        public string? kodu { get; set; }
        public string? stajTuruAdi { get; set; }
        public StajBasvurusuAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBasvurusuAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBasvurusuAYRINTI>(P => P.varmi == true);
            if (stajBasvurusukimlik != null)
                predicate = predicate.And(x => x.stajBasvurusukimlik == stajBasvurusukimlik);
            if (i_stajyerKimlik != null)
                predicate = predicate.And(x => x.i_stajyerKimlik == i_stajyerKimlik);
            if (ogrenciNo != null)
                predicate = predicate.And(x => x.ogrenciNo != null && x.ogrenciNo.Contains(ogrenciNo));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (i_stajBirimiKimlik != null)
                predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik);
            if (stajyerAdi != null)
                predicate = predicate.And(x => x.stajyerAdi != null && x.stajyerAdi.Contains(stajyerAdi));
            if (stajyerSoyadi != null)
                predicate = predicate.And(x => x.stajyerSoyadi != null && x.stajyerSoyadi.Contains(stajyerSoyadi));
            if (stajKurumAdi != null)
                predicate = predicate.And(x => x.stajKurumAdi != null && x.stajKurumAdi.Contains(stajKurumAdi));
            if (i_stajkurumturukimlik != null)
                predicate = predicate.And(x => x.i_stajkurumturukimlik == i_stajkurumturukimlik);
            if (hizmetAlani != null)
                predicate = predicate.And(x => x.hizmetAlani != null && x.hizmetAlani.Contains(hizmetAlani));
            if (vergiNo != null)
                predicate = predicate.And(x => x.vergiNo != null && x.vergiNo.Contains(vergiNo));
            if (ibanNo != null)
                predicate = predicate.And(x => x.ibanNo != null && x.ibanNo.Contains(ibanNo));
            if (calisanSayisi != null)
                predicate = predicate.And(x => x.calisanSayisi == calisanSayisi);
            if (adresi != null)
                predicate = predicate.And(x => x.adresi != null && x.adresi.Contains(adresi));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (telNo != null)
                predicate = predicate.And(x => x.telNo != null && x.telNo.Contains(telNo));
            if (faks != null)
                predicate = predicate.And(x => x.faks != null && x.faks.Contains(faks));
            if (webAdresi != null)
                predicate = predicate.And(x => x.webAdresi != null && x.webAdresi.Contains(webAdresi));
            if (yetkilisi != null)
                predicate = predicate.And(x => x.yetkilisi != null && x.yetkilisi.Contains(yetkilisi));
            if (yetkiliGorevi != null)
                predicate = predicate.And(x => x.yetkiliGorevi != null && x.yetkiliGorevi.Contains(yetkiliGorevi));
            if (yetkiliTel != null)
                predicate = predicate.And(x => x.yetkiliTel != null && x.yetkiliTel.Contains(yetkiliTel));
            if (i_stajTuruKimlik != null)
                predicate = predicate.And(x => x.i_stajTuruKimlik == i_stajTuruKimlik);
            if (baslangic != null)
                predicate = predicate.And(x => x.baslangic == baslangic);
            if (bitis != null)
                predicate = predicate.And(x => x.bitis == bitis);
            if (gunSayisi != null)
                predicate = predicate.And(x => x.gunSayisi == gunSayisi);
            if (sinifi != null)
                predicate = predicate.And(x => x.sinifi == sinifi);
            if (calismaGunleri != null)
                predicate = predicate.And(x => x.calismaGunleri != null && x.calismaGunleri.Contains(calismaGunleri));
            if (kodu != null)
                predicate = predicate.And(x => x.kodu != null && x.kodu.Contains(kodu));
            if (stajTuruAdi != null)
                predicate = predicate.And(x => x.stajTuruAdi != null && x.stajTuruAdi.Contains(stajTuruAdi));
            return predicate;

        }
        public async Task<List<StajBasvurusuAYRINTI>> cek(veri.Varlik vari)
        {
            List<StajBasvurusuAYRINTI> sonuc = await vari.StajBasvurusuAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBasvurusuAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBasvurusuAYRINTI? sonuc = await vari.StajBasvurusuAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBasvurusuAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBasvurusuAYRINTI>> ara(params Expression<Func<StajBasvurusuAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBasvurusuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBasvurusuAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBasvurusuAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.stajBasvurusukimlik)
                   .ToListAsync();
        }



        public static async Task<StajBasvurusuAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBasvurusuAYRINTI? kayit = await kime.StajBasvurusuAYRINTIs.FirstOrDefaultAsync(p => p.stajBasvurusukimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static StajBasvurusuAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBasvurusuAYRINTI? kayit = kime.StajBasvurusuAYRINTIs.FirstOrDefault(p => p.stajBasvurusukimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

