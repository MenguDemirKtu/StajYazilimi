using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class TopluSmsGonderimArama
    {
        public Int32? topluSMSGonderimKimlik { get; set; }
        public string? metin { get; set; }
        public DateTime? tarih { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public TopluSmsGonderimArama()
        {
            varmi = true;
        }

        public async Task<List<TopluSmsGonderim>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<TopluSmsGonderim>(P => P.varmi == true);
            if (topluSMSGonderimKimlik != null)
                predicate = predicate.And(x => x.topluSMSGonderimKimlik == topluSMSGonderimKimlik);
            if (metin != null)
                predicate = predicate.And(x => x.metin.Contains(metin));
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<TopluSmsGonderim> sonuc = new List<TopluSmsGonderim>();
            sonuc = await vari.TopluSmsGonderims
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class TopluSmsGonderimCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<TopluSmsGonderim> ara(params Predicate<TopluSmsGonderim>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.TopluSmsGonderims.ToList().FindAll(kosul).OrderByDescending(p => p.topluSMSGonderimKimlik).ToList();
            }
        }


        public static List<TopluSmsGonderim> tamami(Varlik kime)
        {
            return kime.TopluSmsGonderims.Where(p => p.varmi == true).OrderByDescending(p => p.topluSMSGonderimKimlik).ToList();
        }
        public static async Task<TopluSmsGonderim?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            TopluSmsGonderim? kayit = await kime.TopluSmsGonderims.FirstOrDefaultAsync(p => p.topluSMSGonderimKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(TopluSmsGonderim yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.topluSMSGonderimKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.TopluSmsGonderims.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                TopluSmsGonderim? bulunan = await vari.TopluSmsGonderims.FirstOrDefaultAsync(p => p.topluSMSGonderimKimlik == yeni.topluSMSGonderimKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(TopluSmsGonderim kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.TopluSmsGonderims.FirstOrDefaultAsync(p => p.topluSMSGonderimKimlik == kimi.topluSMSGonderimKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static TopluSmsGonderim tekliCek(Int32 kimlik, Varlik kime)
        {
            TopluSmsGonderim kayit = kime.TopluSmsGonderims.FirstOrDefault(p => p.topluSMSGonderimKimlik == kimlik);
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
        public static void kaydet(TopluSmsGonderim yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.topluSMSGonderimKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.TopluSmsGonderims.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.TopluSmsGonderims.FirstOrDefault(p => p.topluSMSGonderimKimlik == yeni.topluSMSGonderimKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(TopluSmsGonderim kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.TopluSmsGonderims.FirstOrDefault(p => p.topluSMSGonderimKimlik == kimi.topluSMSGonderimKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


