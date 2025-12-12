using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_OturumAcmaTuruArama
    {
        public Int32? oturumAcmaTuruKimlik { get; set; }
        public string? oturumAcmaTuru { get; set; }
        public ref_OturumAcmaTuruArama()
        {
        }

        public async Task<List<ref_OturumAcmaTuru>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<ref_OturumAcmaTuru>();
            if (oturumAcmaTuruKimlik != null)
                predicate = predicate.And(x => x.oturumAcmaTuruKimlik == oturumAcmaTuruKimlik);
            if (oturumAcmaTuru != null)
                predicate = predicate.And(x => x.oturumAcmaTuru != null && x.oturumAcmaTuru.Contains(oturumAcmaTuru));
            List<ref_OturumAcmaTuru> sonuc = new List<ref_OturumAcmaTuru>();
            sonuc = await vari.ref_OturumAcmaTurus
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class ref_OturumAcmaTuruCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ref_OturumAcmaTuru>> ara(params Expression<Func<ref_OturumAcmaTuru, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ref_OturumAcmaTuru>> ara(veri.Varlik vari, params Expression<Func<ref_OturumAcmaTuru, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_OturumAcmaTurus
                            .Where(kosul).OrderByDescending(p => p.oturumAcmaTuruKimlik)
                   .ToListAsync();
        }



        public static async Task<ref_OturumAcmaTuru?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_OturumAcmaTuru? kayit = await kime.ref_OturumAcmaTurus.FirstOrDefaultAsync(p => p.oturumAcmaTuruKimlik == kimlik);
            return kayit;
        }




        public static ref_OturumAcmaTuru? tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_OturumAcmaTuru? kayit = kime.ref_OturumAcmaTurus.FirstOrDefault(p => p.oturumAcmaTuruKimlik == kimlik);
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(ref_OturumAcmaTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.oturumAcmaTuruKimlik <= 0)
            {
                kime.ref_OturumAcmaTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_OturumAcmaTurus.FirstOrDefault(p => p.oturumAcmaTuruKimlik == yeni.oturumAcmaTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_OturumAcmaTuru kimi, Varlik kime)
        {

        }
    }
}


