using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KullaniciWebSayfasiIzni : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KullaniciWebSayfasiIzniAYRINTI> bilesenler = KullaniciWebSayfasiIzniAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KullaniciWebSayfasiIzniAYRINTI> bilesenler = KullaniciWebSayfasiIzniAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }
        public static KullaniciWebSayfasiIzni olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciWebSayfasiIzni sonuc = new KullaniciWebSayfasiIzni();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciWebSayfasiIzniCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        public KullaniciWebSayfasiIzniAYRINTI _ayrintisi()
        {
            KullaniciWebSayfasiIzniAYRINTI sonuc = KullaniciWebSayfasiIzniAYRINTI.olustur(kullaniciWebSayfasiIzniKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.KullaniciWebSayfasiIzniCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected KullaniciWebSayfasiIzni cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciWebSayfasiIzniCizelgesi.tekliCek(kullaniciWebSayfasiIzniKimlik, vari); }
        }
        public static List<KullaniciWebSayfasiIzni> ara(KullaniciWebSayfasiIzniArama kosul)
        {
            return veriTabani.KullaniciWebSayfasiIzniCizelgesi.ara(kosul);
        }
        public static List<KullaniciWebSayfasiIzni> ara(params Predicate<KullaniciWebSayfasiIzni>[] kosullar)
        {
            return veriTabani.KullaniciWebSayfasiIzniCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.KullaniciWebSayfasiIzniCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KullaniciWebSayfasiIzni";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciWebSayfasiIzniKimlik";
        }
        public override long _birincilAnahtar()
        {
            return kullaniciWebSayfasiIzniKimlik;
        }
        #endregion
    }
}
