using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class YuklenenDosyalarAYRINTI : Bilesen
    {
        public string fizikselKonumu()
        {
            return Path.Combine(Genel.kayitKonumu, dosyaKonumu);
        }
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<YuklenenDosyalarAYRINTI> bilesenler = YuklenenDosyalarAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<YuklenenDosyalarAYRINTI> bilesenler = YuklenenDosyalarAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt64(ilgiliCizelgeKimlik, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return dosyaKonumu.ToString();
        }
        public static YuklenenDosyalarAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                YuklenenDosyalarAYRINTI sonuc = new YuklenenDosyalarAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.YuklenenDosyalarAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public YuklenenDosyalar _verisi()
        {
            YuklenenDosyalar sonuc = YuklenenDosyalar.olustur(yuklenenDosyalarKimlik);
            return sonuc;
        }
        protected YuklenenDosyalarAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.YuklenenDosyalarAYRINTICizelgesi.tekliCek(yuklenenDosyalarKimlik, vari); }
        }
        public static List<YuklenenDosyalarAYRINTI> ara(YuklenenDosyalarAYRINTIArama kosul)
        {
            return veriTabani.YuklenenDosyalarAYRINTICizelgesi.ara(kosul);
        }
        public static List<YuklenenDosyalarAYRINTI> ara(params Predicate<YuklenenDosyalarAYRINTI>[] kosullar)
        {
            return veriTabani.YuklenenDosyalarAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "YuklenenDosyalarAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "yuklenenDosyalarKimlik";
        }
        public override long _birincilAnahtar()
        {
            return yuklenenDosyalarKimlik;
        }
        #endregion
    }
}
