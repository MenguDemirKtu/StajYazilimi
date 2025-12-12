using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class SistemHatasi : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SistemHatasiAYRINTI> bilesenler = SistemHatasiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<SistemHatasiAYRINTI> bilesenler = SistemHatasiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return tarih.ToString();
        }
        public static SistemHatasi olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SistemHatasi sonuc = new SistemHatasi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.SistemHatasiCizelgesi.tekliCek(kimlik, vari); }
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
        public SistemHatasiAYRINTI _ayrintisi()
        {
            SistemHatasiAYRINTI sonuc = SistemHatasiAYRINTI.olustur(sistemHatasiKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.SistemHatasiCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected SistemHatasi cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.SistemHatasiCizelgesi.tekliCek(sistemHatasiKimlik, vari); }
        }
        public static List<SistemHatasi> ara(SistemHatasiArama kosul)
        {
            return veriTabani.SistemHatasiCizelgesi.ara(kosul);
        }
        public static List<SistemHatasi> ara(params Predicate<SistemHatasi>[] kosullar)
        {
            return veriTabani.SistemHatasiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.SistemHatasiCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "SistemHatasi";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sistemHatasiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return sistemHatasiKimlik;
        }
        #endregion
    }
}
