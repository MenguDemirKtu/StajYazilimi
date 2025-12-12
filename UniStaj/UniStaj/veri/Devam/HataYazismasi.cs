using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class HataYazismasi : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<HataYazismasiAYRINTI> bilesenler = HataYazismasiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<HataYazismasiAYRINTI> bilesenler = HataYazismasiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt64(i_hataBildirimiKimlik, "", dilKimlik);
            uyariVerInt64(i_yoneticiKimlik, "", dilKimlik);
            if (metin == null)
                uyariVer("Metin boş olamaz", dilKimlik);
            if (metin.Length < 10)
                uyariVer("Metin yeterli uzunlukta değil", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_hataBildirimiKimlik.ToString();
        }
        public static HataYazismasi olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                HataYazismasi sonuc = new HataYazismasi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.HataYazismasiCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            varmi = true;
        }
        public HataYazismasiAYRINTI _ayrintisi()
        {
            HataYazismasiAYRINTI sonuc = HataYazismasiAYRINTI.olustur(hataYasizmasiKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.HataYazismasiCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected HataYazismasi cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.HataYazismasiCizelgesi.tekliCek(hataYasizmasiKimlik, vari); }
        }
        public static List<HataYazismasi> ara(HataYazismasiArama kosul)
        {
            return veriTabani.HataYazismasiCizelgesi.ara(kosul);
        }
        public static List<HataYazismasi> ara(params Predicate<HataYazismasi>[] kosullar)
        {
            return veriTabani.HataYazismasiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.HataYazismasiCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "HataYazismasi";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "hataYasizmasiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return hataYasizmasiKimlik;
        }
        #endregion
    }
}
