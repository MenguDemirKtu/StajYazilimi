using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class SifreDegisikligiAYRINTIArama
    {
        public Int32? sifreDegisikligiKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public string? kullaniciAdi { get; set; }
        public string? eskiSifre { get; set; }
        public string? yeniSifre { get; set; }
        public bool? varmi { get; set; }
        public SifreDegisikligiAYRINTIArama()
        {
            varmi = true;
        }

        public async Task<List<SifreDegisikligiAYRINTI>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SifreDegisikligiAYRINTI>(P => P.varmi == true);
            if (sifreDegisikligiKimlik != null)
                predicate = predicate.And(x => x.sifreDegisikligiKimlik == sifreDegisikligiKimlik);
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (i_kullaniciKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciKimlik == i_kullaniciKimlik);
            if (kullaniciAdi != null)
                predicate = predicate.And(x => x.kullaniciAdi.Contains(kullaniciAdi));
            if (eskiSifre != null)
                predicate = predicate.And(x => x.eskiSifre.Contains(eskiSifre));
            if (yeniSifre != null)
                predicate = predicate.And(x => x.yeniSifre.Contains(yeniSifre));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SifreDegisikligiAYRINTI> sonuc = new List<SifreDegisikligiAYRINTI>();
            sonuc = await vari.SifreDegisikligiAYRINTIs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SifreDegisikligiAYRINTICizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SifreDegisikligiAYRINTI> ara(params Predicate<SifreDegisikligiAYRINTI>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SifreDegisikligiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.sifreDegisikligiKimlik).ToList();
            }
        }


        public static List<SifreDegisikligiAYRINTI> tamami(Varlik kime)
        {
            return kime.SifreDegisikligiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.sifreDegisikligiKimlik).ToList();
        }
        public static async Task<SifreDegisikligiAYRINTI?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SifreDegisikligiAYRINTI? kayit = await kime.SifreDegisikligiAYRINTIs.FirstOrDefaultAsync(p => p.sifreDegisikligiKimlik == kimlik && p.varmi == true);
            return kayit;
        }




        public static SifreDegisikligiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            SifreDegisikligiAYRINTI kayit = kime.SifreDegisikligiAYRINTIs.FirstOrDefault(p => p.sifreDegisikligiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}

