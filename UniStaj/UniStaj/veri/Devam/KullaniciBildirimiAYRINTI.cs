using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KullaniciBildirimiAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KullaniciBildirimiAYRINTI> bilesenler = KullaniciBildirimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KullaniciBildirimiAYRINTI> bilesenler = KullaniciBildirimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_kullaniciKimlik, "", dilKimlik);
            uyariVerString(kullaniciAdi, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }
        public static KullaniciBildirimiAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciBildirimiAYRINTI sonuc = new KullaniciBildirimiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciBildirimiAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        /// <summary>
        /// Veri tabanından kayıtlı olan verisini çeker. Dolayısıyla yapılan bir değişikliği aktaran işlemi burada yeniden seçemeyiz.
        /// </summary>
        /// <returns></returns>
        public KullaniciBildirimi _verisi()
        {
            KullaniciBildirimi sonuc = KullaniciBildirimi.olustur(kullaniciBildirimiKimlik);
            return sonuc;
        }
        protected KullaniciBildirimiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciBildirimiAYRINTICizelgesi.tekliCek(kullaniciBildirimiKimlik, vari); }
        }
        public static List<KullaniciBildirimiAYRINTI> ara(KullaniciBildirimiAYRINTIArama kosul)
        {
            return veriTabani.KullaniciBildirimiAYRINTICizelgesi.ara(kosul);
        }
        public static List<KullaniciBildirimiAYRINTI> ara(params Predicate<KullaniciBildirimiAYRINTI>[] kosullar)
        {
            return veriTabani.KullaniciBildirimiAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KullaniciBildirimiAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciBildirimiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return kullaniciBildirimiKimlik;
        }
        #endregion
    }
}
