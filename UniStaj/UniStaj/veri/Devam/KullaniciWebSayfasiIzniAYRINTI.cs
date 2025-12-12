using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KullaniciWebSayfasiIzniAYRINTI : Bilesen
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
            uyariVerString(kullaniciAdi, "");
            uyariVerBool(e_gormeIzniVarmi, "", dilKimlik);
            uyariVerBool(e_eklemeIzniVarmi, "", dilKimlik);
            uyariVerBool(e_silmeIzniVarmi, "", dilKimlik);
            uyariVerBool(e_guncellemeIzniVarmi, "", dilKimlik);
            uyariVerInt32(i_modulKimlik, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }
        public static KullaniciWebSayfasiIzniAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciWebSayfasiIzniAYRINTI sonuc = new KullaniciWebSayfasiIzniAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciWebSayfasiIzniAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        protected KullaniciWebSayfasiIzniAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciWebSayfasiIzniAYRINTICizelgesi.tekliCek(kullaniciWebSayfasiIzniKimlik, vari); }
        }
        public static List<KullaniciWebSayfasiIzniAYRINTI> ara(KullaniciWebSayfasiIzniAYRINTIArama kosul)
        {
            return veriTabani.KullaniciWebSayfasiIzniAYRINTICizelgesi.ara(kosul);
        }
        public static List<KullaniciWebSayfasiIzniAYRINTI> ara(params Predicate<KullaniciWebSayfasiIzniAYRINTI>[] kosullar)
        {
            return veriTabani.KullaniciWebSayfasiIzniAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KullaniciWebSayfasiIzniAYRINTI";
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
