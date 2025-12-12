using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class TopluEPostaAlicisiArama
    {
        public Int64? topluEPostaAlicisiKimlik { get; set; }
        public Int32? i_topluSMSGonderimKimlik { get; set; }
        public string? aliciTanimi { get; set; }
        public bool? varmi { get; set; }
        public string? ePostaAdresi { get; set; }
        public TopluEPostaAlicisiArama()
        {
            this.varmi = true;
        }

        public async Task<List<TopluEPostaAlicisi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<TopluEPostaAlicisi>(P => P.varmi == true);
            if (topluEPostaAlicisiKimlik != null)
                predicate = predicate.And(x => x.topluEPostaAlicisiKimlik == topluEPostaAlicisiKimlik);
            if (i_topluSMSGonderimKimlik != null)
                predicate = predicate.And(x => x.i_topluSMSGonderimKimlik == i_topluSMSGonderimKimlik);
            if (aliciTanimi != null)
                predicate = predicate.And(x => x.aliciTanimi.Contains(aliciTanimi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (ePostaAdresi != null)
                predicate = predicate.And(x => x.ePostaAdresi.Contains(ePostaAdresi));
            List<TopluEPostaAlicisi> sonuc = new List<TopluEPostaAlicisi>();
            sonuc = await vari.TopluEPostaAlicisis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class TopluEPostaAlicisiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<TopluEPostaAlicisi> ara(params Predicate<TopluEPostaAlicisi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.TopluEPostaAlicisis.ToList().FindAll(kosul).OrderByDescending(p => p.topluEPostaAlicisiKimlik).ToList();
            }
        }


        public static List<TopluEPostaAlicisi> tamami(Varlik kime)
        {
            return kime.TopluEPostaAlicisis.Where(p => p.varmi == true).OrderByDescending(p => p.topluEPostaAlicisiKimlik).ToList();
        }
        public static async Task<TopluEPostaAlicisi?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            TopluEPostaAlicisi? kayit = await kime.TopluEPostaAlicisis.FirstOrDefaultAsync(p => p.topluEPostaAlicisiKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(TopluEPostaAlicisi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.topluEPostaAlicisiKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.TopluEPostaAlicisis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                TopluEPostaAlicisi? bulunan = await vari.TopluEPostaAlicisis.FirstOrDefaultAsync(p => p.topluEPostaAlicisiKimlik == yeni.topluEPostaAlicisiKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(TopluEPostaAlicisi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.TopluEPostaAlicisis.FirstOrDefaultAsync(p => p.topluEPostaAlicisiKimlik == kimi.topluEPostaAlicisiKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static TopluEPostaAlicisi tekliCek(Int64 kimlik, Varlik kime)
        {
            TopluEPostaAlicisi kayit = kime.TopluEPostaAlicisis.FirstOrDefault(p => p.topluEPostaAlicisiKimlik == kimlik);
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
        public static void kaydet(TopluEPostaAlicisi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.topluEPostaAlicisiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.TopluEPostaAlicisis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.TopluEPostaAlicisis.FirstOrDefault(p => p.topluEPostaAlicisiKimlik == yeni.topluEPostaAlicisiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(TopluEPostaAlicisi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.TopluEPostaAlicisis.FirstOrDefault(p => p.topluEPostaAlicisiKimlik == kimi.topluEPostaAlicisiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


