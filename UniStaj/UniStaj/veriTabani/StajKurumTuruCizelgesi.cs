using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajKurumTuruArama
    {
        public Int32? stajKurumTurukimlik { get; set; }
        public string? stajKurumTurAdi { get; set; }
        public bool? e_argeFirmasiMi { get; set; }
        public bool? varmi { get; set; }
        public StajKurumTuruArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajKurumTuru> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajKurumTuru>(P => P.varmi == true);
            if (stajKurumTurukimlik != null)
                predicate = predicate.And(x => x.stajKurumTurukimlik == stajKurumTurukimlik);
            if (stajKurumTurAdi != null)
                predicate = predicate.And(x => x.stajKurumTurAdi != null && x.stajKurumTurAdi.Contains(stajKurumTurAdi));
            if (e_argeFirmasiMi != null)
                predicate = predicate.And(x => x.e_argeFirmasiMi == e_argeFirmasiMi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajKurumTuru>> cek(veri.Varlik vari)
        {
            List<StajKurumTuru> sonuc = await vari.StajKurumTurus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajKurumTuru?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajKurumTuru? sonuc = await vari.StajKurumTurus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajKurumTuruCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajKurumTuru>> ara(params Expression<Func<StajKurumTuru, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajKurumTuru>> ara(veri.Varlik vari, params Expression<Func<StajKurumTuru, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajKurumTurus
                            .Where(kosul).OrderByDescending(p => p.stajKurumTurukimlik)
                   .ToListAsync();
        }



        public static async Task<StajKurumTuru?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajKurumTuru? kayit = await kime.StajKurumTurus.FirstOrDefaultAsync(p => p.stajKurumTurukimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajKurumTuru yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajKurumTurukimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajKurumTurus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajKurumTuru? bulunan = await vari.StajKurumTurus.FirstOrDefaultAsync(p => p.stajKurumTurukimlik == yeni.stajKurumTurukimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajKurumTuru kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajKurumTurus.FirstOrDefaultAsync(p => p.stajKurumTurukimlik == kimi.stajKurumTurukimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajKurumTuru? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajKurumTuru? kayit = kime.StajKurumTurus.FirstOrDefault(p => p.stajKurumTurukimlik == kimlik);
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
        public static void kaydet(StajKurumTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajKurumTurukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajKurumTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajKurumTurus.FirstOrDefault(p => p.stajKurumTurukimlik == yeni.stajKurumTurukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajKurumTuru kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajKurumTurus.FirstOrDefault(p => p.stajKurumTurukimlik == kimi.stajKurumTurukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


