using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class UlkeArama
    {
        public Int32? ulkeKimlik { get; set; }
        public string? ulkeAdi { get; set; }
        public string? ingilizceAdi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public string? kisaltmasi { get; set; }
        public UlkeArama()
        {
            this.varmi = true;
        }

        public async Task<List<Ulke>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Ulke>(P => P.varmi == true);
            if (ulkeKimlik != null)
                predicate = predicate.And(x => x.ulkeKimlik == ulkeKimlik);
            if (ulkeAdi != null)
                predicate = predicate.And(x => x.ulkeAdi != null && x.ulkeAdi.Contains(ulkeAdi));
            if (ingilizceAdi != null)
                predicate = predicate.And(x => x.ingilizceAdi != null && x.ingilizceAdi.Contains(ingilizceAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (kisaltmasi != null)
                predicate = predicate.And(x => x.kisaltmasi != null && x.kisaltmasi.Contains(kisaltmasi));
            List<Ulke> sonuc = new List<Ulke>();
            sonuc = await vari.Ulkes
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class UlkeCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<Ulke>> ara(params Expression<Func<Ulke, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<Ulke>> ara(veri.Varlik vari, params Expression<Func<Ulke, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.Ulkes
                            .Where(kosul).OrderByDescending(p => p.ulkeKimlik)
                   .ToListAsync();
        }



        public static async Task<Ulke?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Ulke? kayit = await kime.Ulkes.FirstOrDefaultAsync(p => p.ulkeKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Ulke yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.ulkeKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Ulkes.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Ulke? bulunan = await vari.Ulkes.FirstOrDefaultAsync(p => p.ulkeKimlik == yeni.ulkeKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Ulke kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Ulkes.FirstOrDefaultAsync(p => p.ulkeKimlik == kimi.ulkeKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Ulke? tekliCek(Int32 kimlik, Varlik kime)
        {
            Ulke? kayit = kime.Ulkes.FirstOrDefault(p => p.ulkeKimlik == kimlik);
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
        public static void kaydet(Ulke yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.ulkeKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Ulkes.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Ulkes.FirstOrDefault(p => p.ulkeKimlik == yeni.ulkeKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Ulke kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Ulkes.FirstOrDefault(p => p.ulkeKimlik == kimi.ulkeKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


