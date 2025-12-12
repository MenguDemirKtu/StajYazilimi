using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KullaniciRolWebSayfasiIzniAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KullaniciRolWebSayfasiIzniAYRINTI> bilesenler = KullaniciRolWebSayfasiIzniAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KullaniciRolWebSayfasiIzniAYRINTI> bilesenler = KullaniciRolWebSayfasiIzniAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_rolKimlik, "", dilKimlik);
            uyariVerInt32(i_kullaniciKimlik, "", dilKimlik);
            uyariVerInt32(i_webSayfasiKimlik, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_rolKimlik.ToString();
        }
        public static KullaniciRolWebSayfasiIzniAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciRolWebSayfasiIzniAYRINTI sonuc = new KullaniciRolWebSayfasiIzniAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciRolWebSayfasiIzniAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        protected KullaniciRolWebSayfasiIzniAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciRolWebSayfasiIzniAYRINTICizelgesi.tekliCek(rolWebSayfasiIzniKimlik, vari); }
        }
        public static List<KullaniciRolWebSayfasiIzniAYRINTI> ara(KullaniciRolWebSayfasiIzniAYRINTIArama kosul)
        {
            return veriTabani.KullaniciRolWebSayfasiIzniAYRINTICizelgesi.ara(kosul);
        }
        public static List<KullaniciRolWebSayfasiIzniAYRINTI> ara(params Predicate<KullaniciRolWebSayfasiIzniAYRINTI>[] kosullar)
        {
            return veriTabani.KullaniciRolWebSayfasiIzniAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KullaniciRolWebSayfasiIzniAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
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
