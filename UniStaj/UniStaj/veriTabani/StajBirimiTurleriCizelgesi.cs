using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UniStaj.veri;

namespace UniStaj.veriTabani
{

    public class StajBirimiTurleriArama
    {
        public Int32? stajBirimiTurlerikimlik { get; set; }
        public Int32? i_stajBirimiKimlik { get; set; }
        public Int32? i_stajTuruKimlik { get; set; }
        public Int32? gunu { get; set; }
        public string? siniflari { get; set; }
        public string? ekAciklama { get; set; }
        public bool? varmi { get; set; }
        public StajBirimiTurleriArama()
        {
            this.varmi = true;
        }

        private ExpressionStarter<StajBirimiTurleri> kosulOlustur()
        {
            var predicate = PredicateBuilder.New<StajBirimiTurleri>(P => P.varmi == true);
            if (stajBirimiTurlerikimlik != null)
                predicate = predicate.And(x => x.stajBirimiTurlerikimlik == stajBirimiTurlerikimlik);
            if (i_stajBirimiKimlik != null)
                predicate = predicate.And(x => x.i_stajBirimiKimlik == i_stajBirimiKimlik);
            if (i_stajTuruKimlik != null)
                predicate = predicate.And(x => x.i_stajTuruKimlik == i_stajTuruKimlik);
            if (gunu != null)
                predicate = predicate.And(x => x.gunu == gunu);
            if (siniflari != null)
                predicate = predicate.And(x => x.siniflari != null && x.siniflari.Contains(siniflari));
            if (ekAciklama != null)
                predicate = predicate.And(x => x.ekAciklama != null && x.ekAciklama.Contains(ekAciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            return predicate;

        }
        public async Task<List<StajBirimiTurleri>> cek(veri.Varlik vari)
        {
            List<StajBirimiTurleri> sonuc = await vari.StajBirimiTurleris
           .Where(kosulOlustur())
           .ToListAsync();
            return sonuc;
        }
        public async Task<StajBirimiTurleri?> bul(veri.Varlik vari)
        {
            var predicate = kosulOlustur();
            StajBirimiTurleri? sonuc = await vari.StajBirimiTurleris
           .Where(predicate)
           .FirstOrDefaultAsync();
            return sonuc;
        }
    }


    public class StajBirimiTurleriCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<StajBirimiTurleri>> ara(params Expression<Func<StajBirimiTurleri, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<StajBirimiTurleri>> ara(veri.Varlik vari, params Expression<Func<StajBirimiTurleri, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.StajBirimiTurleris
                            .Where(kosul).OrderByDescending(p => p.stajBirimiTurlerikimlik)
                   .ToListAsync();
        }



        public static async Task<StajBirimiTurleri?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            StajBirimiTurleri? kayit = await kime.StajBirimiTurleris.FirstOrDefaultAsync(p => p.stajBirimiTurlerikimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(StajBirimiTurleri yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBirimiTurlerikimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.StajBirimiTurleris.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                StajBirimiTurleri? bulunan = await vari.StajBirimiTurleris.FirstOrDefaultAsync(p => p.stajBirimiTurlerikimlik == yeni.stajBirimiTurlerikimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(StajBirimiTurleri kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.StajBirimiTurleris.FirstOrDefaultAsync(p => p.stajBirimiTurlerikimlik == kimi.stajBirimiTurlerikimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static StajBirimiTurleri? tekliCek(Int32 kimlik, Varlik kime)
        {
            StajBirimiTurleri? kayit = kime.StajBirimiTurleris.FirstOrDefault(p => p.stajBirimiTurlerikimlik == kimlik);
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
        public static void kaydet(StajBirimiTurleri yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.stajBirimiTurlerikimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.StajBirimiTurleris.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.StajBirimiTurleris.FirstOrDefault(p => p.stajBirimiTurlerikimlik == yeni.stajBirimiTurlerikimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(StajBirimiTurleri kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.StajBirimiTurleris.FirstOrDefault(p => p.stajBirimiTurlerikimlik == kimi.stajBirimiTurlerikimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


