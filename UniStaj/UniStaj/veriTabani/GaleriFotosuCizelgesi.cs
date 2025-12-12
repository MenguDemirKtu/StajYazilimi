using Microsoft.EntityFrameworkCore; //;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GaleriFotosuArama
    {
        public Int32? galeriFotosuKimlik { get; set; }
        public Int32? i_galeriKimlik { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public bool? e_gosterimdemi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public GaleriFotosuArama()
        {
            this.varmi = true;
        }

        public async Task<List<GaleriFotosu>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<GaleriFotosu>(P => P.varmi == true);
            if (galeriFotosuKimlik != null)
                predicate = predicate.And(x => x.galeriFotosuKimlik == galeriFotosuKimlik);
            if (i_galeriKimlik != null)
                predicate = predicate.And(x => x.i_galeriKimlik == i_galeriKimlik);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (e_gosterimdemi != null)
                predicate = predicate.And(x => x.e_gosterimdemi == e_gosterimdemi);
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<GaleriFotosu> sonuc = new List<GaleriFotosu>();
            sonuc = await vari.GaleriFotosus
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class GaleriFotosuCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<GaleriFotosu> ara(params Predicate<GaleriFotosu>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.GaleriFotosus.ToList().FindAll(kosul).OrderByDescending(p => p.galeriFotosuKimlik).ToList();
            }
        }


        public static List<GaleriFotosu> tamami(Varlik kime)
        {
            return kime.GaleriFotosus.Where(p => p.varmi == true).OrderByDescending(p => p.galeriFotosuKimlik).ToList();
        }
        public static async Task<GaleriFotosu?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            GaleriFotosu? kayit = await kime.GaleriFotosus.FirstOrDefaultAsync(p => p.galeriFotosuKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(GaleriFotosu yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.galeriFotosuKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.GaleriFotosus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                GaleriFotosu? bulunan = await vari.GaleriFotosus.FirstOrDefaultAsync(p => p.galeriFotosuKimlik == yeni.galeriFotosuKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(GaleriFotosu kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.GaleriFotosus.FirstOrDefaultAsync(p => p.galeriFotosuKimlik == kimi.galeriFotosuKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static GaleriFotosu? tekliCek(Int32 kimlik, Varlik kime)
        {
            GaleriFotosu? kayit = kime.GaleriFotosus.FirstOrDefault(p => p.galeriFotosuKimlik == kimlik);
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
        public static void kaydet(GaleriFotosu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.galeriFotosuKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.GaleriFotosus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.GaleriFotosus.FirstOrDefault(p => p.galeriFotosuKimlik == yeni.galeriFotosuKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(GaleriFotosu kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.GaleriFotosus.FirstOrDefault(p => p.galeriFotosuKimlik == kimi.galeriFotosuKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


