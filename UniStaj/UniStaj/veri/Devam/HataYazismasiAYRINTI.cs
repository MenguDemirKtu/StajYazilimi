using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class HataYazismasiAYRINTI : Bilesen
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
        }
        public override string _tanimi()
        {
            return i_hataBildirimiKimlik.ToString();
        }
        public static HataYazismasiAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                HataYazismasiAYRINTI sonuc = new HataYazismasiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.HataYazismasiAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public HataYazismasi _verisi()
        {
            HataYazismasi sonuc = HataYazismasi.olustur(hataYasizmasiKimlik);
            return sonuc;
        }
        protected HataYazismasiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.HataYazismasiAYRINTICizelgesi.tekliCek(hataYasizmasiKimlik, vari); }
        }
        public static List<HataYazismasiAYRINTI> ara(HataYazismasiAYRINTIArama kosul)
        {
            return veriTabani.HataYazismasiAYRINTICizelgesi.ara(kosul);
        }
        public static List<HataYazismasiAYRINTI> ara(params Predicate<HataYazismasiAYRINTI>[] kosullar)
        {
            return veriTabani.HataYazismasiAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "HataYazismasiAYRINTI";
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
