using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class AramaTalebiArama
    {
        public Int32? aramaTalebiKimlik { get; set; }
        public string? kodu { get; set; }
        public DateTime? tarih { get; set; }
        public string? talepAyrintisi { get; set; }
        public bool? varmi { get; set; }
        public AramaTalebiArama()
        {
            varmi = true;
        }

        public async Task<List<AramaTalebi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<AramaTalebi>(P => P.varmi == true);
            if (aramaTalebiKimlik != null)
                predicate = predicate.And(x => x.aramaTalebiKimlik == aramaTalebiKimlik);
            if (kodu != null)
                predicate = predicate.And(x => x.kodu.Contains(kodu));
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (talepAyrintisi != null)
                predicate = predicate.And(x => x.talepAyrintisi.Contains(talepAyrintisi));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<AramaTalebi> sonuc = new List<AramaTalebi>();
            sonuc = await vari.AramaTalebis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class AramaTalebiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<AramaTalebi> ara(params Predicate<AramaTalebi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.AramaTalebis.ToList().FindAll(kosul).OrderByDescending(p => p.aramaTalebiKimlik).ToList();
            }
        }


        public static List<AramaTalebi> tamami(Varlik kime)
        {
            return kime.AramaTalebis.Where(p => p.varmi == true).OrderByDescending(p => p.aramaTalebiKimlik).ToList();
        }
        public static async Task<AramaTalebi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            AramaTalebi? kayit = await kime.AramaTalebis.FirstOrDefaultAsync(p => p.aramaTalebiKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(AramaTalebi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.aramaTalebiKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.AramaTalebis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                AramaTalebi? bulunan = await vari.AramaTalebis.FirstOrDefaultAsync(p => p.aramaTalebiKimlik == yeni.aramaTalebiKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(AramaTalebi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.AramaTalebis.FirstOrDefaultAsync(p => p.aramaTalebiKimlik == kimi.aramaTalebiKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static AramaTalebi tekliCek(Int32 kimlik, Varlik kime)
        {
            AramaTalebi kayit = kime.AramaTalebis.FirstOrDefault(p => p.aramaTalebiKimlik == kimlik);
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
        public static void kaydet(AramaTalebi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.aramaTalebiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.AramaTalebis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.AramaTalebis.FirstOrDefault(p => p.aramaTalebiKimlik == yeni.aramaTalebiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(AramaTalebi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.AramaTalebis.FirstOrDefault(p => p.aramaTalebiKimlik == kimi.aramaTalebiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


