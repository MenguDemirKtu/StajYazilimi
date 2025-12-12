using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KullaniciRoluIzinAYRINTIArama
    {
        public Int64? rolWebSayfasiIzniKimlik { get; set; }
        public Int32? i_webSayfasiKimlik { get; set; }
        public bool? e_gormeIzniVarmi { get; set; }
        public bool? e_eklemeIzniVarmi { get; set; }
        public bool? e_silmeIzniVarmi { get; set; }
        public bool? e_guncellemeIzniVarmi { get; set; }
        public bool? varmi { get; set; }
        public Int32? i_rolKimlik { get; set; }
        public KullaniciRoluIzinAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<KullaniciRoluIzinAYRINTI> kosulu()
        {
            List<Predicate<KullaniciRoluIzinAYRINTI>> kosullar = new List<Predicate<KullaniciRoluIzinAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (rolWebSayfasiIzniKimlik != null)
                kosullar.Add(c => c.rolWebSayfasiIzniKimlik == rolWebSayfasiIzniKimlik);
            if (i_webSayfasiKimlik != null)
                kosullar.Add(c => c.i_webSayfasiKimlik == i_webSayfasiKimlik);
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
            if (i_rolKimlik != null)
                kosullar.Add(c => c.i_rolKimlik == i_rolKimlik);
            Predicate<KullaniciRoluIzinAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KullaniciRoluIzinAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KullaniciRoluIzinAYRINTI> ara(KullaniciRoluIzinAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciRoluIzinAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KullaniciRoluIzinAYRINTI> ara(params Predicate<KullaniciRoluIzinAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciRoluIzinAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList(); }
        }
        public static List<KullaniciRoluIzinAYRINTI> tamami(Varlik kime)
        {
            return kime.KullaniciRoluIzinAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList();
        }
        public static List<KullaniciRoluIzinAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KullaniciRoluIzinAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KullaniciRoluIzinAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciRoluIzinAYRINTI kayit = kime.KullaniciRoluIzinAYRINTIs.FirstOrDefault(p => p.rolWebSayfasiIzniKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
