using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class RolWebSayfasiIzniAYRINTIArama
    {
        public Int64? rolWebSayfasiIzniKimlik { get; set; }
        public Int32? i_rolKimlik { get; set; }
        public string rolAdi { get; set; }
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
        public RolWebSayfasiIzniAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<RolWebSayfasiIzniAYRINTI> kosulu()
        {
            List<Predicate<RolWebSayfasiIzniAYRINTI>> kosullar = new List<Predicate<RolWebSayfasiIzniAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (rolWebSayfasiIzniKimlik != null)
                kosullar.Add(c => c.rolWebSayfasiIzniKimlik == rolWebSayfasiIzniKimlik);
            if (i_rolKimlik != null)
                kosullar.Add(c => c.i_rolKimlik == i_rolKimlik);
            if (rolAdi != null)
                kosullar.Add(c => c.rolAdi == rolAdi);
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
            Predicate<RolWebSayfasiIzniAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class RolWebSayfasiIzniAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<RolWebSayfasiIzniAYRINTI> ara(RolWebSayfasiIzniAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new Varlik())
            {
                return vari.RolWebSayfasiIzniAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList();
            }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<RolWebSayfasiIzniAYRINTI> ara(params Predicate<RolWebSayfasiIzniAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            {
                using (veri.Varlik vari = new Varlik())
                {
                    return vari.RolWebSayfasiIzniAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList();
                }
            }
        }
        public static List<RolWebSayfasiIzniAYRINTI> tamami(Varlik kime)
        {
            return kime.RolWebSayfasiIzniAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.rolWebSayfasiIzniKimlik).ToList();
        }
        public static List<RolWebSayfasiIzniAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<RolWebSayfasiIzniAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik())
            {
                return tamami(vari);
            }
        }
        public static RolWebSayfasiIzniAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            RolWebSayfasiIzniAYRINTI kayit = kime.RolWebSayfasiIzniAYRINTIs.FirstOrDefault(p => p.rolWebSayfasiIzniKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
