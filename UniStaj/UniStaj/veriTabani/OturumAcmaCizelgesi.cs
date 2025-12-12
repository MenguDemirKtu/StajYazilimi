// ;
// .Sql;
// .SqlClient;
// .SqlTypes;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class OturumAcmaArama
    {
        public Int32? oturumAcmaKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public Int32? gunlukSayi { get; set; }
        public bool? varmi { get; set; }
        public OturumAcmaArama()
        {
            this.varmi = true;
        }

        public async Task<List<OturumAcma>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<OturumAcma>(P => P.varmi == true);
            if (oturumAcmaKimlik != null)
                predicate = predicate.And(x => x.oturumAcmaKimlik == oturumAcmaKimlik);
            if (i_kullaniciKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciKimlik == i_kullaniciKimlik);
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (gunlukSayi != null)
                predicate = predicate.And(x => x.gunlukSayi == gunlukSayi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<OturumAcma> sonuc = new List<OturumAcma>();
            sonuc = await vari.OturumAcmas
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class OturumAcmaCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<OturumAcma> ara(params Predicate<OturumAcma>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.OturumAcmas.ToList().FindAll(kosul).OrderByDescending(p => p.oturumAcmaKimlik).ToList();
            }
        }


        public static List<OturumAcma> tamami(Varlik kime)
        {
            return kime.OturumAcmas.Where(p => p.varmi == true).OrderByDescending(p => p.oturumAcmaKimlik).ToList();
        }
        public static async Task<OturumAcma?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            OturumAcma? kayit = await kime.OturumAcmas.FirstOrDefaultAsync(p => p.oturumAcmaKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(OturumAcma yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.oturumAcmaKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.OturumAcmas.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                OturumAcma? bulunan = await vari.OturumAcmas.FirstOrDefaultAsync(p => p.oturumAcmaKimlik == yeni.oturumAcmaKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(OturumAcma kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.OturumAcmas.FirstOrDefaultAsync(p => p.oturumAcmaKimlik == kimi.oturumAcmaKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static OturumAcma tekliCek(Int32 kimlik, Varlik kime)
        {
            OturumAcma kayit = kime.OturumAcmas.FirstOrDefault(p => p.oturumAcmaKimlik == kimlik);
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
        public static void kaydet(OturumAcma yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.oturumAcmaKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.OturumAcmas.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.OturumAcmas.FirstOrDefault(p => p.oturumAcmaKimlik == yeni.oturumAcmaKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(OturumAcma kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.OturumAcmas.FirstOrDefault(p => p.oturumAcmaKimlik == kimi.oturumAcmaKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


