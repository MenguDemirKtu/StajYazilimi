using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KullaniciWebSayfasiIzniAYRINTIArama
    {
        public Int64? kullaniciWebSayfasiIzniKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public string kullaniciAdi { get; set; }
        public Int32? i_webSayfasiKimlik { get; set; }
        public string hamAdresi { get; set; }
        public bool? e_gormeIzniVarmi { get; set; }
        public string gormeIzniVarmi { get; set; }
        public bool? e_eklemeIzniVarmi { get; set; }
        public string eklemeIzniVarmi { get; set; }
        public bool? e_silmeIzniVarmi { get; set; }
        public string silmeIzniVarmi { get; set; }
        public bool? e_guncellemeIzniVarmi { get; set; }
        public string guncellemeIzniVarmi { get; set; }
        public bool? varmi { get; set; }
        public string sayfaBasligi { get; set; }
        public Int32? i_modulKimlik { get; set; }
        public string tanitim { get; set; }
        public bool? e_izinSayfasindaGorunsunmu { get; set; }
        public string modulAdi { get; set; }
        public KullaniciWebSayfasiIzniAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<KullaniciWebSayfasiIzniAYRINTI> kosulu()
        {
            List<Predicate<KullaniciWebSayfasiIzniAYRINTI>> kosullar = new List<Predicate<KullaniciWebSayfasiIzniAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (kullaniciWebSayfasiIzniKimlik != null)
                kosullar.Add(c => c.kullaniciWebSayfasiIzniKimlik == kullaniciWebSayfasiIzniKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (kullaniciAdi != null)
                kosullar.Add(c => c.kullaniciAdi == kullaniciAdi);
            if (i_webSayfasiKimlik != null)
                kosullar.Add(c => c.i_webSayfasiKimlik == i_webSayfasiKimlik);
            if (hamAdresi != null)
                kosullar.Add(c => c.hamAdresi == hamAdresi);
            if (e_gormeIzniVarmi != null)
                kosullar.Add(c => c.e_gormeIzniVarmi == e_gormeIzniVarmi);
            if (gormeIzniVarmi != null)
                kosullar.Add(c => c.gormeIzniVarmi == gormeIzniVarmi);
            if (e_eklemeIzniVarmi != null)
                kosullar.Add(c => c.e_eklemeIzniVarmi == e_eklemeIzniVarmi);
            if (eklemeIzniVarmi != null)
                kosullar.Add(c => c.eklemeIzniVarmi == eklemeIzniVarmi);
            if (e_silmeIzniVarmi != null)
                kosullar.Add(c => c.e_silmeIzniVarmi == e_silmeIzniVarmi);
            if (silmeIzniVarmi != null)
                kosullar.Add(c => c.silmeIzniVarmi == silmeIzniVarmi);
            if (e_guncellemeIzniVarmi != null)
                kosullar.Add(c => c.e_guncellemeIzniVarmi == e_guncellemeIzniVarmi);
            if (guncellemeIzniVarmi != null)
                kosullar.Add(c => c.guncellemeIzniVarmi == guncellemeIzniVarmi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (sayfaBasligi != null)
                kosullar.Add(c => c.sayfaBasligi == sayfaBasligi);
            if (i_modulKimlik != null)
                kosullar.Add(c => c.i_modulKimlik == i_modulKimlik);
            if (tanitim != null)
                kosullar.Add(c => c.tanitim == tanitim);
            if (e_izinSayfasindaGorunsunmu != null)
                kosullar.Add(c => c.e_izinSayfasindaGorunsunmu == e_izinSayfasindaGorunsunmu);
            if (modulAdi != null)
                kosullar.Add(c => c.modulAdi == modulAdi);
            Predicate<KullaniciWebSayfasiIzniAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KullaniciWebSayfasiIzniAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KullaniciWebSayfasiIzniAYRINTI> ara(KullaniciWebSayfasiIzniAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciWebSayfasiIzniAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.kullaniciWebSayfasiIzniKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KullaniciWebSayfasiIzniAYRINTI> ara(params Predicate<KullaniciWebSayfasiIzniAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciWebSayfasiIzniAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.kullaniciWebSayfasiIzniKimlik).ToList(); }
        }
        public static List<KullaniciWebSayfasiIzniAYRINTI> tamami(Varlik kime)
        {
            return kime.KullaniciWebSayfasiIzniAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.kullaniciWebSayfasiIzniKimlik).ToList();
        }
        public static List<KullaniciWebSayfasiIzniAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KullaniciWebSayfasiIzniAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KullaniciWebSayfasiIzniAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciWebSayfasiIzniAYRINTI kayit = kime.KullaniciWebSayfasiIzniAYRINTIs.FirstOrDefault(p => p.kullaniciWebSayfasiIzniKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
