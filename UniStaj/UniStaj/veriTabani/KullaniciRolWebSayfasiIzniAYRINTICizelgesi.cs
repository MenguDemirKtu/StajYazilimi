using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KullaniciRolWebSayfasiIzniAYRINTIArama
    {
        public Int64? rolWebSayfasiIzniKimlik { get; set; }
        public Int32? i_rolKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public Int32? i_webSayfasiKimlik { get; set; }
        public string hamAdresi { get; set; }
        public string sayfaBasligi { get; set; }
        public bool? e_gormeIzniVarmi { get; set; }
        public bool? e_eklemeIzniVarmi { get; set; }
        public bool? e_silmeIzniVarmi { get; set; }
        public bool? e_guncellemeIzniVarmi { get; set; }
        public bool? varmi { get; set; }
        public KullaniciRolWebSayfasiIzniAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<KullaniciRolWebSayfasiIzniAYRINTI> kosulu()
        {
            List<Predicate<KullaniciRolWebSayfasiIzniAYRINTI>> kosullar = new List<Predicate<KullaniciRolWebSayfasiIzniAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (rolWebSayfasiIzniKimlik != null)
                kosullar.Add(c => c.rolWebSayfasiIzniKimlik == rolWebSayfasiIzniKimlik);
            if (i_rolKimlik != null)
                kosullar.Add(c => c.i_rolKimlik == i_rolKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (i_webSayfasiKimlik != null)
                kosullar.Add(c => c.i_webSayfasiKimlik == i_webSayfasiKimlik);
            if (hamAdresi != null)
                kosullar.Add(c => c.hamAdresi == hamAdresi);
            if (sayfaBasligi != null)
                kosullar.Add(c => c.sayfaBasligi == sayfaBasligi);
            if (e_gormeIzniVarmi != null)
                kosullar.Add(c => c.e_gormeIzniVarmi == e_gormeIzniVarmi);
            if (e_eklemeIzniVarmi != null)
                kosullar.Add(c => c.e_eklemeIzniVarmi == e_eklemeIzniVarmi);
            if (e_silmeIzniVarmi != null)
                kosullar.Add(c => c.e_silmeIzniVarmi == e_silmeIzniVarmi);
            if (e_guncellemeIzniVarmi != null)
                kosullar.Add(c => c.e_guncellemeIzniVarmi == e_guncellemeIzniVarmi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<KullaniciRolWebSayfasiIzniAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KullaniciRolWebSayfasiIzniAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KullaniciRolWebSayfasiIzniAYRINTI> ara(KullaniciRolWebSayfasiIzniAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciRolWebSayfasiIzniAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KullaniciRolWebSayfasiIzniAYRINTI> ara(params Predicate<KullaniciRolWebSayfasiIzniAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciRolWebSayfasiIzniAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList(); }
        }
        public static List<KullaniciRolWebSayfasiIzniAYRINTI> tamami(Varlik kime)
        {
            return kime.KullaniciRolWebSayfasiIzniAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList();
        }
        public static List<KullaniciRolWebSayfasiIzniAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KullaniciRolWebSayfasiIzniAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KullaniciRolWebSayfasiIzniAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciRolWebSayfasiIzniAYRINTI kayit = kime.KullaniciRolWebSayfasiIzniAYRINTIs.FirstOrDefault(p => p.rolWebSayfasiIzniKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
