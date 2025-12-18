using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class ref_StajTipiArama
    {
        public Int32? StajTipiKimlik { get; set; }
        public string? StajTipiAdi { get; set; }
        public ref_StajTipiArama()
        {
        }

        private ExpressionStarter<ref_StajTipi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<ref_StajTipi>();
            if (StajTipiKimlik != null)
                predicate = predicate.And(x => x.StajTipiKimlik == StajTipiKimlik);
            if (StajTipiAdi != null)
                predicate = predicate.And(x => x.StajTipiAdi != null && x.StajTipiAdi.Contains(StajTipiAdi));
            return predicate;

        }
        public async Task<List<ref_StajTipi>> cek(veri.Varlik vari)
        {
            List<ref_StajTipi> sonuc = await vari.ref_StajTipis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<ref_StajTipi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            ref_StajTipi? sonuc = await vari.ref_StajTipis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class ref_StajTipiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<ref_StajTipi>> ara(params Expression<Func<ref_StajTipi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<ref_StajTipi>> ara(veri.Varlik vari, params Expression<Func<ref_StajTipi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.ref_StajTipis
                            .Where(kosul).OrderByDescending(p => p.StajTipiKimlik)
                   .ToListAsync();
        }



        public static async Task<ref_StajTipi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_StajTipi? kayit = await kime.ref_StajTipis.FirstOrDefaultAsync(p => p.StajTipiKimlik == kimlik);
            return kayit;
        }




        public static ref_StajTipi? tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_StajTipi? kayit = kime.ref_StajTipis.FirstOrDefault(p => p.StajTipiKimlik == kimlik);
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(ref_StajTipi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.StajTipiKimlik <= 0)
            {
                kime.ref_StajTipis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_StajTipis.FirstOrDefault(p => p.StajTipiKimlik == yeni.StajTipiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_StajTipi kimi, Varlik kime)
        {

        }
    }
}


