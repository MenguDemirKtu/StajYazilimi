using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KullaniciRoluAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KullaniciRoluAYRINTI> bilesenler = KullaniciRoluAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KullaniciRoluAYRINTI> bilesenler = KullaniciRoluAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_kullaniciKimlik, "", dilKimlik);
            uyariVerInt32(i_rolKimlik, "", dilKimlik);
            uyariVerBool(e_gecerlimi, "", dilKimlik);
            uyariVerString(kullaniciAdi, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }
        public static KullaniciRoluAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciRoluAYRINTI sonuc = new KullaniciRoluAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciRoluAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        #region bu_sinifin_bagli_oldugu_sinif
        public Kullanici _KullaniciBilgisi()
        {
            return Kullanici.olustur(i_kullaniciKimlik);
        }
        #endregion bu_sinifin_bagli_oldugu_sinif
        /// <summary>
        /// Veri tabanından kayıtlı olan verisini çeker. Dolayısıyla yapılan bir değişikliği aktaran işlemi burada yeniden seçemeyiz.
        /// </summary>
        /// <returns></returns>
        public KullaniciRolu _verisi()
        {
            KullaniciRolu sonuc = KullaniciRolu.olustur(kullaniciRoluKimlik);
            return sonuc;
        }
        protected KullaniciRoluAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciRoluAYRINTICizelgesi.tekliCek(kullaniciRoluKimlik, vari); }
        }
        public static List<KullaniciRoluAYRINTI> ara(KullaniciRoluAYRINTIArama kosul)
        {
            return veriTabani.KullaniciRoluAYRINTICizelgesi.ara(kosul);
        }
        public static List<KullaniciRoluAYRINTI> ara(params Predicate<KullaniciRoluAYRINTI>[] kosullar)
        {
            return veriTabani.KullaniciRoluAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KullaniciRoluAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciRoluKimlik";
        }
        public override long _birincilAnahtar()
        {
            return kullaniciRoluKimlik;
        }
        #endregion
    }
}
