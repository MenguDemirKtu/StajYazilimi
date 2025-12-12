using LinqKit;
using Microsoft.EntityFrameworkCore; //;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class ResimAyariArama
    {
        public Int32? resimAyariKimlik { get; set; }
        public string? ilgiliCizelge { get; set; }
        public Int32? genislik { get; set; }
        public Int32? yukseklik { get; set; }
        public bool? e_genislik2Varmi { get; set; }
        public bool? e_genislik3Varmi { get; set; }
        public bool? e_genislik4Varmi { get; set; }
        public Int32? genislik2 { get; set; }
        public Int32? yukseklik2 { get; set; }
        public Int32? genislik3 { get; set; }
        public Int32? yukseklik3 { get; set; }
        public Int32? genislik4 { get; set; }
        public Int32? yukseklik4 { get; set; }
        public string? genislikSutunAdi2 { get; set; }
        public string? genislikSutunAdi3 { get; set; }
        public string? genislikSutunAdi4 { get; set; }
        public bool? varmi { get; set; }
        public string? dizinAdi { get; set; }
        public Int32? kalite { get; set; }
        public ResimAyariArama()
        {
            this.varmi = true;
        }

        public async Task<List<ResimAyari>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<ResimAyari>(P => P.varmi == true);
            if (resimAyariKimlik != null)
                predicate = predicate.And(x => x.resimAyariKimlik == resimAyariKimlik);
            if (ilgiliCizelge != null)
                predicate = predicate.And(x => x.ilgiliCizelge != null && x.ilgiliCizelge.Contains(ilgiliCizelge));
            if (genislik != null)
                predicate = predicate.And(x => x.genislik == genislik);
            if (yukseklik != null)
                predicate = predicate.And(x => x.yukseklik == yukseklik);
            if (e_genislik2Varmi != null)
                predicate = predicate.And(x => x.e_genislik2Varmi == e_genislik2Varmi);
            if (e_genislik3Varmi != null)
                predicate = predicate.And(x => x.e_genislik3Varmi == e_genislik3Varmi);
            if (e_genislik4Varmi != null)
                predicate = predicate.And(x => x.e_genislik4Varmi == e_genislik4Varmi);
            if (genislik2 != null)
                predicate = predicate.And(x => x.genislik2 == genislik2);
            if (yukseklik2 != null)
                predicate = predicate.And(x => x.yukseklik2 == yukseklik2);
            if (genislik3 != null)
                predicate = predicate.And(x => x.genislik3 == genislik3);
            if (yukseklik3 != null)
                predicate = predicate.And(x => x.yukseklik3 == yukseklik3);
            if (genislik4 != null)
                predicate = predicate.And(x => x.genislik4 == genislik4);
            if (yukseklik4 != null)
                predicate = predicate.And(x => x.yukseklik4 == yukseklik4);
            if (genislikSutunAdi2 != null)
                predicate = predicate.And(x => x.genislikSutunAdi2 != null && x.genislikSutunAdi2.Contains(genislikSutunAdi2));
            if (genislikSutunAdi3 != null)
                predicate = predicate.And(x => x.genislikSutunAdi3 != null && x.genislikSutunAdi3.Contains(genislikSutunAdi3));
            if (genislikSutunAdi4 != null)
                predicate = predicate.And(x => x.genislikSutunAdi4 != null && x.genislikSutunAdi4.Contains(genislikSutunAdi4));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (dizinAdi != null)
                predicate = predicate.And(x => x.dizinAdi != null && x.dizinAdi.Contains(dizinAdi));
            if (kalite != null)
                predicate = predicate.And(x => x.kalite == kalite);
            List<ResimAyari> sonuc = new List<ResimAyari>();
            sonuc = await vari.ResimAyaris
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class ResimAyariCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<ResimAyari> ara(params Predicate<ResimAyari>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.ResimAyaris.ToList().FindAll(kosul).OrderByDescending(p => p.resimAyariKimlik).ToList();
            }
        }


        public static List<ResimAyari> tamami(Varlik kime)
        {
            return kime.ResimAyaris.Where(p => p.varmi == true).OrderByDescending(p => p.resimAyariKimlik).ToList();
        }
        public static async Task<ResimAyari?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            ResimAyari? kayit = await kime.ResimAyaris.FirstOrDefaultAsync(p => p.resimAyariKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(ResimAyari yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.resimAyariKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.ResimAyaris.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                ResimAyari? bulunan = await vari.ResimAyaris.FirstOrDefaultAsync(p => p.resimAyariKimlik == yeni.resimAyariKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }


        public static async Task silKos(ResimAyari kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.ResimAyaris.FirstOrDefaultAsync(p => p.resimAyariKimlik == kimi.resimAyariKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static ResimAyari? tekliCek(Int32 kimlik, Varlik kime)
        {
            ResimAyari? kayit = kime.ResimAyaris.FirstOrDefault(p => p.resimAyariKimlik == kimlik);
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
        public static void kaydet(ResimAyari yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.resimAyariKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.ResimAyaris.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.ResimAyaris.FirstOrDefault(p => p.resimAyariKimlik == yeni.resimAyariKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(ResimAyari kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.ResimAyaris.FirstOrDefault(p => p.resimAyariKimlik == kimi.resimAyariKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


