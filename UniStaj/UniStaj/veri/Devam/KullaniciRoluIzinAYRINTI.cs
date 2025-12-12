using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KullaniciRoluIzinAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KullaniciRoluIzinAYRINTI> bilesenler = KullaniciRoluIzinAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KullaniciRoluIzinAYRINTI> bilesenler = KullaniciRoluIzinAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_webSayfasiKimlik, ".", dilKimlik);
            uyariVerInt32(i_rolKimlik, ".", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_webSayfasiKimlik.ToString();
        }
        public static KullaniciRoluIzinAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciRoluIzinAYRINTI sonuc = new KullaniciRoluIzinAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciRoluIzinAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        protected KullaniciRoluIzinAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciRoluIzinAYRINTICizelgesi.tekliCek(rolWebSayfasiIzniKimlik, vari); }
        }
        public static List<KullaniciRoluIzinAYRINTI> ara(KullaniciRoluIzinAYRINTIArama kosul)
        {
            return veriTabani.KullaniciRoluIzinAYRINTICizelgesi.ara(kosul);
        }
        public static List<KullaniciRoluIzinAYRINTI> ara(params Predicate<KullaniciRoluIzinAYRINTI>[] kosullar)
        {
            return veriTabani.KullaniciRoluIzinAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KullaniciRoluIzinAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "KullaniciRoluIzinAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "rolWebSayfasiIzniKimlik";
        }
        public override long _birincilAnahtar()
        {
            return rolWebSayfasiIzniKimlik;
        }
        #endregion
    }
}
