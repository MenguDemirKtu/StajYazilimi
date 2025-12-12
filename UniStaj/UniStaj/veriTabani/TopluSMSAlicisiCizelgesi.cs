using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class TopluSMSAlicisiArama
    {
        public Int64? toplSMSAlicisiKimlik { get; set; }
        public Int32? i_topluSMSGonderimKimlik { get; set; }
        public string? telefon { get; set; }
        public string? aliciTanimi { get; set; }
        public bool? varmi { get; set; }
        public TopluSMSAlicisiArama()
        {
            varmi = true;
        }

        public async Task<List<TopluSMSAlicisi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<TopluSMSAlicisi>(P => P.varmi == true);
            if (toplSMSAlicisiKimlik != null)
                predicate = predicate.And(x => x.toplSMSAlicisiKimlik == toplSMSAlicisiKimlik);
            if (i_topluSMSGonderimKimlik != null)
                predicate = predicate.And(x => x.i_topluSMSGonderimKimlik == i_topluSMSGonderimKimlik);
            if (telefon != null)
                predicate = predicate.And(x => x.telefon.Contains(telefon));
            if (aliciTanimi != null)
                predicate = predicate.And(x => x.aliciTanimi.Contains(aliciTanimi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<TopluSMSAlicisi> sonuc = new List<TopluSMSAlicisi>();
            sonuc = await vari.TopluSMSAlicisis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class TopluSMSAlicisiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<TopluSMSAlicisi> ara(params Predicate<TopluSMSAlicisi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.TopluSMSAlicisis.ToList().FindAll(kosul).OrderByDescending(p => p.toplSMSAlicisiKimlik).ToList();
            }
        }


        public static List<TopluSMSAlicisi> tamami(Varlik kime)
        {
            return kime.TopluSMSAlicisis.Where(p => p.varmi == true).OrderByDescending(p => p.toplSMSAlicisiKimlik).ToList();
        }
        public static async Task<TopluSMSAlicisi?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            TopluSMSAlicisi? kayit = await kime.TopluSMSAlicisis.FirstOrDefaultAsync(p => p.toplSMSAlicisiKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(TopluSMSAlicisi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.toplSMSAlicisiKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.TopluSMSAlicisis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                TopluSMSAlicisi? bulunan = await vari.TopluSMSAlicisis.FirstOrDefaultAsync(p => p.toplSMSAlicisiKimlik == yeni.toplSMSAlicisiKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(TopluSMSAlicisi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.TopluSMSAlicisis.FirstOrDefaultAsync(p => p.toplSMSAlicisiKimlik == kimi.toplSMSAlicisiKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static TopluSMSAlicisi tekliCek(Int64 kimlik, Varlik kime)
        {
            TopluSMSAlicisi kayit = kime.TopluSMSAlicisis.FirstOrDefault(p => p.toplSMSAlicisiKimlik == kimlik);
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
        public static void kaydet(TopluSMSAlicisi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.toplSMSAlicisiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.TopluSMSAlicisis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.TopluSMSAlicisis.FirstOrDefault(p => p.toplSMSAlicisiKimlik == yeni.toplSMSAlicisiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(TopluSMSAlicisi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.TopluSMSAlicisis.FirstOrDefault(p => p.toplSMSAlicisiKimlik == kimi.toplSMSAlicisiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


