using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;


namespace UniStaj.veriTabani
{
    public class SozlesmeArama
    {
        public Int32? sozlesmekimlik { get; set; }
        public string? baslik { get; set; }
        public Int32? i_sozlesmeTuruKimlik { get; set; }
        public bool? e_gecerliMi { get; set; }
        public string? metin { get; set; }
        public bool? varmi { get; set; }
        public SozlesmeArama()
        {
            varmi = true;
        }

        public async Task<List<Sozlesme>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<Sozlesme>(P => P.varmi == true);
            if (sozlesmekimlik != null)
                predicate = predicate.And(x => x.sozlesmekimlik == sozlesmekimlik);
            if (baslik != null)
                predicate = predicate.And(x => x.baslik.Contains(baslik));
            if (i_sozlesmeTuruKimlik != null)
                predicate = predicate.And(x => x.i_sozlesmeTuruKimlik == i_sozlesmeTuruKimlik);
            if (e_gecerliMi != null)
                predicate = predicate.And(x => x.e_gecerliMi == e_gecerliMi);
            if (metin != null)
                predicate = predicate.And(x => x.metin.Contains(metin));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            List<Sozlesme> sonuc = new List<Sozlesme>();
            sonuc = await vari.Sozlesmes
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class SozlesmeCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Sozlesme> ara(params Predicate<Sozlesme>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.Sozlesmes.ToList().FindAll(kosul).OrderByDescending(p => p.sozlesmekimlik).ToList();
            }
        }


        public static List<Sozlesme> tamami(Varlik kime)
        {
            return kime.Sozlesmes.Where(p => p.varmi == true).OrderByDescending(p => p.sozlesmekimlik).ToList();
        }
        public static async Task<Sozlesme?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            Sozlesme? kayit = await kime.Sozlesmes.FirstOrDefaultAsync(p => p.sozlesmekimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(Sozlesme yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.sozlesmekimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.Sozlesmes.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                Sozlesme? bulunan = await vari.Sozlesmes.FirstOrDefaultAsync(p => p.sozlesmekimlik == yeni.sozlesmekimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(Sozlesme kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.Sozlesmes.FirstOrDefaultAsync(p => p.sozlesmekimlik == kimi.sozlesmekimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static Sozlesme tekliCek(Int32 kimlik, Varlik kime)
        {
            Sozlesme kayit = kime.Sozlesmes.FirstOrDefault(p => p.sozlesmekimlik == kimlik);
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
        public static void kaydet(Sozlesme yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.sozlesmekimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Sozlesmes.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.Sozlesmes.FirstOrDefault(p => p.sozlesmekimlik == yeni.sozlesmekimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(Sozlesme kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Sozlesmes.FirstOrDefault(p => p.sozlesmekimlik == kimi.sozlesmekimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


