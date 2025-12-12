using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class YuklenenDosyalarAYRINTIArama
    {
        public Int64? yuklenenDosyalarKimlik { get; set; }
        public string dosyaKonumu { get; set; }
        public string ilgiliCizelgeAdi { get; set; }
        public Int64? ilgiliCizelgeKimlik { get; set; }
        public DateTime? yuklemeTarihi { get; set; }
        public bool? varmi { get; set; }
        public string dosyaUzantisi { get; set; }
        public Int32? i_dosyaTuruKimlik { get; set; }
        public string kodu { get; set; }
        public YuklenenDosyalarAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<YuklenenDosyalarAYRINTI> kosulu()
        {
            List<Predicate<YuklenenDosyalarAYRINTI>> kosullar = new List<Predicate<YuklenenDosyalarAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (yuklenenDosyalarKimlik != null)
                kosullar.Add(c => c.yuklenenDosyalarKimlik == yuklenenDosyalarKimlik);
            if (dosyaKonumu != null)
                kosullar.Add(c => c.dosyaKonumu == dosyaKonumu);
            if (ilgiliCizelgeAdi != null)
                kosullar.Add(c => c.ilgiliCizelgeAdi == ilgiliCizelgeAdi);
            if (ilgiliCizelgeKimlik != null)
                kosullar.Add(c => c.ilgiliCizelgeKimlik == ilgiliCizelgeKimlik);
            if (yuklemeTarihi != null)
                kosullar.Add(c => c.yuklemeTarihi == yuklemeTarihi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (dosyaUzantisi != null)
                kosullar.Add(c => c.dosyaUzantisi == dosyaUzantisi);
            if (i_dosyaTuruKimlik != null)
                kosullar.Add(c => c.i_dosyaTuruKimlik == i_dosyaTuruKimlik);
            if (kodu != null)
                kosullar.Add(c => c.kodu == kodu);
            Predicate<YuklenenDosyalarAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class YuklenenDosyalarAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<YuklenenDosyalarAYRINTI> ara(YuklenenDosyalarAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.YuklenenDosyalarAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.yuklenenDosyalarKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<YuklenenDosyalarAYRINTI> ara(params Predicate<YuklenenDosyalarAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.YuklenenDosyalarAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.yuklenenDosyalarKimlik).ToList(); }
        }
        public static List<YuklenenDosyalarAYRINTI> tamami(Varlik kime)
        {
            return kime.YuklenenDosyalarAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.yuklenenDosyalarKimlik).ToList();
        }
        public static List<YuklenenDosyalarAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<YuklenenDosyalarAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static YuklenenDosyalarAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            YuklenenDosyalarAYRINTI kayit = kime.YuklenenDosyalarAYRINTIs.FirstOrDefault(p => p.yuklenenDosyalarKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
