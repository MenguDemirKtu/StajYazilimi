using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GonderilenSMSArama
    {
        public Int64? gonderilenSMSKimlik { get; set; }
        public string? telefon { get; set; }
        public string? baslik { get; set; }
        public string? adSoyAd { get; set; }
        public string? metin { get; set; }
        public DateTime? tarih { get; set; }
        public bool? varmi { get; set; }
        public Int64? y_topluGonderimKimlik { get; set; }
        public string? tcKimlikNo { get; set; }
        public bool? e_gonderildimi { get; set; }
        public GonderilenSMSArama()
        {
            this.varmi = true;
        }

        public async Task<List<GonderilenSMS>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<GonderilenSMS>(P => P.varmi == true);
            if (gonderilenSMSKimlik != null)
                predicate = predicate.And(x => x.gonderilenSMSKimlik == gonderilenSMSKimlik);
            if (telefon != null)
                predicate = predicate.And(x => x.telefon.Contains(telefon));
            if (baslik != null)
                predicate = predicate.And(x => x.baslik.Contains(baslik));
            if (adSoyAd != null)
                predicate = predicate.And(x => x.adSoyAd.Contains(adSoyAd));
            if (metin != null)
                predicate = predicate.And(x => x.metin.Contains(metin));
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (y_topluGonderimKimlik != null)
                predicate = predicate.And(x => x.y_topluGonderimKimlik == y_topluGonderimKimlik);
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo.Contains(tcKimlikNo));
            if (e_gonderildimi != null)
                predicate = predicate.And(x => x.e_gonderildimi == e_gonderildimi);
            List<GonderilenSMS> sonuc = new List<GonderilenSMS>();
            sonuc = await vari.GonderilenSMSs
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class GonderilenSMSCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<GonderilenSMS> ara(params Predicate<GonderilenSMS>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.GonderilenSMSs.ToList().FindAll(kosul).OrderByDescending(p => p.gonderilenSMSKimlik).ToList();
            }
        }


        public static List<GonderilenSMS> tamami(Varlik kime)
        {
            return kime.GonderilenSMSs.Where(p => p.varmi == true).OrderByDescending(p => p.gonderilenSMSKimlik).ToList();
        }
        public static async Task<GonderilenSMS?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            GonderilenSMS? kayit = await kime.GonderilenSMSs.FirstOrDefaultAsync(p => p.gonderilenSMSKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(GonderilenSMS yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.gonderilenSMSKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.GonderilenSMSs.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                GonderilenSMS? bulunan = await vari.GonderilenSMSs.FirstOrDefaultAsync(p => p.gonderilenSMSKimlik == yeni.gonderilenSMSKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(GonderilenSMS kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.GonderilenSMSs.FirstOrDefaultAsync(p => p.gonderilenSMSKimlik == kimi.gonderilenSMSKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static GonderilenSMS tekliCek(Int64 kimlik, Varlik kime)
        {
            GonderilenSMS kayit = kime.GonderilenSMSs.FirstOrDefault(p => p.gonderilenSMSKimlik == kimlik);
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
        public static void kaydet(GonderilenSMS yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.gonderilenSMSKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.GonderilenSMSs.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.GonderilenSMSs.FirstOrDefault(p => p.gonderilenSMSKimlik == yeni.gonderilenSMSKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(GonderilenSMS kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.GonderilenSMSs.FirstOrDefault(p => p.gonderilenSMSKimlik == kimi.gonderilenSMSKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


