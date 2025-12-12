using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class ref_SozlesmeTuruArama
    {
        public Int32? SozlesmeTuruKimlik { get; set; }
        public string? SozlesmeTuruAdi { get; set; }
        public ref_SozlesmeTuruArama()
        {
        }

        public async Task<List<ref_SozlesmeTuru>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<ref_SozlesmeTuru>();
            if (SozlesmeTuruKimlik != null)
                predicate = predicate.And(x => x.SozlesmeTuruKimlik == SozlesmeTuruKimlik);
            if (SozlesmeTuruAdi != null)
                predicate = predicate.And(x => x.SozlesmeTuruAdi.Contains(SozlesmeTuruAdi));
            List<ref_SozlesmeTuru> sonuc = new List<ref_SozlesmeTuru>();
            sonuc = await vari.ref_SozlesmeTurus
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class ref_SozlesmeTuruCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ref_SozlesmeTuru> ara(params Predicate<ref_SozlesmeTuru>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ref_SozlesmeTurus.ToList().FindAll(kosul).OrderByDescending(p => p.SozlesmeTuruKimlik).ToList();
            }
        }


        public static List<ref_SozlesmeTuru> tamami(Varlik kime)
        {
            return kime.ref_SozlesmeTurus.ToList();
        }
        public static async Task<ref_SozlesmeTuru?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ref_SozlesmeTuru? kayit = await kime.ref_SozlesmeTurus.FirstOrDefaultAsync(p => p.SozlesmeTuruKimlik == kimlik);
            return kayit;
        }




        public static ref_SozlesmeTuru tekliCek(Int32 kimlik, Varlik kime)
        {
            ref_SozlesmeTuru kayit = kime.ref_SozlesmeTurus.FirstOrDefault(p => p.SozlesmeTuruKimlik == kimlik);
            return kayit;
        }


        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(ref_SozlesmeTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.SozlesmeTuruKimlik <= 0)
            {
                kime.ref_SozlesmeTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ref_SozlesmeTurus.FirstOrDefault(p => p.SozlesmeTuruKimlik == yeni.SozlesmeTuruKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ref_SozlesmeTuru kimi, Varlik kime)
        {

        }
    }
}


