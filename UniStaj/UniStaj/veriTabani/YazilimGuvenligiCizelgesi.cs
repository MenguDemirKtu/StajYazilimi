using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class YazilimGuvenligiArama
    {
        public Int32? yazilimGuvenligikimlik { get; set; }
        public DateTime? sonKullanimTarihi { get; set; }
        public string? apiKodu { get; set; }
        public string? pythonKodu { get; set; }
        public bool? e_gecerliMi { get; set; }
        public YazilimGuvenligiArama()
        {
        }

        public async Task<List<YazilimGuvenligi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<YazilimGuvenligi>(P => P.varmi == true);
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
            List<YazilimGuvenligi> sonuc = new List<YazilimGuvenligi>();
            sonuc = await vari.YazilimGuvenligis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class YazilimGuvenligiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<YazilimGuvenligi> ara(params Predicate<YazilimGuvenligi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.YazilimGuvenligis.ToList().FindAll(kosul).OrderByDescending(p => p.yazilimGuvenligikimlik).ToList();
            }
        }


        public static List<YazilimGuvenligi> tamami(Varlik kime)
        {
            return kime.YazilimGuvenligis.ToList();
        }
        public static async Task<YazilimGuvenligi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            YazilimGuvenligi? kayit = await kime.YazilimGuvenligis.FirstOrDefaultAsync(p => p.yazilimGuvenligikimlik == kimlik);
            return kayit;
        }


        public static async Task kaydetKos(YazilimGuvenligi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.yazilimGuvenligikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                await vari.YazilimGuvenligis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                YazilimGuvenligi? bulunan = await vari.YazilimGuvenligis.FirstOrDefaultAsync(p => p.yazilimGuvenligikimlik == yeni.yazilimGuvenligikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(YazilimGuvenligi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.YazilimGuvenligis.FirstOrDefaultAsync(p => p.yazilimGuvenligikimlik == kimi.yazilimGuvenligikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static YazilimGuvenligi tekliCek(Int32 kimlik, Varlik kime)
        {
            YazilimGuvenligi kayit = kime.YazilimGuvenligis.FirstOrDefault(p => p.yazilimGuvenligikimlik == kimlik);
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(YazilimGuvenligi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.yazilimGuvenligikimlik <= 0)
            {
                kime.YazilimGuvenligis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.YazilimGuvenligis.FirstOrDefault(p => p.yazilimGuvenligikimlik == yeni.yazilimGuvenligikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(YazilimGuvenligi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.YazilimGuvenligis.FirstOrDefault(p => p.yazilimGuvenligikimlik == kimi.yazilimGuvenligikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


