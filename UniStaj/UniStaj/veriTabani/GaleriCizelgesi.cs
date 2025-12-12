using Microsoft.EntityFrameworkCore; //;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class GaleriArama
    {
        public Int32? galeriKimlik { get; set; }
        public string? galeriAdi { get; set; }
        public string? galeriUrl { get; set; }
        public string? ilgiliCizelge { get; set; }
        public Int64? ilgiliKimlik { get; set; }
        public Int32? genislik { get; set; }
        public Int32? yukseklik { get; set; }
        public bool? varmi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public GaleriArama()
        {
            this.varmi = true;
        }

        public async Task<List<Galeri>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Galeri>(P => P.varmi == true);
            if (galeriKimlik != null)
                predicate = predicate.And(x => x.galeriKimlik == galeriKimlik);
            if (galeriAdi != null)
                predicate = predicate.And(x => x.galeriAdi != null && x.galeriAdi.Contains(galeriAdi));
            if (galeriUrl != null)
                predicate = predicate.And(x => x.galeriUrl != null && x.galeriUrl.Contains(galeriUrl));
            if (ilgiliCizelge != null)
                predicate = predicate.And(x => x.ilgiliCizelge != null && x.ilgiliCizelge.Contains(ilgiliCizelge));
            if (ilgiliKimlik != null)
                predicate = predicate.And(x => x.ilgiliKimlik == ilgiliKimlik);
            if (genislik != null)
                predicate = predicate.And(x => x.genislik == genislik);
            if (yukseklik != null)
                predicate = predicate.And(x => x.yukseklik == yukseklik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            List<Galeri> sonuc = new List<Galeri>();
            sonuc = await vari.Galeris
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class GaleriCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Galeri> ara(params Predicate<Galeri>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.Galeris.ToList().FindAll(kosul).OrderByDescending(p => p.galeriKimlik).ToList();
            }
        }


        public static List<Galeri> tamami(Varlik kime)
        {
            return kime.Galeris.Where(p => p.varmi == true).OrderByDescending(p => p.galeriKimlik).ToList();
        }
        public static async Task<Galeri?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Galeri? kayit = await kime.Galeris.FirstOrDefaultAsync(p => p.galeriKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Galeri yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.galeriKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Galeris.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Galeri? bulunan = await vari.Galeris.FirstOrDefaultAsync(p => p.galeriKimlik == yeni.galeriKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(Galeri kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Galeris.FirstOrDefaultAsync(p => p.galeriKimlik == kimi.galeriKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static Galeri? tekliCek(Int32 kimlik, Varlik kime)
        {
            Galeri? kayit = kime.Galeris.FirstOrDefault(p => p.galeriKimlik == kimlik);
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
        public static void kaydet(Galeri yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.galeriKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Galeris.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Galeris.FirstOrDefault(p => p.galeriKimlik == yeni.galeriKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Galeri kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Galeris.FirstOrDefault(p => p.galeriKimlik == kimi.galeriKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


