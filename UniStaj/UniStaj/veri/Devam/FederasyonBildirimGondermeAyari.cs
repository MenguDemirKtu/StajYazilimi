using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class FederasyonBildirimGondermeAyari : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<FederasyonBildirimGondermeAyariAYRINTI> bilesenler = FederasyonBildirimGondermeAyariAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<FederasyonBildirimGondermeAyariAYRINTI> bilesenler = FederasyonBildirimGondermeAyariAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt64(i_kullaniciKimlik, "", dilKimlik);
            uyariVerInt32(i_federasyonBildirimTuruKimlik, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }
        public static FederasyonBildirimGondermeAyari olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                FederasyonBildirimGondermeAyari sonuc = new FederasyonBildirimGondermeAyari();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.FederasyonBildirimGondermeAyariCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            e_gecerlimi = true;
            varmi = true;
        }
        public FederasyonBildirimGondermeAyariAYRINTI _ayrintisi()
        {
            FederasyonBildirimGondermeAyariAYRINTI sonuc = FederasyonBildirimGondermeAyariAYRINTI.olustur(federasyonBildirimGondermeAyariKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.FederasyonBildirimGondermeAyariCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected FederasyonBildirimGondermeAyari cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.FederasyonBildirimGondermeAyariCizelgesi.tekliCek(federasyonBildirimGondermeAyariKimlik, vari); }
        }
        public static List<FederasyonBildirimGondermeAyari> ara(FederasyonBildirimGondermeAyariArama kosul)
        {
            return veriTabani.FederasyonBildirimGondermeAyariCizelgesi.ara(kosul);
        }
        public static List<FederasyonBildirimGondermeAyari> ara(params Predicate<FederasyonBildirimGondermeAyari>[] kosullar)
        {
            return veriTabani.FederasyonBildirimGondermeAyariCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.FederasyonBildirimGondermeAyariCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "FederasyonBildirimGondermeAyari";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "federasyonBildirimGondermeAyariKimlik";
        }
        public override long _birincilAnahtar()
        {
            return federasyonBildirimGondermeAyariKimlik;
        }
        #endregion
    }
}
