using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class RolWebSayfasiIzniArama
    {
        public Int64? rolWebSayfasiIzniKimlik { get; set; }
        public Int32? i_rolKimlik { get; set; }
        public Int32? i_webSayfasiKimlik { get; set; }
        public bool? e_gormeIzniVarmi { get; set; }
        public bool? e_eklemeIzniVarmi { get; set; }
        public bool? e_silmeIzniVarmi { get; set; }
        public bool? e_guncellemeIzniVarmi { get; set; }
        public bool? varmi { get; set; }
        public RolWebSayfasiIzniArama()
        {
            varmi = true;
        }
        public async Task<List<RolWebSayfasiIzni>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<RolWebSayfasiIzni>(P => P.varmi == true);
            if (rolWebSayfasiIzniKimlik != null)
                predicate = predicate.And(x => x.rolWebSayfasiIzniKimlik == rolWebSayfasiIzniKimlik);
            if (i_rolKimlik != null)
                predicate = predicate.And(x => x.i_rolKimlik == i_rolKimlik);
            if (i_webSayfasiKimlik != null)
                predicate = predicate.And(x => x.i_webSayfasiKimlik == i_webSayfasiKimlik);
            if (e_gormeIzniVarmi != null)
                predicate = predicate.And(x => x.e_gormeIzniVarmi == e_gormeIzniVarmi);
            if (e_eklemeIzniVarmi != null)
                predicate = predicate.And(x => x.e_eklemeIzniVarmi == e_eklemeIzniVarmi);
            if (e_silmeIzniVarmi != null)
                predicate = predicate.And(x => x.e_silmeIzniVarmi == e_silmeIzniVarmi);
            if (e_guncellemeIzniVarmi != null)
                predicate = predicate.And(x => x.e_guncellemeIzniVarmi == e_guncellemeIzniVarmi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<RolWebSayfasiIzni> sonuc = new List<RolWebSayfasiIzni>();
            sonuc = await vari.RolWebSayfasiIznis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }
    }
    public class RolWebSayfasiIzniCizelgesi
    {
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<RolWebSayfasiIzni> ara(params Predicate<RolWebSayfasiIzni>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.RolWebSayfasiIznis.ToList().FindAll(kosul).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList();
            }
        }
        public static List<RolWebSayfasiIzni> tamami(Varlik kime)
        {
            return kime.RolWebSayfasiIznis.Where(p => p.varmi == true).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList();
        }
        public static async Task<RolWebSayfasiIzni?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            RolWebSayfasiIzni? kayit = await kime.RolWebSayfasiIznis.FirstOrDefaultAsync(p => p.rolWebSayfasiIzniKimlik == kimlik && p.varmi == true);
            return kayit;
        }
        public static async Task kaydetKos(RolWebSayfasiIzni yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.rolWebSayfasiIzniKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.RolWebSayfasiIznis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                RolWebSayfasiIzni? bulunan = await vari.RolWebSayfasiIznis.FirstOrDefaultAsync(p => p.rolWebSayfasiIzniKimlik == yeni.rolWebSayfasiIzniKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }
        public static async Task silKos(RolWebSayfasiIzni kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.RolWebSayfasiIznis.FirstOrDefaultAsync(p => p.rolWebSayfasiIzniKimlik == kimi.rolWebSayfasiIzniKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }
        public static RolWebSayfasiIzni tekliCek(Int64 kimlik, Varlik kime)
        {
            RolWebSayfasiIzni kayit = kime.RolWebSayfasiIznis.FirstOrDefault(p => p.rolWebSayfasiIzniKimlik == kimlik);
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
        public static void kaydet(RolWebSayfasiIzni yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.rolWebSayfasiIzniKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.RolWebSayfasiIznis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.RolWebSayfasiIznis.FirstOrDefault(p => p.rolWebSayfasiIzniKimlik == yeni.rolWebSayfasiIzniKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(RolWebSayfasiIzni kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.RolWebSayfasiIznis.FirstOrDefault(p => p.rolWebSayfasiIzniKimlik == kimi.rolWebSayfasiIzniKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
