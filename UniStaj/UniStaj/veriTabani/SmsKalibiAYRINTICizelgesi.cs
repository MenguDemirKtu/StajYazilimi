using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

// ;

namespace UniStaj.veriTabani
{
    public class SmsKalibiAYRINTIArama
    {
        public Int32? smsKalibikimlik { get; set; }
        public string? baslik { get; set; }
        public Int32? i_epostaturukimlik { get; set; }
        public string? kalip { get; set; }
        public bool? e_gecerlimi { get; set; }
        public string? gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public SmsKalibiAYRINTIArama()
        {
            varmi = true;
        }

        public async Task<List<SmsKalibiAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SmsKalibiAYRINTI>(P => P.varmi == true);
            if (smsKalibikimlik != null)
                predicate = predicate.And(x => x.smsKalibikimlik == smsKalibikimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik.Contains(baslik));
            if (i_epostaturukimlik != null)
                predicate = predicate.And(x => x.i_epostaturukimlik == i_epostaturukimlik);
            if (kalip != null)
                predicate = predicate.And(x => x.kalip.Contains(kalip));
            if (e_gecerlimi != null)
                predicate = predicate.And(x => x.e_gecerlimi == e_gecerlimi);
            if (gecerlimi != null)
                predicate = predicate.And(x => x.gecerlimi.Contains(gecerlimi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SmsKalibiAYRINTI> sonuc = new List<SmsKalibiAYRINTI>();
            sonuc = await vari.SmsKalibiAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SmsKalibiAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SmsKalibiAYRINTI> ara(params Predicate<SmsKalibiAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SmsKalibiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.smsKalibikimlik).ToList();
            }
        }


        public static List<SmsKalibiAYRINTI> tamami(Varlik kime)
        {
            return kime.SmsKalibiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.smsKalibikimlik).ToList();
        }
        public static async Task<SmsKalibiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SmsKalibiAYRINTI? kayit = await kime.SmsKalibiAYRINTIs.FirstOrDefaultAsync(p => p.smsKalibikimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SmsKalibiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            SmsKalibiAYRINTI kayit = kime.SmsKalibiAYRINTIs.FirstOrDefault(p => p.smsKalibikimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

