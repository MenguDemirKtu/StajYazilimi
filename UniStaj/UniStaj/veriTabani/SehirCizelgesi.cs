using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SehirArama
    {
        public Int32? sehirKimlik { get; set; }
        public string? sehirAdi { get; set; }
        public string? telefonKodu { get; set; }
        public string? plakaKodu { get; set; }
        public bool? varmi { get; set; }
        public SehirArama()
        {
            this.varmi = true;
        }

        public async Task<List<Sehir>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Sehir>(P => P.varmi == true);
            if (sehirKimlik != null)
                predicate = predicate.And(x => x.sehirKimlik == sehirKimlik);
            if (sehirAdi != null)
                predicate = predicate.And(x => x.sehirAdi != null && x.sehirAdi.Contains(sehirAdi));
            if (telefonKodu != null)
                predicate = predicate.And(x => x.telefonKodu != null && x.telefonKodu.Contains(telefonKodu));
            if (plakaKodu != null)
                predicate = predicate.And(x => x.plakaKodu != null && x.plakaKodu.Contains(plakaKodu));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<Sehir> sonuc = new List<Sehir>();
            sonuc = await vari.Sehirs
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class SehirCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Sehir>> ara(params Expression<Func<Sehir, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Sehir>> ara(veri.Varlik vari, params Expression<Func<Sehir, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Sehirs
                            .Where(kosul).OrderByDescending(p => p.sehirKimlik)
                   .ToListAsync();
        }



        public static async Task<Sehir?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Sehir? kayit = await kime.Sehirs.FirstOrDefaultAsync(p => p.sehirKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Sehir yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.sehirKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Sehirs.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Sehir? bulunan = await vari.Sehirs.FirstOrDefaultAsync(p => p.sehirKimlik == yeni.sehirKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Sehir kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Sehirs.FirstOrDefaultAsync(p => p.sehirKimlik == kimi.sehirKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Sehir? tekliCek(Int32 kimlik, Varlik kime)
        {
            Sehir? kayit = kime.Sehirs.FirstOrDefault(p => p.sehirKimlik == kimlik);
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
        public static void kaydet(Sehir yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sehirKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Sehirs.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Sehirs.FirstOrDefault(p => p.sehirKimlik == yeni.sehirKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Sehir kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Sehirs.FirstOrDefault(p => p.sehirKimlik == kimi.sehirKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


