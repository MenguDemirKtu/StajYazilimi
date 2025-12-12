using Microsoft.EntityFrameworkCore; //;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SosyalMedyaArama
    {
        public Int32? sosyalMedyakimlik { get; set; }
        public string? sosyamMedyaAdi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public string? adres { get; set; }
        public Int32? sirasi { get; set; }
        public bool? varmi { get; set; }
        public SosyalMedyaArama()
        {
            this.varmi = true;
        }

        public async Task<List<SosyalMedya>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SosyalMedya>(P => P.varmi == true);
            if (sosyalMedyakimlik != null)
                predicate = predicate.And(x => x.sosyalMedyakimlik == sosyalMedyakimlik);
            if (sosyamMedyaAdi != null)
                predicate = predicate.And(x => x.sosyamMedyaAdi != null && x.sosyamMedyaAdi.Contains(sosyamMedyaAdi));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (adres != null)
                predicate = predicate.And(x => x.adres != null && x.adres.Contains(adres));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SosyalMedya> sonuc = new List<SosyalMedya>();
            sonuc = await vari.SosyalMedyas
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SosyalMedyaCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SosyalMedya> ara(params Predicate<SosyalMedya>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SosyalMedyas.ToList().FindAll(kosul).OrderByDescending(p => p.sosyalMedyakimlik).ToList();
            }
        }


        public static List<SosyalMedya> tamami(Varlik kime)
        {
            return kime.SosyalMedyas.Where(p => p.varmi == true).OrderByDescending(p => p.sosyalMedyakimlik).ToList();
        }
        public static async Task<SosyalMedya?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SosyalMedya? kayit = await kime.SosyalMedyas.FirstOrDefaultAsync(p => p.sosyalMedyakimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(SosyalMedya yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.sosyalMedyakimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.SosyalMedyas.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                SosyalMedya? bulunan = await vari.SosyalMedyas.FirstOrDefaultAsync(p => p.sosyalMedyakimlik == yeni.sosyalMedyakimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(SosyalMedya kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.SosyalMedyas.FirstOrDefaultAsync(p => p.sosyalMedyakimlik == kimi.sosyalMedyakimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static SosyalMedya? tekliCek(Int32 kimlik, Varlik kime)
        {
            SosyalMedya? kayit = kime.SosyalMedyas.FirstOrDefault(p => p.sosyalMedyakimlik == kimlik);
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
        public static void kaydet(SosyalMedya yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sosyalMedyakimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SosyalMedyas.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.SosyalMedyas.FirstOrDefault(p => p.sosyalMedyakimlik == yeni.sosyalMedyakimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(SosyalMedya kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SosyalMedyas.FirstOrDefault(p => p.sosyalMedyakimlik == kimi.sosyalMedyakimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


