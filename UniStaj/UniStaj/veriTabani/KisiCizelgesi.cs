using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KisiArama
    {
        public Int64? kisiKimlik { get; set; }
        public string tcKimlikNo { get; set; }
        public string adi { get; set; }
        public string soyAdi { get; set; }
        public string telefon { get; set; }
        public string ePosta { get; set; }
        public string adres { get; set; }
        public Int32? i_sehirKimlik { get; set; }
        public Int64? i_sporcuKimlik { get; set; }
        public Int64? i_hakemKimlik { get; set; }
        public Int64? i_antrenorKimlik { get; set; }
        public bool? varmi { get; set; }
        public Int64? i_ilTemsilcisiKimlik { get; set; }
        public DateTime? dogumTarihi { get; set; }
        public Int64? i_kulupUyesiKimlik { get; set; }
        public bool? e_tcDogrulandimi { get; set; }
        public bool? e_PostaDogrulandimi { get; set; }
        public bool? e_telefonDogrulandimi { get; set; }
        public string kisiTanimi { get; set; }
        public Int32? i_cinsiyetKimlik { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public Int64? i_diyetisyenKimlik { get; set; }
        public Int64? i_federasyonGonullusuKimlik { get; set; }
        public Int32? i_kisiTuruKimlik { get; set; }
        public string telefonOnayKodu { get; set; }
        public string ePostaOnayKodu { get; set; }
        public KisiArama()
        {
            varmi = true;
        }
        public Predicate<Kisi> kosulu()
        {
            List<Predicate<Kisi>> kosullar = new List<Predicate<Kisi>>();
            kosullar.Add(c => c.varmi == true);
            if (kisiKimlik != null)
                kosullar.Add(c => c.kisiKimlik == kisiKimlik);
            if (tcKimlikNo != null)
                kosullar.Add(c => c.tcKimlikNo == tcKimlikNo);
            if (adi != null)
                kosullar.Add(c => c.adi == adi);
            if (soyAdi != null)
                kosullar.Add(c => c.soyAdi == soyAdi);
            if (telefon != null)
                kosullar.Add(c => c.telefon == telefon);
            if (ePosta != null)
                kosullar.Add(c => c.ePosta == ePosta);
            if (adres != null)
                kosullar.Add(c => c.adres == adres);
            if (i_sehirKimlik != null)
                kosullar.Add(c => c.i_sehirKimlik == i_sehirKimlik);

            kosullar.Add(c => c.e_PostaDogrulandimi == e_PostaDogrulandimi);
            if (e_telefonDogrulandimi != null)
                kosullar.Add(c => c.e_telefonDogrulandimi == e_telefonDogrulandimi);
            if (kisiTanimi != null)
                kosullar.Add(c => c.kisiTanimi == kisiTanimi);
            if (i_cinsiyetKimlik != null)
                kosullar.Add(c => c.i_cinsiyetKimlik == i_cinsiyetKimlik);
            if (i_fotoKimlik != null)
                kosullar.Add(c => c.i_fotoKimlik == i_fotoKimlik);
            Predicate<Kisi> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KisiCizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<Kisi> ara(KisiArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.Kisis.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.kisiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<Kisi> ara(params Predicate<Kisi>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.Kisis.ToList().FindAll(kosul).OrderByDescending(p => p.kisiKimlik).ToList(); }
        }
        public static List<Kisi> tamami(Varlik kime)
        {
            return kime.Kisis.Where(p => p.varmi == true).OrderByDescending(p => p.kisiKimlik).ToList();
        }
        public static List<Kisi> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<Kisi> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static Kisi tekliCek(Int64 kimlik, Varlik kime)
        {
            Kisi kayit = kime.Kisis.FirstOrDefault(p => p.kisiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
        /// <summary>
        /// Varlık nesnesine ekler ya da nesnede  günceller ancak veri tabanına kaydetmez. Seri ekleme için kullanılmalıdır.
        /// </summary>
        public static void ula(Kisi yeni, ref Varlik kime)
        {
            if (yeni.kisiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Kisis.Add(yeni);
            }
            else
            {
                var bulunan = kime.Kisis.FirstOrDefault(p => p.kisiKimlik == yeni.kisiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            }
        }
        /// <summary>
        /// Hızlı bir şekilde veri tabanına ekler. Yedekleme yoktur
        /// </summary>
        private static void ekle(Kisi yeni, Varlik kime)
        {
            if (yeni.varmi == null)
                yeni.varmi = true;
            kime.Kisis.Add(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Hızlı bir şekilde günceller. Yedekleme yoktur
        /// </summary>
        private static void guncelle(Kisi yeni, Varlik kime)
        {
            var bulunan = kime.Kisis.FirstOrDefault(p => p.kisiKimlik == yeni.kisiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(yeni);
            kime.SaveChanges();
        }
        /// <summary>
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır.
        /// </summary>
        /// <param name="yeni"></param>
        /// <param name="kime"></param>
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param>
        public static void kaydet(Kisi yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.kisiKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.Kisis.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {
                var bulunan = kime.Kisis.FirstOrDefault(p => p.kisiKimlik == yeni.kisiKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }
        public static void sil(Kisi kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.Kisis.FirstOrDefault(p => p.kisiKimlik == kimi.kisiKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();
        }
    }
}
