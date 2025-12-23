using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajDonemiArama
    {
        public Int32? stajDonemikimlik { get; set; }
        public string? stajDonemAdi { get; set; }
        public string? tanim { get; set; }
        public DateTime? baslangic { get; set; }
        public DateTime? bitis { get; set; }
        public bool? e_gecerliDonemmi { get; set; }
        public bool? varmi { get; set; }
        public StajDonemiArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajDonemi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajDonemi>(P => P.varmi == true);
            if (stajDonemikimlik != null)
                predicate = predicate.And(x => x.stajDonemikimlik == stajDonemikimlik);
            if (stajDonemAdi != null)
                predicate = predicate.And(x => x.stajDonemAdi != null && x.stajDonemAdi.Contains(stajDonemAdi));
            if (tanim != null)
                predicate = predicate.And(x => x.tanim != null && x.tanim.Contains(tanim));
            if (baslangic != null)
                predicate = predicate.And(x => x.baslangic == baslangic);
            if (bitis != null)
                predicate = predicate.And(x => x.bitis == bitis);
            if (e_gecerliDonemmi != null)
                predicate = predicate.And(x => x.e_gecerliDonemmi == e_gecerliDonemmi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajDonemi>> cek(veri.Varlik vari)
        {
            List<StajDonemi> sonuc = await vari.StajDonemis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajDonemi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajDonemi? sonuc = await vari.StajDonemis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }

    public class StajDonemiCizelgesi
    {
        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajDonemi>> ara(params Expression<Func<StajDonemi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajDonemi>> ara(veri.Varlik vari, params Expression<Func<StajDonemi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajDonemis
                            .Where(kosul).OrderByDescending(p => p.stajDonemikimlik)
                   .ToListAsync();
        }



        public static async Task<StajDonemi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajDonemi? kayit = await kime.StajDonemis.FirstOrDefaultAsync(p => p.stajDonemikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajDonemi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajDonemikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajDonemis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajDonemi? bulunan = await vari.StajDonemis.FirstOrDefaultAsync(p => p.stajDonemikimlik == yeni.stajDonemikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajDonemi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajDonemis.FirstOrDefaultAsync(p => p.stajDonemikimlik == kimi.stajDonemikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajDonemi? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajDonemi? kayit = kime.StajDonemis.FirstOrDefault(p => p.stajDonemikimlik == kimlik);
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
        public static void kaydet(StajDonemi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajDonemikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajDonemis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajDonemis.FirstOrDefault(p => p.stajDonemikimlik == yeni.stajDonemikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajDonemi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajDonemis.FirstOrDefault(p => p.stajDonemikimlik == kimi.stajDonemikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


