using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class BysMenuArama
    {
        public Int32? bysMenuKimlik { get; set; }
        public string? bysMenuAdi { get; set; }
        public string? bysMenuBicim { get; set; }
        public Int32? i_ustMenuKimlik { get; set; }
        public Int32? i_webSayfasiKimlik { get; set; }
        public string? bysMenuUrl { get; set; }
        public bool? varmi { get; set; }
        public Int32? sirasi { get; set; }
        public bool? e_modulSayfasimi { get; set; }
        public Int32? i_modulKimlik { get; set; }
        public bool? e_gosterilsimmi { get; set; }
        public BysMenuArama()
        {
            varmi = true;
        }
        public async Task<List<BysMenu>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<BysMenu>(P => P.varmi == true);
            if (bysMenuKimlik != null)
                predicate = predicate.And(x => x.bysMenuKimlik == bysMenuKimlik);
            if (bysMenuAdi != null)
                predicate = predicate.And(x => x.bysMenuAdi != null && x.bysMenuAdi.Contains(bysMenuAdi));
            if (bysMenuBicim != null)
                predicate = predicate.And(x => x.bysMenuBicim != null && x.bysMenuBicim.Contains(bysMenuBicim));
            if (i_ustMenuKimlik != null)
                predicate = predicate.And(x => x.i_ustMenuKimlik == i_ustMenuKimlik);
            if (i_webSayfasiKimlik != null)
                predicate = predicate.And(x => x.i_webSayfasiKimlik == i_webSayfasiKimlik);
            if (bysMenuUrl != null)
                predicate = predicate.And(x => x.bysMenuUrl != null && x.bysMenuUrl.Contains(bysMenuUrl));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (sirasi != null)
                predicate = predicate.And(x => x.sirasi == sirasi);
            if (e_modulSayfasimi != null)
                predicate = predicate.And(x => x.e_modulSayfasimi == e_modulSayfasimi);
            if (i_modulKimlik != null)
                predicate = predicate.And(x => x.i_modulKimlik == i_modulKimlik);
            if (e_gosterilsimmi != null)
                predicate = predicate.And(x => x.e_gosterilsimmi == e_gosterilsimmi);
            List<BysMenu> sonuc = new List<BysMenu>();
            sonuc = await vari.BysMenus
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }
    }
    public class BysMenuCizelgesi
    {
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<BysMenu> ara(params Predicate<BysMenu>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.BysMenus.ToList().FindAll(kosul).OrderByDescending(p => p.bysMenuKimlik).ToList();
            }
        }
        public static List<BysMenu> tamami(Varlik kime)
        {
            return kime.BysMenus.Where(p => p.varmi == true).OrderByDescending(p => p.bysMenuKimlik).ToList();
        }
        public static async Task<BysMenu?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            BysMenu? kayit = await kime.BysMenus.FirstOrDefaultAsync(p => p.bysMenuKimlik == kimlik && p.varmi == true);
            return kayit;
        }
        public static async Task kaydetKos(BysMenu yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (string.IsNullOrEmpty(yeni.bysMenuUrl) == false)
            {
                yeni.bysMenuUrl = yeni.bysMenuUrl.ToLower();
                yeni.bysMenuUrl = yeni.bysMenuUrl.Replace("ı", "i");
                yeni.bysMenuUrl = yeni.bysMenuUrl.Replace(" ", "");
                yeni.bysMenuUrl = yeni.bysMenuUrl.Trim();
            }

            if (yeni.varmi == null)
                yeni.varmi = true;

            if (yeni.bysMenuKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.BysMenus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                BysMenu? bulunan = await vari.BysMenus.FirstOrDefaultAsync(p => p.bysMenuKimlik == yeni.bysMenuKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }
        public static async Task silKos(BysMenu kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.BysMenus.FirstOrDefaultAsync(p => p.bysMenuKimlik == kimi.bysMenuKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }
        public static BysMenu? tekliCek(Int32 kimlik, Varlik kime)
        {
            BysMenu? kayit = kime.BysMenus.FirstOrDefault(p => p.bysMenuKimlik == kimlik);
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
        public static void kaydet(BysMenu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.bysMenuKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.BysMenus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.BysMenus.FirstOrDefault(p => p.bysMenuKimlik == yeni.bysMenuKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(BysMenu kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.BysMenus.FirstOrDefault(p => p.bysMenuKimlik == kimi.bysMenuKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
