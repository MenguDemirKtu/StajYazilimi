// ;
// .Sql;
// .SqlClient;
// .SqlTypes;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SmsKalibiArama
    {
        public Int32? smsKalibikimlik { get; set; }
        public string? baslik { get; set; }
        public Int32? i_epostaturukimlik { get; set; }
        public string? kalip { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public SmsKalibiArama()
        {
            this.varmi = true;
        }

        public async Task<List<SmsKalibi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SmsKalibi>(P => P.varmi == true);
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
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SmsKalibi> sonuc = new List<SmsKalibi>();
            sonuc = await vari.SmsKalibis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SmsKalibiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SmsKalibi> ara(params Predicate<SmsKalibi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SmsKalibis.ToList().FindAll(kosul).OrderByDescending(p => p.smsKalibikimlik).ToList();
            }
        }


        public static List<SmsKalibi> tamami(Varlik kime)
        {
            return kime.SmsKalibis.Where(p => p.varmi == true).OrderByDescending(p => p.smsKalibikimlik).ToList();
        }
        public static async Task<SmsKalibi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SmsKalibi? kayit = await kime.SmsKalibis.FirstOrDefaultAsync(p => p.smsKalibikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(SmsKalibi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.smsKalibikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.SmsKalibis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                SmsKalibi? bulunan = await vari.SmsKalibis.FirstOrDefaultAsync(p => p.smsKalibikimlik == yeni.smsKalibikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(SmsKalibi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.SmsKalibis.FirstOrDefaultAsync(p => p.smsKalibikimlik == kimi.smsKalibikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static SmsKalibi tekliCek(Int32 kimlik, Varlik kime)
        {
            SmsKalibi kayit = kime.SmsKalibis.FirstOrDefault(p => p.smsKalibikimlik == kimlik);
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
        public static void kaydet(SmsKalibi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.smsKalibikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SmsKalibis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.SmsKalibis.FirstOrDefault(p => p.smsKalibikimlik == yeni.smsKalibikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(SmsKalibi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SmsKalibis.FirstOrDefault(p => p.smsKalibikimlik == kimi.smsKalibikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


