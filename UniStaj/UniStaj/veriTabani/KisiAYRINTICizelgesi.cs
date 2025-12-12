using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KisiAYRINTIArama
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
        public Int32? i_kisiTuruKimlik { get; set; }
        public string sehir { get; set; }
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
        public string isim { get; set; }
        public KisiAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<KisiAYRINTI> kosulu()
        {
            List<Predicate<KisiAYRINTI>> kosullar = new List<Predicate<KisiAYRINTI>>();
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
            if (i_sporcuKimlik != null)

                if (e_PostaDogrulandimi != null)
                    kosullar.Add(c => c.e_PostaDogrulandimi == e_PostaDogrulandimi);
            if (e_telefonDogrulandimi != null)
                kosullar.Add(c => c.e_telefonDogrulandimi == e_telefonDogrulandimi);
            if (kisiTanimi != null)
                kosullar.Add(c => c.kisiTanimi == kisiTanimi);
            if (i_cinsiyetKimlik != null)
                kosullar.Add(c => c.i_cinsiyetKimlik == i_cinsiyetKimlik);
            if (i_fotoKimlik != null)
                kosullar.Add(c => c.i_fotoKimlik == i_fotoKimlik);

            Predicate<KisiAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KisiAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KisiAYRINTI> ara(KisiAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KisiAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.kisiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KisiAYRINTI> ara(params Predicate<KisiAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KisiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.kisiKimlik).ToList(); }
        }
        public static List<KisiAYRINTI> tamami(Varlik kime)
        {
            return kime.KisiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.kisiKimlik).ToList();
        }
        public static List<KisiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KisiAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KisiAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            KisiAYRINTI kayit = kime.KisiAYRINTIs.FirstOrDefault(p => p.kisiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
