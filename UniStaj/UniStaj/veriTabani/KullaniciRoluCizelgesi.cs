using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class KullaniciRoluArama
    {
        public Int64? kullaniciRoluKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public Int32? i_rolKimlik { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public KullaniciRoluArama()
        {
            varmi = true;
        }

        public async Task<List<KullaniciRolu>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<KullaniciRolu>(P => P.varmi == true);
            if (kullaniciRoluKimlik != null)
                predicate = predicate.And(x => x.kullaniciRoluKimlik == kullaniciRoluKimlik);
            if (i_kullaniciKimlik != null)
                predicate = predicate.And(x => x.i_kullaniciKimlik == i_kullaniciKimlik);
            if (i_rolKimlik != null)
                predicate = predicate.And(x => x.i_rolKimlik == i_rolKimlik);
            if (e_gecerlimi != null)
                predicate = predicate.And(x => x.e_gecerlimi == e_gecerlimi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<KullaniciRolu> sonuc = new List<KullaniciRolu>();
            sonuc = await vari.KullaniciRolus
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class KullaniciRoluCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KullaniciRolu> ara(params Predicate<KullaniciRolu>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.KullaniciRolus.ToList().FindAll(kosul).OrderByDescending(p => p.kullaniciRoluKimlik).ToList();
            }
        }


        public static List<KullaniciRolu> tamami(Varlik kime)
        {
            return kime.KullaniciRolus.Where(p => p.varmi == true).OrderByDescending(p => p.kullaniciRoluKimlik).ToList();
        }
        public static async Task<KullaniciRolu?> tekliCekKos(Int64 kimlik, Varlik kime)
        {
            KullaniciRolu? kayit = await kime.KullaniciRolus.FirstOrDefaultAsync(p => p.kullaniciRoluKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(KullaniciRolu yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.kullaniciRoluKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.KullaniciRolus.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                KullaniciRolu? bulunan = await vari.KullaniciRolus.FirstOrDefaultAsync(p => p.kullaniciRoluKimlik == yeni.kullaniciRoluKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(KullaniciRolu kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.KullaniciRolus.FirstOrDefaultAsync(p => p.kullaniciRoluKimlik == kimi.kullaniciRoluKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static KullaniciRolu tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciRolu kayit = kime.KullaniciRolus.FirstOrDefault(p => p.kullaniciRoluKimlik == kimlik);
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
        public static void kaydet(KullaniciRolu yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.kullaniciRoluKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.KullaniciRolus.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.KullaniciRolus.FirstOrDefault(p => p.kullaniciRoluKimlik == yeni.kullaniciRoluKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(KullaniciRolu kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.KullaniciRolus.FirstOrDefault(p => p.kullaniciRoluKimlik == kimi.kullaniciRoluKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


