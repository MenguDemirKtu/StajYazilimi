
using LinqKit;
using Microsoft.EntityFrameworkCore; //  
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SifremiUnuttumAYRINTIArama
    {
        public Int32? sifremiUnuttumKimlik { get; set; }
        public string? tcKimlikNo { get; set; }
        public bool? e_smsmi { get; set; }
        public string? ePosta { get; set; }
        public string? telefon { get; set; }
        public DateTime? tarih { get; set; }
        public bool? varmi { get; set; }
        public SifremiUnuttumAYRINTIArama()
        {
            this.varmi = true;
        }

        public async Task<List<SifremiUnuttumAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SifremiUnuttumAYRINTI>(P => P.varmi == true);
            if (sifremiUnuttumKimlik != null)
                predicate = predicate.And(x => x.sifremiUnuttumKimlik == sifremiUnuttumKimlik);
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (e_smsmi != null)
                predicate = predicate.And(x => x.e_smsmi == e_smsmi);
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SifremiUnuttumAYRINTI> sonuc = new List<SifremiUnuttumAYRINTI>();
            sonuc = await vari.SifremiUnuttumAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SifremiUnuttumAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SifremiUnuttumAYRINTI> ara(params Predicate<SifremiUnuttumAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SifremiUnuttumAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.sifremiUnuttumKimlik).ToList();
            }
        }


        public static List<SifremiUnuttumAYRINTI> tamami(Varlik kime)
        {
            return kime.SifremiUnuttumAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.sifremiUnuttumKimlik).ToList();
        }
        public static async Task<SifremiUnuttumAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SifremiUnuttumAYRINTI? kayit = await kime.SifremiUnuttumAYRINTIs.FirstOrDefaultAsync(p => p.sifremiUnuttumKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SifremiUnuttumAYRINTI? tekliCek(Int32 kimlik, Varlik kime)
        {
            SifremiUnuttumAYRINTI? kayit = kime.SifremiUnuttumAYRINTIs.FirstOrDefault(p => p.sifremiUnuttumKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

