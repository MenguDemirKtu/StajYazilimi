using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class SozlesmeAYRINTIArama
    {
        public Int32? sozlesmekimlik { get; set; }
        public string? baslik { get; set; }
        public Int32? i_sozlesmeTuruKimlik { get; set; }
        public string? SozlesmeTuruAdi { get; set; }
        public bool? e_gecerliMi { get; set; }
        public string? gecerliMi { get; set; }
        public string? metin { get; set; }
        public bool? varmi { get; set; }
        public SozlesmeAYRINTIArama()
        {
            varmi = true;
        }

        public async Task<List<SozlesmeAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SozlesmeAYRINTI>(P => P.varmi == true);
            if (sozlesmekimlik != null)
                predicate = predicate.And(x => x.sozlesmekimlik == sozlesmekimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik.Contains(baslik));
            if (i_sozlesmeTuruKimlik != null)
                predicate = predicate.And(x => x.i_sozlesmeTuruKimlik == i_sozlesmeTuruKimlik);
            if (SozlesmeTuruAdi != null)
                predicate = predicate.And(x => x.SozlesmeTuruAdi.Contains(SozlesmeTuruAdi));
            if (e_gecerliMi != null)
                predicate = predicate.And(x => x.e_gecerliMi == e_gecerliMi);
            if (gecerliMi != null)
                predicate = predicate.And(x => x.gecerliMi.Contains(gecerliMi));
            if (metin != null)
                predicate = predicate.And(x => x.metin.Contains(metin));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SozlesmeAYRINTI> sonuc = new List<SozlesmeAYRINTI>();
            sonuc = await vari.SozlesmeAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SozlesmeAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SozlesmeAYRINTI> ara(params Predicate<SozlesmeAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SozlesmeAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.sozlesmekimlik).ToList();
            }
        }


        public static List<SozlesmeAYRINTI> tamami(Varlik kime)
        {
            return kime.SozlesmeAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.sozlesmekimlik).ToList();
        }
        public static async Task<SozlesmeAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SozlesmeAYRINTI? kayit = await kime.SozlesmeAYRINTIs.FirstOrDefaultAsync(p => p.sozlesmekimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SozlesmeAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            SozlesmeAYRINTI kayit = kime.SozlesmeAYRINTIs.FirstOrDefault(p => p.sozlesmekimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

