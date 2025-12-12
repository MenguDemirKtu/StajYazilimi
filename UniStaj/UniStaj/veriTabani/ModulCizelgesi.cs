using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ModulArama
    {
        public Int32? modulKimlik { get; set; }
        public string? modulAdi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public bool? varmi { get; set; }
        public string? ikonAdi { get; set; }
        public Int32? sirasi { get; set; }
        public ModulArama()
        {
            varmi = true;
        }

        public async Task<List<Modul>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Modul>(P => P.varmi == true);
            if (modulKimlik != null)
                predicate = predicate.And(x => x.modulKimlik == modulKimlik);
            if (modulAdi != null)
                predicate = predicate.And(x => x.modulAdi.Contains(modulAdi));
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (ikonAdi != null)
                predicate = predicate.And(x => x.ikonAdi.Contains(ikonAdi));
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            List<Modul> sonuc = new List<Modul>();
            sonuc = await vari.Moduls
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class ModulCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Modul> ara(params Predicate<Modul>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.Moduls.ToList().FindAll(kosul).OrderByDescending(p => p.modulKimlik).ToList();
            }
        }


        public static List<Modul> tamami(Varlik kime)
        {
            return kime.Moduls.Where(p => p.varmi == true).OrderByDescending(p => p.modulKimlik).ToList();
        }
        public static async Task<Modul?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Modul? kayit = await kime.Moduls.FirstOrDefaultAsync(p => p.modulKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Modul yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.modulKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Moduls.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Modul? bulunan = await vari.Moduls.FirstOrDefaultAsync(p => p.modulKimlik == yeni.modulKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(Modul kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Moduls.FirstOrDefaultAsync(p => p.modulKimlik == kimi.modulKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static Modul tekliCek(Int32 kimlik, Varlik kime)
        {
            Modul kayit = kime.Moduls.FirstOrDefault(p => p.modulKimlik == kimlik);
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
        public static void kaydet(Modul yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.modulKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Moduls.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Moduls.FirstOrDefault(p => p.modulKimlik == yeni.modulKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Modul kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Moduls.FirstOrDefault(p => p.modulKimlik == kimi.modulKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


