using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajyerYukumlulukArama
    {
        public Int32? stajyerYukumlulukkimlik { get; set; }
        public Int32? i_stajyerKimlik { get; set; }
        public Int32? i_stajTuruKimlik { get; set; }
        public Int32? gunSayisi { get; set; }
        public string? siniflar { get; set; }
        public Int32? yaptigiGunSayis { get; set; }
        public Int32? kabulEdilenGunSayisi { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public StajyerYukumlulukArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajyerYukumluluk> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajyerYukumluluk>(P => P.varmi == true);
            if (stajyerYukumlulukkimlik != null)
                predicate = predicate.And(x => x.stajyerYukumlulukkimlik == stajyerYukumlulukkimlik);
            if (i_stajyerKimlik != null)
                predicate = predicate.And(x => x.i_stajyerKimlik == i_stajyerKimlik);
            if (i_stajTuruKimlik != null)
                predicate = predicate.And(x => x.i_stajTuruKimlik == i_stajTuruKimlik);
            if (gunSayisi != null)
                predicate = predicate.And(x => x.gunSayisi == gunSayisi);
            if (siniflar != null)
                predicate = predicate.And(x => x.siniflar != null && x.siniflar.Contains(siniflar));
            if (yaptigiGunSayis != null)
                predicate = predicate.And(x => x.yaptigiGunSayis == yaptigiGunSayis);
            if (kabulEdilenGunSayisi != null)
                predicate = predicate.And(x => x.kabulEdilenGunSayisi == kabulEdilenGunSayisi);
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajyerYukumluluk>> cek(veri.Varlik vari)
        {
            List<StajyerYukumluluk> sonuc = await vari.StajyerYukumluluks
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajyerYukumluluk?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajyerYukumluluk? sonuc = await vari.StajyerYukumluluks
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajyerYukumlulukCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajyerYukumluluk>> ara(params Expression<Func<StajyerYukumluluk, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajyerYukumluluk>> ara(veri.Varlik vari, params Expression<Func<StajyerYukumluluk, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajyerYukumluluks
                            .Where(kosul).OrderByDescending(p => p.stajyerYukumlulukkimlik)
                   .ToListAsync();
        }



        public static async Task<StajyerYukumluluk?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajyerYukumluluk? kayit = await kime.StajyerYukumluluks.FirstOrDefaultAsync(p => p.stajyerYukumlulukkimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajyerYukumluluk yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajyerYukumlulukkimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajyerYukumluluks.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajyerYukumluluk? bulunan = await vari.StajyerYukumluluks.FirstOrDefaultAsync(p => p.stajyerYukumlulukkimlik == yeni.stajyerYukumlulukkimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajyerYukumluluk kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajyerYukumluluks.FirstOrDefaultAsync(p => p.stajyerYukumlulukkimlik == kimi.stajyerYukumlulukkimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajyerYukumluluk? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajyerYukumluluk? kayit = kime.StajyerYukumluluks.FirstOrDefault(p => p.stajyerYukumlulukkimlik == kimlik);
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
        public static void kaydet(StajyerYukumluluk yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajyerYukumlulukkimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajyerYukumluluks.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajyerYukumluluks.FirstOrDefault(p => p.stajyerYukumlulukkimlik == yeni.stajyerYukumlulukkimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajyerYukumluluk kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajyerYukumluluks.FirstOrDefault(p => p.stajyerYukumlulukkimlik == kimi.stajyerYukumlulukkimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


