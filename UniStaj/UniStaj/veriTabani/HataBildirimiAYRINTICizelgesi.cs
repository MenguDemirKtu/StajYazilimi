using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class HataBildirimiAYRINTIArama
    {
        public Int32? hataBildirimiKimlik { get; set; }
        public Int32? i_yoneticiKimlik { get; set; }
        public string baslik { get; set; }
        public DateTime? tarih { get; set; }
        public bool? varmi { get; set; }
        public Byte? e_goruldumu { get; set; }
        public string goruldumu { get; set; }
        public string hataAlinanSayfa { get; set; }
        public string hataAciklamasi { get; set; }
        public HataBildirimiAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<HataBildirimiAYRINTI> kosulu()
        {
            List<Predicate<HataBildirimiAYRINTI>> kosullar = new List<Predicate<HataBildirimiAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (hataBildirimiKimlik != null)
                kosullar.Add(c => c.hataBildirimiKimlik == hataBildirimiKimlik);
            if (i_yoneticiKimlik != null)
                kosullar.Add(c => c.i_yoneticiKimlik == i_yoneticiKimlik);
            if (baslik != null)
                kosullar.Add(c => c.baslik == baslik);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (e_goruldumu != null)
                kosullar.Add(c => c.e_goruldumu == e_goruldumu);
            if (goruldumu != null)
                kosullar.Add(c => c.goruldumu == goruldumu);
            if (hataAlinanSayfa != null)
                kosullar.Add(c => c.hataAlinanSayfa == hataAlinanSayfa);
            if (hataAciklamasi != null)
                kosullar.Add(c => c.hataAciklamasi == hataAciklamasi);
            Predicate<HataBildirimiAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class HataBildirimiAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<HataBildirimiAYRINTI> ara(HataBildirimiAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.HataBildirimiAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.hataBildirimiKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<HataBildirimiAYRINTI> ara(params Predicate<HataBildirimiAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.HataBildirimiAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.hataBildirimiKimlik).ToList(); }
        }
        public static List<HataBildirimiAYRINTI> tamami(Varlik kime)
        {
            return kime.HataBildirimiAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.hataBildirimiKimlik).ToList();
        }
        public static List<HataBildirimiAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<HataBildirimiAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static HataBildirimiAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            HataBildirimiAYRINTI kayit = kime.HataBildirimiAYRINTIs.FirstOrDefault(p => p.hataBildirimiKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
