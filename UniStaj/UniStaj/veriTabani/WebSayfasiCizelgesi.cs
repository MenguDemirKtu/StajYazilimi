using LinqKit;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class WebSayfasiArama
    {
        public Int32? webSayfasiKimlik { get; set; }
        public string? hamAdresi { get; set; }
        public string? sayfaBasligi { get; set; }
        public Int32? i_modulKimlik { get; set; }
        public Int32? i_sayfaTuruKimlik { get; set; }
        public string? tanitim { get; set; }
        public string? aciklama { get; set; }
        public bool? varmi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public bool? e_izinSayfasindaGorunsunmu { get; set; }
        public bool? e_varsayilanEklemeAcikmi { get; set; }
        public bool? e_varsayilanGorunmeAcikmi { get; set; }
        public bool? e_varsayilanGuncellemeAcikmi { get; set; }
        public bool? e_varsayilanSilmeAcikmi { get; set; }
        public string? dokumAciklamasi { get; set; }
        public string? kartAciklamasi { get; set; }
        public WebSayfasiArama()
        {
            varmi = true;
        }

        public async Task<List<WebSayfasi>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<WebSayfasi>(P => P.varmi == true);
            if (webSayfasiKimlik != null)
                predicate = predicate.And(x => x.webSayfasiKimlik == webSayfasiKimlik);
            if (hamAdresi != null)
                predicate = predicate.And(x => x.hamAdresi.Contains(hamAdresi));
            if (sayfaBasligi != null)
                predicate = predicate.And(x => x.sayfaBasligi.Contains(sayfaBasligi));
            if (i_modulKimlik != null)
                predicate = predicate.And(x => x.i_modulKimlik == i_modulKimlik);
            if (i_sayfaTuruKimlik != null)
                predicate = predicate.And(x => x.i_sayfaTuruKimlik == i_sayfaTuruKimlik);
            if (tanitim != null)
                predicate = predicate.And(x => x.tanitim.Contains(tanitim));
            if (aciklama != null)
                predicate = predicate.And(x => x.aciklama.Contains(aciklama));
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (e_izinSayfasindaGorunsunmu != null)
                predicate = predicate.And(x => x.e_izinSayfasindaGorunsunmu == e_izinSayfasindaGorunsunmu);
            if (e_varsayilanEklemeAcikmi != null)
                predicate = predicate.And(x => x.e_varsayilanEklemeAcikmi == e_varsayilanEklemeAcikmi);
            if (e_varsayilanGorunmeAcikmi != null)
                predicate = predicate.And(x => x.e_varsayilanGorunmeAcikmi == e_varsayilanGorunmeAcikmi);
            if (e_varsayilanGuncellemeAcikmi != null)
                predicate = predicate.And(x => x.e_varsayilanGuncellemeAcikmi == e_varsayilanGuncellemeAcikmi);
            if (e_varsayilanSilmeAcikmi != null)
                predicate = predicate.And(x => x.e_varsayilanSilmeAcikmi == e_varsayilanSilmeAcikmi);
            if (dokumAciklamasi != null)
                predicate = predicate.And(x => x.dokumAciklamasi.Contains(dokumAciklamasi));
            if (kartAciklamasi != null)
                predicate = predicate.And(x => x.kartAciklamasi.Contains(kartAciklamasi));
            List<WebSayfasi> sonuc = new List<WebSayfasi>();
            sonuc = await vari.WebSayfasis
            .Where(predicate)
            .ToListAsync();
            return sonuc;
        }

    }


    public class WebSayfasiCizelgesi
    {




        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<WebSayfasi> ara(params Predicate<WebSayfasi>[] kosullar)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                var kosul = Vt.birlestir(kosullar);
                return vari.WebSayfasis.ToList().FindAll(kosul).OrderByDescending(p => p.webSayfasiKimlik).ToList();
            }
        }


        public static List<WebSayfasi> tamami(Varlik kime)
        {
            return kime.WebSayfasis.Where(p => p.varmi == true).OrderByDescending(p => p.webSayfasiKimlik).ToList();
        }
        public static async Task<WebSayfasi?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            WebSayfasi? kayit = await kime.WebSayfasis.FirstOrDefaultAsync(p => p.webSayfasiKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(WebSayfasi yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.webSayfasiKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.WebSayfasis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                WebSayfasi? bulunan = await vari.WebSayfasis.FirstOrDefaultAsync(p => p.webSayfasiKimlik == yeni.webSayfasiKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(yedekAlinsinmi);
            }
        }


        public static async Task silKos(WebSayfasi kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.WebSayfasis.FirstOrDefaultAsync(p => p.webSayfasiKimlik == kimi.webSayfasiKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(yedekAlinsinmi);
        }


        public static WebSayfasi tekliCek(Int32 kimlik, Varlik kime)
        {
            WebSayfasi kayit = kime.WebSayfasis.FirstOrDefault(p => p.webSayfasiKimlik == kimlik);
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
        public static void kaydet(WebSayfasi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.webSayfasiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.WebSayfasis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.WebSayfasis.FirstOrDefault(p => p.webSayfasiKimlik == yeni.webSayfasiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(WebSayfasi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.WebSayfasis.FirstOrDefault(p => p.webSayfasiKimlik == kimi.webSayfasiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}


