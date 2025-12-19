using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajyerArama
    {
        public Int32? stajyerkimlik { get; set; }
        public string? ogrenciNo { get; set; }
        public string? tcKimlikNo { get; set; }
        public Int32? i_stajBirimiKimlik { get; set; }
        public string? stajyerAdi { get; set; }
        public string? stajyerSoyadi { get; set; }
        public Int32? sinifi { get; set; }
        public string? telefon { get; set; }
        public string? ePosta { get; set; }
        public bool? varmi { get; set; }
        public StajyerArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<Stajyer> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<Stajyer>(P => P.varmi == true);
            if (stajyerkimlik != null)
                predicate = predicate.And(x => x.stajyerkimlik == stajyerkimlik);
            if (ogrenciNo != null)
                predicate = predicate.And(x => x.ogrenciNo != null && x.ogrenciNo.Contains(ogrenciNo));
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo != null && x.tcKimlikNo.Contains(tcKimlikNo));
            if (i_stajBirimiKimlik != null)
                predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik);
            if (stajyerAdi != null)
                predicate = predicate.And(x => x.stajyerAdi != null && x.stajyerAdi.Contains(stajyerAdi));
            if (stajyerSoyadi != null)
                predicate = predicate.And(x => x.stajyerSoyadi != null && x.stajyerSoyadi.Contains(stajyerSoyadi));
            if (sinifi != null)
                predicate = predicate.And(x => x.sinifi == sinifi);
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<Stajyer>> cek(veri.Varlik vari)
        {
            List<Stajyer> sonuc = await vari.Stajyers
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<Stajyer?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            Stajyer? sonuc = await vari.Stajyers
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajyerCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Stajyer>> ara(params Expression<Func<Stajyer, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Stajyer>> ara(veri.Varlik vari, params Expression<Func<Stajyer, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Stajyers
                            .Where(kosul).OrderByDescending(p => p.stajyerkimlik)
                   .ToListAsync();
        }



        public static async Task<Stajyer?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Stajyer? kayit = await kime.Stajyers.FirstOrDefaultAsync(p => p.stajyerkimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Stajyer yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajyerkimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Stajyers.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Stajyer? bulunan = await vari.Stajyers.FirstOrDefaultAsync(p => p.stajyerkimlik == yeni.stajyerkimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Stajyer kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Stajyers.FirstOrDefaultAsync(p => p.stajyerkimlik == kimi.stajyerkimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Stajyer? tekliCek(Int32 kimlik, Varlik kime)
        {
            Stajyer? kayit = kime.Stajyers.FirstOrDefault(p => p.stajyerkimlik == kimlik);
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
        public static void kaydet(Stajyer yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajyerkimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Stajyers.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Stajyers.FirstOrDefault(p => p.stajyerkimlik == yeni.stajyerkimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Stajyer kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Stajyers.FirstOrDefault(p => p.stajyerkimlik == kimi.stajyerkimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


