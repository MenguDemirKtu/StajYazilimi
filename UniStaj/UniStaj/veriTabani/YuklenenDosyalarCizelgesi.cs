using LinqKit;
using Microsoft.EntityFrameworkCore; // // using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class YuklenenDosyalarArama
    {
        public Int64? yuklenenDosyalarKimlik { get; set; }
        public string? dosyaKonumu { get; set; }
        public string? ilgiliCizelgeAdi { get; set; }
        public Int64? ilgiliCizelgeKimlik { get; set; }
        public DateTime? yuklemeTarihi { get; set; }
        public bool? varmi { get; set; }
        public string? dosyaUzantisi { get; set; }
        public Int32? i_dosyaTuruKimlik { get; set; }
        public string? kodu { get; set; }
        public YuklenenDosyalarArama()
        {
            varmi = true;
        }

        public async Task<List<YuklenenDosyalar>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<YuklenenDosyalar>(P => P.varmi == true);
            if (yuklenenDosyalarKimlik != null)
                predicate = predicate.And(x => x.yuklenenDosyalarKimlik == yuklenenDosyalarKimlik);
            if (dosyaKonumu != null)
                predicate = predicate.And(x => x.dosyaKonumu.Contains(dosyaKonumu));
            if (ilgiliCizelgeAdi != null)
                predicate = predicate.And(x => x.ilgiliCizelgeAdi.Contains(ilgiliCizelgeAdi));
            if (ilgiliCizelgeKimlik != null)
                predicate = predicate.And(x => x.ilgiliCizelgeKimlik == ilgiliCizelgeKimlik);
            if (yuklemeTarihi != null)
                predicate = predicate.And(x => x.yuklemeTarihi == yuklemeTarihi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (dosyaUzantisi != null)
                predicate = predicate.And(x => x.dosyaUzantisi.Contains(dosyaUzantisi));
            if (i_dosyaTuruKimlik != null)
                predicate = predicate.And(x => x.i_dosyaTuruKimlik == i_dosyaTuruKimlik);
            if (kodu != null)
                predicate = predicate.And(x => x.kodu.Contains(kodu));
            List<YuklenenDosyalar> sonuc = new List<YuklenenDosyalar>();
            sonuc = await vari.YuklenenDosyalars
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class YuklenenDosyalarCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<YuklenenDosyalar> ara(params Predicate<YuklenenDosyalar>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.YuklenenDosyalars.ToList().FindAll(kosul).OrderByDescending(p => p.yuklenenDosyalarKimlik).ToList();
            }
        }


        public static List<YuklenenDosyalar> tamami(Varlik kime)
        {
            return kime.YuklenenDosyalars.Where(p => p.varmi == true).OrderByDescending(p => p.yuklenenDosyalarKimlik).ToList();
        }
        public static async Task<YuklenenDosyalar?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            YuklenenDosyalar? kayit = await kime.YuklenenDosyalars.FirstOrDefaultAsync(p => p.yuklenenDosyalarKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(YuklenenDosyalar yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.yuklenenDosyalarKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.YuklenenDosyalars.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                YuklenenDosyalar? bulunan = await vari.YuklenenDosyalars.FirstOrDefaultAsync(p => p.yuklenenDosyalarKimlik == yeni.yuklenenDosyalarKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(YuklenenDosyalar kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.YuklenenDosyalars.FirstOrDefaultAsync(p => p.yuklenenDosyalarKimlik == kimi.yuklenenDosyalarKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static YuklenenDosyalar tekliCek(Int64 kimlik, Varlik kime)
        {
            YuklenenDosyalar kayit = kime.YuklenenDosyalars.FirstOrDefault(p => p.yuklenenDosyalarKimlik == kimlik);
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
        public static void kaydet(YuklenenDosyalar yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.yuklenenDosyalarKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.YuklenenDosyalars.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.YuklenenDosyalars.FirstOrDefault(p => p.yuklenenDosyalarKimlik == yeni.yuklenenDosyalarKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(YuklenenDosyalar kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.YuklenenDosyalars.FirstOrDefault(p => p.yuklenenDosyalarKimlik == kimi.yuklenenDosyalarKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


