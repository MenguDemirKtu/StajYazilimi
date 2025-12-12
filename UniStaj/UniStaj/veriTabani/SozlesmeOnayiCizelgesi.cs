using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class SozlesmeOnayiArama
    {
        public Int64? sozlesmeOnayiKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public Int32? i_sozlesmeKimlik { get; set; }
        public bool? varmi { get; set; }
        public SozlesmeOnayiArama()
        {
            varmi = true;
        }

        public async Task<List<SozlesmeOnayi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SozlesmeOnayi>(P => P.varmi == true);
            if (sozlesmeOnayiKimlik != null)
                predicate = predicate.And(x => x.sozlesmeOnayiKimlik == sozlesmeOnayiKimlik);
            if (i_kullaniciKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciKimlik == i_kullaniciKimlik);
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (i_sozlesmeKimlik != null)
                predicate = predicate.And(x => x.i_sozlesmeKimlik == i_sozlesmeKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SozlesmeOnayi> sonuc = new List<SozlesmeOnayi>();
            sonuc = await vari.SozlesmeOnayis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SozlesmeOnayiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SozlesmeOnayi> ara(params Predicate<SozlesmeOnayi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SozlesmeOnayis.ToList().FindAll(kosul).OrderByDescending(p => p.sozlesmeOnayiKimlik).ToList();
            }
        }


        public static List<SozlesmeOnayi> tamami(Varlik kime)
        {
            return kime.SozlesmeOnayis.Where(p => p.varmi == true).OrderByDescending(p => p.sozlesmeOnayiKimlik).ToList();
        }
        public static async Task<SozlesmeOnayi?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            SozlesmeOnayi? kayit = await kime.SozlesmeOnayis.FirstOrDefaultAsync(p => p.sozlesmeOnayiKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(SozlesmeOnayi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.sozlesmeOnayiKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.SozlesmeOnayis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                SozlesmeOnayi? bulunan = await vari.SozlesmeOnayis.FirstOrDefaultAsync(p => p.sozlesmeOnayiKimlik == yeni.sozlesmeOnayiKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(SozlesmeOnayi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.SozlesmeOnayis.FirstOrDefaultAsync(p => p.sozlesmeOnayiKimlik == kimi.sozlesmeOnayiKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static SozlesmeOnayi tekliCek(Int64 kimlik, Varlik kime)
        {
            SozlesmeOnayi kayit = kime.SozlesmeOnayis.FirstOrDefault(p => p.sozlesmeOnayiKimlik == kimlik);
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
        public static void kaydet(SozlesmeOnayi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sozlesmeOnayiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SozlesmeOnayis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.SozlesmeOnayis.FirstOrDefault(p => p.sozlesmeOnayiKimlik == yeni.sozlesmeOnayiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(SozlesmeOnayi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SozlesmeOnayis.FirstOrDefault(p => p.sozlesmeOnayiKimlik == kimi.sozlesmeOnayiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


