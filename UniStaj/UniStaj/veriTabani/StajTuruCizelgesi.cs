using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajTuruArama
    {
        public Int32? stajTurukimlik { get; set; }
        public string? stajTuruAdi { get; set; }
        public string? tanim { get; set; }
        public bool? varmi { get; set; }
        public StajTuruArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajTuru> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajTuru>(P => P.varmi == true);
            if (stajTurukimlik != null)
                predicate = predicate.And(x => x.stajTurukimlik == stajTurukimlik);
            if (stajTuruAdi != null)
                predicate = predicate.And(x => x.stajTuruAdi != null && x.stajTuruAdi.Contains(stajTuruAdi));
            if (tanim != null)
                predicate = predicate.And(x => x.tanim != null && x.tanim.Contains(tanim));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajTuru>> cek(veri.Varlik vari)
        {
            List<StajTuru> sonuc = await vari.StajTurus
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajTuru?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajTuru? sonuc = await vari.StajTurus
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajTuruCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajTuru>> ara(params Expression<Func<StajTuru, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajTuru>> ara(veri.Varlik vari, params Expression<Func<StajTuru, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajTurus
                            .Where(kosul).OrderByDescending(p => p.stajTurukimlik)
                   .ToListAsync();
        }



        public static async Task<StajTuru?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajTuru? kayit = await kime.StajTurus.FirstOrDefaultAsync(p => p.stajTurukimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajTuru yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajTurukimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajTurus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajTuru? bulunan = await vari.StajTurus.FirstOrDefaultAsync(p => p.stajTurukimlik == yeni.stajTurukimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajTuru kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajTurus.FirstOrDefaultAsync(p => p.stajTurukimlik == kimi.stajTurukimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajTuru? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajTuru? kayit = kime.StajTurus.FirstOrDefault(p => p.stajTurukimlik == kimlik);
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
        public static void kaydet(StajTuru yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajTurukimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajTurus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajTurus.FirstOrDefault(p => p.stajTurukimlik == yeni.stajTurukimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajTuru kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajTurus.FirstOrDefault(p => p.stajTurukimlik == kimi.stajTurukimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


