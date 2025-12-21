using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBirimAsamasiArama
    {
        public Int32? stajBirimAsamasikimlik { get; set; }
        public Int32? i_stajBirimiKimlik { get; set; }
        public Int32? i_stajAsamasiKimlik { get; set; }
        public Int32? sirasi { get; set; }
        public bool? e_gecerliMi { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public StajBirimAsamasiArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBirimAsamasi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBirimAsamasi>(P => P.varmi == true);
            if (stajBirimAsamasikimlik != null)
                predicate = predicate.And(x => x.stajBirimAsamasikimlik == stajBirimAsamasikimlik);
            if (i_stajBirimiKimlik != null)
                predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik);
            if (i_stajAsamasiKimlik != null)
                predicate = predicate.And(x => x.i_stajAsamasiKimlik == i_stajAsamasiKimlik);
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (e_gecerliMi != null)
                predicate = predicate.And(x => x.e_gecerliMi == e_gecerliMi);
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama != null && x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajBirimAsamasi>> cek(veri.Varlik vari)
        {
            List<StajBirimAsamasi> sonuc = await vari.StajBirimAsamasis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBirimAsamasi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBirimAsamasi? sonuc = await vari.StajBirimAsamasis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBirimAsamasiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBirimAsamasi>> ara(params Expression<Func<StajBirimAsamasi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBirimAsamasi>> ara(veri.Varlik vari, params Expression<Func<StajBirimAsamasi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBirimAsamasis
                            .Where(kosul).OrderByDescending(p => p.stajBirimAsamasikimlik)
                   .ToListAsync();
        }



        public static async Task<StajBirimAsamasi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBirimAsamasi? kayit = await kime.StajBirimAsamasis.FirstOrDefaultAsync(p => p.stajBirimAsamasikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajBirimAsamasi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBirimAsamasikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajBirimAsamasis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajBirimAsamasi? bulunan = await vari.StajBirimAsamasis.FirstOrDefaultAsync(p => p.stajBirimAsamasikimlik == yeni.stajBirimAsamasikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajBirimAsamasi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajBirimAsamasis.FirstOrDefaultAsync(p => p.stajBirimAsamasikimlik == kimi.stajBirimAsamasikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajBirimAsamasi? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBirimAsamasi? kayit = kime.StajBirimAsamasis.FirstOrDefault(p => p.stajBirimAsamasikimlik == kimlik);
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
        public static void kaydet(StajBirimAsamasi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBirimAsamasikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajBirimAsamasis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajBirimAsamasis.FirstOrDefault(p => p.stajBirimAsamasikimlik == yeni.stajBirimAsamasikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajBirimAsamasi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajBirimAsamasis.FirstOrDefault(p => p.stajBirimAsamasikimlik == kimi.stajBirimAsamasikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


