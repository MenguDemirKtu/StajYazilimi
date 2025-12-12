using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class YazilimGuvenligiAYRINTIArama
    {
        public Int32? yazilimGuvenligikimlik { get; set; }
        public DateTime? sonKullanimTarihi { get; set; }
        public string? apiKodu { get; set; }
        public string? pythonKodu { get; set; }
        public bool? e_gecerliMi { get; set; }
        public string? gecerliMi { get; set; }

        public bool? varmi
        {
            get; set;
        }
        public YazilimGuvenligiAYRINTIArama()
        {
        }

        public async Task<List<YazilimGuvenligiAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<YazilimGuvenligiAYRINTI>(P => P.varmi == true);
            if (yazilimGuvenligikimlik != null)
                predicate = predicate.And(x => x.yazilimGuvenligikimlik == yazilimGuvenligikimlik);
            if (sonKullanimTarihi != null)
                predicate = predicate.And(x => x.sonKullanimTarihi == sonKullanimTarihi);
            if (apiKodu != null)
                predicate = predicate.And(x => x.apiKodu.Contains(apiKodu));
            if (pythonKodu != null)
                predicate = predicate.And(x => x.pythonKodu.Contains(pythonKodu));
            if (e_gecerliMi != null)
                predicate = predicate.And(x => x.e_gecerliMi == e_gecerliMi);
            if (gecerliMi != null)
                predicate = predicate.And(x => x.gecerliMi.Contains(gecerliMi));
            List<YazilimGuvenligiAYRINTI> sonuc = new List<YazilimGuvenligiAYRINTI>();
            sonuc = await vari.YazilimGuvenligiAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class YazilimGuvenligiAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<YazilimGuvenligiAYRINTI> ara(params Predicate<YazilimGuvenligiAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.YazilimGuvenligiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.yazilimGuvenligikimlik).ToList();
            }
        }


        public static List<YazilimGuvenligiAYRINTI> tamami(Varlik kime)
        {
            return kime.YazilimGuvenligiAYRINTIs.ToList();
        }
        public static async Task<YazilimGuvenligiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            YazilimGuvenligiAYRINTI? kayit = await kime.YazilimGuvenligiAYRINTIs.FirstOrDefaultAsync(p => p.yazilimGuvenligikimlik == kimlik);
            return kayit;
        }




        public static YazilimGuvenligiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            YazilimGuvenligiAYRINTI kayit = kime.YazilimGuvenligiAYRINTIs.FirstOrDefault(p => p.yazilimGuvenligikimlik == kimlik);
            return kayit;
        }
    }
}

