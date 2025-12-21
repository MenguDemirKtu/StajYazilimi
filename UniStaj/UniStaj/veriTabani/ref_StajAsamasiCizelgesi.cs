using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class ref_StajAsamasiArama
    {
        public Int32? StajAsamasiKimlik { get; set; }
        public string? StajAsamasiAdi { get; set; }
        public ref_StajAsamasiArama()
        {
        }

        private ExpressionStarter<ref_StajAsamasi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<ref_StajAsamasi>();
            if (StajAsamasiKimlik != null)
                predicate = predicate.And(x => x.StajAsamasiKimlik == StajAsamasiKimlik);
            if (StajAsamasiAdi != null)
                predicate = predicate.And(x => x.StajAsamasiAdi != null && x.StajAsamasiAdi.Contains(StajAsamasiAdi));
            return predicate;

        }
        public async Task<List<ref_StajAsamasi>> cek(veri.Varlik vari)
        {
            List<ref_StajAsamasi> sonuc = await vari.ref_StajAsamasis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<ref_StajAsamasi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            ref_StajAsamasi? sonuc = await vari.ref_StajAsamasis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class ref_StajAsamasiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ref_StajAsamasi>> ara(params Expression<Func<ref_StajAsamasi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ref_StajAsamasi>> ara(veri.Varlik vari, params Expression<Func<ref_StajAsamasi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_StajAsamasis
                            .Where(kosul).OrderByDescending(p => p.StajAsamasiKimlik)
                   .ToListAsync();
        }



        public static async Task<ref_StajAsamasi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_StajAsamasi? kayit = await kime.ref_StajAsamasis.FirstOrDefaultAsync(p => p.StajAsamasiKimlik == kimlik);
            return kayit;
        }




        public static ref_StajAsamasi? tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_StajAsamasi? kayit = kime.ref_StajAsamasis.FirstOrDefault(p => p.StajAsamasiKimlik == kimlik);
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(ref_StajAsamasi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.StajAsamasiKimlik <= 0)
            {
                kime.ref_StajAsamasis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_StajAsamasis.FirstOrDefault(p => p.StajAsamasiKimlik == yeni.StajAsamasiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_StajAsamasi kimi, Varlik kime)
        {

        }
    }
}


