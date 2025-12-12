using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class IlceArama
    {
        public Int32? ilcekimlik { get; set; }
        public string? ilceAdi { get; set; }
        public Int32? i_sehirKimlik { get; set; }
        public bool? varmi { get; set; }
        public IlceArama()
        {
            this.varmi = true;
        }

        public async Task<List<Ilce>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Ilce>(P => P.varmi == true);
            if (ilcekimlik != null)
                predicate = predicate.And(x => x.ilcekimlik == ilcekimlik);
            if (ilceAdi != null)
                predicate = predicate.And(x => x.ilceAdi != null && x.ilceAdi.Contains(ilceAdi));
            if (i_sehirKimlik != null)
                predicate = predicate.And(x => x.i_sehirKimlik == i_sehirKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<Ilce> sonuc = new List<Ilce>();
            sonuc = await vari.Ilces
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }
        public async Task<Ilce?> bul(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Ilce>(P => P.varmi == true);
            if (ilcekimlik != null)
                predicate = predicate.And(x => x.ilcekimlik == ilcekimlik);
            if (ilceAdi != null)
                predicate = predicate.And(x => x.ilceAdi != null && x.ilceAdi.Contains(ilceAdi));
            if (i_sehirKimlik != null)
                predicate = predicate.And(x => x.i_sehirKimlik == i_sehirKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            Ilce? sonuc = await vari.Ilces
          .Where(predicate)
          .FirstOrDefaultAsync();
            return sonuc;
        }




    }


    public class IlceCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Ilce>> ara(params Expression<Func<Ilce, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Ilce>> ara(veri.Varlik vari, params Expression<Func<Ilce, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Ilces
                            .Where(kosul).OrderByDescending(p => p.ilcekimlik)
                   .ToListAsync();
        }



        public static async Task<Ilce?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Ilce? kayit = await kime.Ilces.FirstOrDefaultAsync(p => p.ilcekimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Ilce yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.ilcekimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Ilces.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Ilce? bulunan = await vari.Ilces.FirstOrDefaultAsync(p => p.ilcekimlik == yeni.ilcekimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Ilce kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Ilces.FirstOrDefaultAsync(p => p.ilcekimlik == kimi.ilcekimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Ilce? tekliCek(Int32 kimlik, Varlik kime)
        {
            Ilce? kayit = kime.Ilces.FirstOrDefault(p => p.ilcekimlik == kimlik);
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
        public static void kaydet(Ilce yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.ilcekimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Ilces.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Ilces.FirstOrDefault(p => p.ilcekimlik == yeni.ilcekimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Ilce kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Ilces.FirstOrDefault(p => p.ilcekimlik == kimi.ilcekimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


