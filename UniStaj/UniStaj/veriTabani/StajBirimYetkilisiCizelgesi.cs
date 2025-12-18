using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBirimYetkilisiArama
    {
        public Int32? stajBirimYetkilisikimlik { get; set; }
        public string? tcKimlikNo { get; set; }
        public string? ad { get; set; }
        public string? soyad { get; set; }
        public string? telefon { get; set; }
        public string? ePosta { get; set; }
        public bool? varmi { get; set; }
        public StajBirimYetkilisiArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBirimYetkilisi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBirimYetkilisi>(P => P.varmi == true);
            if (stajBirimYetkilisikimlik != null)
                predicate = predicate.And(x => x.stajBirimYetkilisikimlik == stajBirimYetkilisikimlik);
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (ad != null)
                predicate = predicate.And(x => x.ad != null && x.ad.Contains(ad));
            if (soyad != null)
                predicate = predicate.And(x => x.soyad != null && x.soyad.Contains(soyad));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajBirimYetkilisi>> cek(veri.Varlik vari)
        {
            List<StajBirimYetkilisi> sonuc = await vari.StajBirimYetkilisis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBirimYetkilisi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBirimYetkilisi? sonuc = await vari.StajBirimYetkilisis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBirimYetkilisiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBirimYetkilisi>> ara(params Expression<Func<StajBirimYetkilisi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBirimYetkilisi>> ara(veri.Varlik vari, params Expression<Func<StajBirimYetkilisi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBirimYetkilisis
                            .Where(kosul).OrderByDescending(p => p.stajBirimYetkilisikimlik)
                   .ToListAsync();
        }



        public static async Task<StajBirimYetkilisi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBirimYetkilisi? kayit = await kime.StajBirimYetkilisis.FirstOrDefaultAsync(p => p.stajBirimYetkilisikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajBirimYetkilisi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBirimYetkilisikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajBirimYetkilisis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajBirimYetkilisi? bulunan = await vari.StajBirimYetkilisis.FirstOrDefaultAsync(p => p.stajBirimYetkilisikimlik == yeni.stajBirimYetkilisikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajBirimYetkilisi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajBirimYetkilisis.FirstOrDefaultAsync(p => p.stajBirimYetkilisikimlik == kimi.stajBirimYetkilisikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajBirimYetkilisi? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBirimYetkilisi? kayit = kime.StajBirimYetkilisis.FirstOrDefault(p => p.stajBirimYetkilisikimlik == kimlik);
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
        public static void kaydet(StajBirimYetkilisi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBirimYetkilisikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajBirimYetkilisis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajBirimYetkilisis.FirstOrDefault(p => p.stajBirimYetkilisikimlik == yeni.stajBirimYetkilisikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajBirimYetkilisi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajBirimYetkilisis.FirstOrDefault(p => p.stajBirimYetkilisikimlik == kimi.stajBirimYetkilisikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


