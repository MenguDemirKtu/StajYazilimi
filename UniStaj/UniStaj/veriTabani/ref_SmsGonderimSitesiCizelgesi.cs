using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_SmsGonderimSitesiArama
    {
        public Int32? SmsGonderimSitesiKimlik { get; set; }
        public string? SmsGonderimSitesiAdi { get; set; }
        public ref_SmsGonderimSitesiArama()
        {
        }

        public async Task<List<ref_SmsGonderimSitesi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<ref_SmsGonderimSitesi>();
            if (SmsGonderimSitesiKimlik != null)
                predicate = predicate.And(x => x.SmsGonderimSitesiKimlik == SmsGonderimSitesiKimlik);
            if (SmsGonderimSitesiAdi != null)
                predicate = predicate.And(x => x.SmsGonderimSitesiAdi != null && x.SmsGonderimSitesiAdi.Contains(SmsGonderimSitesiAdi));
            List<ref_SmsGonderimSitesi> sonuc = new List<ref_SmsGonderimSitesi>();
            sonuc = await vari.ref_SmsGonderimSitesis
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class ref_SmsGonderimSitesiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ref_SmsGonderimSitesi>> ara(params Expression<Func<ref_SmsGonderimSitesi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ref_SmsGonderimSitesi>> ara(veri.Varlik vari, params Expression<Func<ref_SmsGonderimSitesi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_SmsGonderimSitesis
                            .Where(kosul).OrderByDescending(p => p.SmsGonderimSitesiKimlik)
                   .ToListAsync();
        }



        public static async Task<ref_SmsGonderimSitesi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_SmsGonderimSitesi? kayit = await kime.ref_SmsGonderimSitesis.FirstOrDefaultAsync(p => p.SmsGonderimSitesiKimlik == kimlik);
            return kayit;
        }




        public static ref_SmsGonderimSitesi? tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_SmsGonderimSitesi? kayit = kime.ref_SmsGonderimSitesis.FirstOrDefault(p => p.SmsGonderimSitesiKimlik == kimlik);
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(ref_SmsGonderimSitesi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.SmsGonderimSitesiKimlik <= 0)
            {
                kime.ref_SmsGonderimSitesis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_SmsGonderimSitesis.FirstOrDefault(p => p.SmsGonderimSitesiKimlik == yeni.SmsGonderimSitesiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_SmsGonderimSitesi kimi, Varlik kime)
        {

        }
    }
}


