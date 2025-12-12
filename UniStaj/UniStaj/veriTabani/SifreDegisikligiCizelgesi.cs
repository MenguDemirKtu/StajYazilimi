using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class SifreDegisikligiArama
    {
        public Int32? sifreDegisikligiKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public string? eskiSifre { get; set; }
        public string? yeniSifre { get; set; }
        public bool? varmi { get; set; }
        public SifreDegisikligiArama()
        {
            varmi = true;
        }

        public async Task<List<SifreDegisikligi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SifreDegisikligi>(P => P.varmi == true);
            if (sifreDegisikligiKimlik != null)
                predicate = predicate.And(x => x.sifreDegisikligiKimlik == sifreDegisikligiKimlik);
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (i_kullaniciKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciKimlik == i_kullaniciKimlik);
            if (eskiSifre != null)
                predicate = predicate.And(x => x.eskiSifre.Contains(eskiSifre));
            if (yeniSifre != null)
                predicate = predicate.And(x => x.yeniSifre.Contains(yeniSifre));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SifreDegisikligi> sonuc = new List<SifreDegisikligi>();
            sonuc = await vari.SifreDegisikligis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SifreDegisikligiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SifreDegisikligi> ara(params Predicate<SifreDegisikligi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SifreDegisikligis.ToList().FindAll(kosul).OrderByDescending(p => p.sifreDegisikligiKimlik).ToList();
            }
        }


        public static List<SifreDegisikligi> tamami(Varlik kime)
        {
            return kime.SifreDegisikligis.Where(p => p.varmi == true).OrderByDescending(p => p.sifreDegisikligiKimlik).ToList();
        }
        public static async Task<SifreDegisikligi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SifreDegisikligi? kayit = await kime.SifreDegisikligis.FirstOrDefaultAsync(p => p.sifreDegisikligiKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(SifreDegisikligi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.sifreDegisikligiKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.SifreDegisikligis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                SifreDegisikligi? bulunan = await vari.SifreDegisikligis.FirstOrDefaultAsync(p => p.sifreDegisikligiKimlik == yeni.sifreDegisikligiKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(SifreDegisikligi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.SifreDegisikligis.FirstOrDefaultAsync(p => p.sifreDegisikligiKimlik == kimi.sifreDegisikligiKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static SifreDegisikligi tekliCek(Int32 kimlik, Varlik kime)
        {
            SifreDegisikligi kayit = kime.SifreDegisikligis.FirstOrDefault(p => p.sifreDegisikligiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(SifreDegisikligi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sifreDegisikligiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SifreDegisikligis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.SifreDegisikligis.FirstOrDefault(p => p.sifreDegisikligiKimlik == yeni.sifreDegisikligiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(SifreDegisikligi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SifreDegisikligis.FirstOrDefault(p => p.sifreDegisikligiKimlik == kimi.sifreDegisikligiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


