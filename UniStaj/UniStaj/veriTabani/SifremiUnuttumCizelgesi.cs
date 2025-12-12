using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class SifremiUnuttumArama
    {
        public Int32? sifremiUnuttumKimlik { get; set; }
        public string? tcKimlikNo { get; set; }
        public bool? e_smsmi { get; set; }
        public string? ePosta { get; set; }
        public string? telefon { get; set; }
        public DateTime? tarih { get; set; }
        public bool? varmi { get; set; }
        public SifremiUnuttumArama()
        {
            this.varmi = true;
        }

        public async Task<List<SifremiUnuttum>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<SifremiUnuttum>(P => P.varmi == true);
            if (sifremiUnuttumKimlik != null)
                predicate = predicate.And(x => x.sifremiUnuttumKimlik == sifremiUnuttumKimlik);
            if (tcKimlikNo != null)
                predicate = predicate.And(x => x.tcKimlikNo.Contains(tcKimlikNo));
            if (e_smsmi != null)
                predicate = predicate.And(x => x.e_smsmi == e_smsmi);
            if (ePosta != null)
                predicate = predicate.And(x => x.ePosta.Contains(ePosta));
            if (telefon != null)
                predicate = predicate.And(x => x.telefon.Contains(telefon));
            if (tarih != null)
                predicate = predicate.And(x => x.tarih == tarih);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<SifremiUnuttum> sonuc = new List<SifremiUnuttum>();
            sonuc = await vari.SifremiUnuttums
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SifremiUnuttumCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<SifremiUnuttum> ara(params Predicate<SifremiUnuttum>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.SifremiUnuttums.ToList().FindAll(kosul).OrderByDescending(p => p.sifremiUnuttumKimlik).ToList();
            }
        }


        public static List<SifremiUnuttum> tamami(Varlik kime)
        {
            return kime.SifremiUnuttums.Where(p => p.varmi == true).OrderByDescending(p => p.sifremiUnuttumKimlik).ToList();
        }
        public static async Task<SifremiUnuttum?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            SifremiUnuttum? kayit = await kime.SifremiUnuttums.FirstOrDefaultAsync(p => p.sifremiUnuttumKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(SifremiUnuttum yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.sifremiUnuttumKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.SifremiUnuttums.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                SifremiUnuttum? bulunan = await vari.SifremiUnuttums.FirstOrDefaultAsync(p => p.sifremiUnuttumKimlik == yeni.sifremiUnuttumKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(SifremiUnuttum kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.SifremiUnuttums.FirstOrDefaultAsync(p => p.sifremiUnuttumKimlik == kimi.sifremiUnuttumKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static SifremiUnuttum tekliCek(Int32 kimlik, Varlik kime)
        {
            SifremiUnuttum kayit = kime.SifremiUnuttums.FirstOrDefault(p => p.sifremiUnuttumKimlik == kimlik);
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
        public static void kaydet(SifremiUnuttum yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sifremiUnuttumKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.SifremiUnuttums.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.SifremiUnuttums.FirstOrDefault(p => p.sifremiUnuttumKimlik == yeni.sifremiUnuttumKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(SifremiUnuttum kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.SifremiUnuttums.FirstOrDefault(p => p.sifremiUnuttumKimlik == kimi.sifremiUnuttumKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


