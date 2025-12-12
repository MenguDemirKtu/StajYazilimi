using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class FederasyonBildirimGondermeAyariAYRINTIArama
    {
        public Int32? federasyonBildirimGondermeAyariKimlik { get; set; }
        public Int64? i_kullaniciKimlik { get; set; }
        public string kullaniciAdi { get; set; }
        public string gercekAdi { get; set; }
        public string telefon { get; set; }
        public string ePostaAdresi { get; set; }
        public Int32? i_federasyonBildirimTuruKimlik { get; set; }
        public string federasyonBildirimTuruAdi { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public FederasyonBildirimGondermeAyariAYRINTIArama()
        {
            varmi = true;
        }
        public Predicate<FederasyonBildirimGondermeAyariAYRINTI> kosulu()
        {
            List<Predicate<FederasyonBildirimGondermeAyariAYRINTI>> kosullar = new List<Predicate<FederasyonBildirimGondermeAyariAYRINTI>>();
            kosullar.Add(c => c.varmi == true);
            if (federasyonBildirimGondermeAyariKimlik != null)
                kosullar.Add(c => c.federasyonBildirimGondermeAyariKimlik == federasyonBildirimGondermeAyariKimlik);
            if (i_kullaniciKimlik != null)
                kosullar.Add(c => c.i_kullaniciKimlik == i_kullaniciKimlik);
            if (kullaniciAdi != null)
                kosullar.Add(c => c.kullaniciAdi == kullaniciAdi);
            if (gercekAdi != null)
                kosullar.Add(c => c.gercekAdi == gercekAdi);
            if (telefon != null)
                kosullar.Add(c => c.telefon == telefon);
            if (ePostaAdresi != null)
                kosullar.Add(c => c.ePostaAdresi == ePostaAdresi);
            if (i_federasyonBildirimTuruKimlik != null)
                kosullar.Add(c => c.i_federasyonBildirimTuruKimlik == i_federasyonBildirimTuruKimlik);
            if (federasyonBildirimTuruAdi != null)
                kosullar.Add(c => c.federasyonBildirimTuruAdi == federasyonBildirimTuruAdi);
            if (e_gecerlimi != null)
                kosullar.Add(c => c.e_gecerlimi == e_gecerlimi);
            if (varmi != null)
                kosullar.Add(c => c.varmi == varmi);
            Predicate<FederasyonBildirimGondermeAyariAYRINTI> kosul = Vt.birlestir(kosullar.ToArray());
            return kosul;
        }
    }
    public class FederasyonBildirimGondermeAyariAYRINTICizelgesi
    {
        /// <summary>
        /// Değeri boş olmayan değerlere göre verileri çeker.
        /// </summary>
        /// <param name="kosul"></param>
        /// <returns></returns>
        public static List<FederasyonBildirimGondermeAyariAYRINTI> ara(FederasyonBildirimGondermeAyariAYRINTIArama kosul)
        {
            using (veri.Varlik vari = new veri.Varlik()) { return vari.FederasyonBildirimGondermeAyariAYRINTIs.ToList().FindAll(kosul.kosulu()).OrderByDescending(p => p.federasyonBildirimGondermeAyariKimlik).ToList(); }
        }
        /// <summary>
        /// Girilen koşullara göre veri çeker.
        /// </summary>
        /// <param name="kosullar"></param>
        /// <returns></returns>
        public static List<FederasyonBildirimGondermeAyariAYRINTI> ara(params Predicate<FederasyonBildirimGondermeAyariAYRINTI>[] kosullar)
        {
            var kosul = Vt.birlestir(kosullar);
            using (veri.Varlik vari = new veri.Varlik()) { return vari.FederasyonBildirimGondermeAyariAYRINTIs.ToList().FindAll(kosul).OrderByDescending(p => p.federasyonBildirimGondermeAyariKimlik).ToList(); }
        }
        public static List<FederasyonBildirimGondermeAyariAYRINTI> tamami(Varlik kime)
        {
            return kime.FederasyonBildirimGondermeAyariAYRINTIs.Where(p => p.varmi == true).OrderByDescending(p => p.federasyonBildirimGondermeAyariKimlik).ToList();
        }
        public static List<FederasyonBildirimGondermeAyariAYRINTI> cek(Varlik kime)
        {
            return tamami(kime);
        }
        public static List<FederasyonBildirimGondermeAyariAYRINTI> cek()
        {
            using (veri.Varlik vari = new Varlik()) { return tamami(vari); }
        }
        public static FederasyonBildirimGondermeAyariAYRINTI tekliCek(Int32 kimlik, Varlik kime)
        {
            FederasyonBildirimGondermeAyariAYRINTI kayit = kime.FederasyonBildirimGondermeAyariAYRINTIs.FirstOrDefault(p => p.federasyonBildirimGondermeAyariKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }
    }
}
