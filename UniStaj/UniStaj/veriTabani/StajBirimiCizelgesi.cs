using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBirimiArama
    {
        public Int32? stajBirimikimlik { get; set; }
        public string? stajBirimAdi { get; set; }
        public string? telefon { get; set; }
        public string? ePosta { get; set; }
        public string? birimSorumluAdi { get; set; }
        public bool? e_altBirimMi { get; set; }
        public Int32? i_ustStajBirimiKimlik { get; set; }
        public bool? varmi { get; set; }
        public StajBirimiArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBirimi> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBirimi>(P => P.varmi == true);
            if (stajBirimikimlik != null)
                predicate = predicate.And(x => x.stajBirimikimlik == stajBirimikimlik);
            if (stajBirimAdi != null)
                predicate = predicate.And(x => x.stajBirimAdi != null && x.stajBirimAdi.Contains(stajBirimAdi));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon != null && x.telefon.Contains(telefon));
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta != null && x.ePosta.Contains(ePosta));
            if (birimSorumluAdi != null)
                predicate = predicate.And(x => x.birimSorumluAdi != null && x.birimSorumluAdi.Contains(birimSorumluAdi));
            if (e_altBirimMi != null)
                predicate = predicate.And(x => x.e_altBirimMi == e_altBirimMi);
            if (i_ustStajBirimiKimlik != null)
                predicate = predicate.And(x => x.i_ustStajBirimiKimlik == i_ustStajBirimiKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajBirimi>> cek(veri.Varlik vari)
        {
            List<StajBirimi> sonuc = await vari.StajBirimis
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBirimi?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBirimi? sonuc = await vari.StajBirimis
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBirimiCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBirimi>> ara(params Expression<Func<StajBirimi, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBirimi>> ara(veri.Varlik vari, params Expression<Func<StajBirimi, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBirimis
                            .Where(kosul).OrderByDescending(p => p.stajBirimikimlik)
                   .ToListAsync();
        }



        public static async Task<StajBirimi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBirimi? kayit = await kime.StajBirimis.FirstOrDefaultAsync(p => p.stajBirimikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajBirimi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBirimikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajBirimis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajBirimi? bulunan = await vari.StajBirimis.FirstOrDefaultAsync(p => p.stajBirimikimlik == yeni.stajBirimikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajBirimi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajBirimis.FirstOrDefaultAsync(p => p.stajBirimikimlik == kimi.stajBirimikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajBirimi? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBirimi? kayit = kime.StajBirimis.FirstOrDefault(p => p.stajBirimikimlik == kimlik);
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
        public static void kaydet(StajBirimi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBirimikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajBirimis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajBirimis.FirstOrDefault(p => p.stajBirimikimlik == yeni.stajBirimikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajBirimi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajBirimis.FirstOrDefault(p => p.stajBirimikimlik == kimi.stajBirimikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


