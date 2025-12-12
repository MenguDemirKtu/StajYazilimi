// ;
// .Sql;
// .SqlClient;
// .SqlTypes;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ref_EPostaTuruArama
    {
        public Int32? ePostaTuruKimlik { get; set; }
        public string? ePostaTuru { get; set; }
        public string? alanAdlari { get; set; }
        public ref_EPostaTuruArama()
        {
        }

        public async Task<List<ref_EPostaTuru>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<ref_EPostaTuru>();
            if (ePostaTuruKimlik != null)
                predicate = predicate.And(x => x.ePostaTuruKimlik == ePostaTuruKimlik);
            if (ePostaTuru != null)
                predicate = predicate.And(x => x.ePostaTuru.Contains(ePostaTuru));
            if (alanAdlari != null)
                predicate = predicate.And(x => x.alanAdlari.Contains(alanAdlari));
            List<ref_EPostaTuru> sonuc = new List<ref_EPostaTuru>();
            sonuc = await vari.ref_EPostaTurus
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class ref_EPostaTuruCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_EPostaTuru> ara(params Predicate<ref_EPostaTuru>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ref_EPostaTurus.ToList().FindAll(kosul).OrderByDescending(p => p.ePostaTuruKimlik).ToList();
            }
        }


        public static List<ref_EPostaTuru> tamami(Varlik kime)
        {
            return kime.ref_EPostaTurus.ToList();
        }
        public static async Task<ref_EPostaTuru?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_EPostaTuru? kayit = await kime.ref_EPostaTurus.FirstOrDefaultAsync(p => p.ePostaTuruKimlik == kimlik);
            return kayit;
        }




        public static ref_EPostaTuru tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_EPostaTuru kayit = kime.ref_EPostaTurus.FirstOrDefault(p => p.ePostaTuruKimlik == kimlik);
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(ref_EPostaTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.ePostaTuruKimlik <= 0)
            {
                kime.ref_EPostaTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_EPostaTurus.FirstOrDefault(p => p.ePostaTuruKimlik == yeni.ePostaTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_EPostaTuru kimi, Varlik kime)
        {

        }
    }
}


