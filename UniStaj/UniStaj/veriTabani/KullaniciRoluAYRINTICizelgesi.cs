using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KullaniciRoluAYRINTIArama
    {
        public Int64? kullaniciRoluKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public Int32? i_rolKimlik { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public string rolAdi { get; set; }
        public string kullaniciAdi { get; set; }
        public KullaniciRoluAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<KullaniciRoluAYRINTI> kosulu()
        {
            List<Predicate<KullaniciRoluAYRINTI>> kosullar = new List<Predicate<KullaniciRoluAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (kullaniciRoluKimlik != null)
                kosullar.Add(c => c.kullaniciRoluKimlik == kullaniciRoluKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (i_rolKimlik != null)
                kosullar.Add(c => c.i_rolKimlik == i_rolKimlik);
            if (e_gecerlimi != null)
                kosullar.Add(c => c.e_gecerlimi == e_gecerlimi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            if (rolAdi != null)
                kosullar.Add(c => c.rolAdi == rolAdi);
            if (kullaniciAdi != null)
                kosullar.Add(c => c.kullaniciAdi == kullaniciAdi);
            Predicate<KullaniciRoluAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KullaniciRoluAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KullaniciRoluAYRINTI> ara(KullaniciRoluAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciRoluAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.kullaniciRoluKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KullaniciRoluAYRINTI> ara(params Predicate<KullaniciRoluAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KullaniciRoluAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.kullaniciRoluKimlik).ToList(); }
        }
        public static List<KullaniciRoluAYRINTI> tamami(Varlik kime)
        {
            return kime.KullaniciRoluAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.kullaniciRoluKimlik).ToList();
        }
        public static List<KullaniciRoluAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KullaniciRoluAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KullaniciRoluAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            KullaniciRoluAYRINTI kayit = kime.KullaniciRoluAYRINTIs.FirstOrDefault(p => p.kullaniciRoluKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
