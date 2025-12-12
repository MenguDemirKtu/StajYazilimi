using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class KayitAYRINTIArama
    {
        public Int64? kayitKimlik { get; set; }
        public string islemTuru { get; set; }
        public string cizelgeAdi { get; set; }
        public Int64? cizelgeKimlik { get; set; }
        public Int32? i_kullaniciKimlik { get; set; }
        public DateTime? tarih { get; set; }
        public string ipAdresi { get; set; }
        public string ekBilgi { get; set; }
        public Byte? kullaniciTuru { get; set; }
        public KayitAYRINTIArama()
        {
        }
        public Predicate<KayitAYRINTI> kosulu()
        {
            List<Predicate<KayitAYRINTI>> kosullar = new List<Predicate<KayitAYRINTI>>();
            if (kayitKimlik != null)
                kosullar.Add(c => c.kayitKimlik == kayitKimlik);
            if (islemTuru != null)
                kosullar.Add(c => c.islemTuru == islemTuru);
            if (cizelgeAdi != null)
                kosullar.Add(c => c.cizelgeAdi == cizelgeAdi);
            if (cizelgeKimlik != null)
                kosullar.Add(c => c.cizelgeKimlik == cizelgeKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (tarih != null)
                kosullar.Add(c => c.tarih == tarih);
            if (ipAdresi != null)
                kosullar.Add(c => c.ipAdresi == ipAdresi);
            if (ekBilgi != null)
                kosullar.Add(c => c.ekBilgi == ekBilgi);
            if (kullaniciTuru != null)
                kosullar.Add(c => c.kullaniciTuru == kullaniciTuru);
            Predicate<KayitAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class KayitAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<KayitAYRINTI> ara(KayitAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KayitAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.kayitKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<KayitAYRINTI> ara(params Predicate<KayitAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.KayitAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.kayitKimlik).ToList(); }
        }
        public static List<KayitAYRINTI> tamami(Varlik kime)
        {
            return kime.KayitAYRINTIs.ToList();
        }
        public static List<KayitAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<KayitAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static KayitAYRINTI tekliCek(Int64 kimlik, Varlik kime)
        {
            KayitAYRINTI kayit = kime.KayitAYRINTIs.FirstOrDefault(p => p.kayitKimlik == kimlik);
            return kayit;
        }
    }
}
