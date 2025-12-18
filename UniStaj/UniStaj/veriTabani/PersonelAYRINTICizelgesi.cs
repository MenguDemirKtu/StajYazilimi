using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class PersonelAYRINTIArama
    {
        public Int32? personelKimlik { get; set; }
        public string? sicilNo { get; set; }
        public string? tcKimlikNo { get; set; }
        public string? adi { get; set; }
        public string? soyAdi { get; set; }
        public string? telefon { get; set; }
        public string? ePosta { get; set; }
        public Int64? y_kisiKimlik { get; set; }
        public bool? varmi { get; set; }
        public Int32? i_cinsiyetKimlik { get; set; }
        public string? CinsiyetAdi { get; set; }
        public string? pasaportNo { get; set; }
        public DateTime? dogumTarihi { get; set; }
        public string? anaAdi { get; set; }
        public string? babaAdi { get; set; }
        public PersonelAYRINTIArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<PersonelAYRINTI> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<PersonelAYRINTI>(P => P.varmi == true);
            if (personelKimlik != null)
                predicate = predicate.And(x => x.personelKimlik == personelKimlik);
            if (sicilNo != null)
                predicate = predicate.And(x => x.sicilNo != null && x.sicilNo.Contains(sicilNo));
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (adi != null)
                predicate = predicate.And(x => x.adi != null && x.adi.Contains(adi));
            if (soyAdi != null)
                predicate = predicate.And(x => x.soyAdi != null && x.soyAdi.Contains(soyAdi));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (y_kisiKimlik != null)
                predicate = predicate.And(x => x.y_kisiKimlik == y_kisiKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (i_cinsiyetKimlik != null)
                predicate = predicate.And(x => x.i_cinsiyetKimlik == i_cinsiyetKimlik);
            if (CinsiyetAdi != null)
                predicate = predicate.And(x => x.CinsiyetAdi != null && x.CinsiyetAdi.Contains(CinsiyetAdi));
            if (pasaportNo != null)
                predicate = predicate.And(x => x.pasaportNo != null && x.pasaportNo.Contains(pasaportNo));
            if (dogumTarihi != null)
                predicate = predicate.And(x => x.dogumTarihi == dogumTarihi);
            if (anaAdi != null)
                predicate = predicate.And(x => x.anaAdi != null && x.anaAdi.Contains(anaAdi));
            if (babaAdi != null)
                predicate = predicate.And(x => x.babaAdi != null && x.babaAdi.Contains(babaAdi));
            return predicate;

        }
        public async Task<List<PersonelAYRINTI>> cek(veri.Varlik vari)
        {
            List<PersonelAYRINTI> sonuc = await vari.PersonelAYRINTIs
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<PersonelAYRINTI?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            PersonelAYRINTI? sonuc = await vari.PersonelAYRINTIs
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class PersonelAYRINTICizelgesi
    {





        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<PersonelAYRINTI>> ara(params Expression<Func<PersonelAYRINTI, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<PersonelAYRINTI>> ara(veri.Varlik vari, params Expression<Func<PersonelAYRINTI, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.PersonelAYRINTIs
                            .Where(kosul).OrderByDescending(p => p.personelKimlik)
                   .ToListAsync();
        }



        public static async Task<PersonelAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            PersonelAYRINTI? kayit = await kime.PersonelAYRINTIs.FirstOrDefaultAsync(p => p.personelKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static PersonelAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            PersonelAYRINTI? kayit = kime.PersonelAYRINTIs.FirstOrDefault(p => p.personelKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

