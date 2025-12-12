using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class SistemHatasiAYRINTI : Bilesen
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
        public static SistemHatasiAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SistemHatasiAYRINTI sonuc = new SistemHatasiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.SistemHatasiAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public SistemHatasi _verisi()
        {
            SistemHatasi sonuc = SistemHatasi.olustur(sistemHatasiKimlik);
            return sonuc;
        }
        protected SistemHatasiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.SistemHatasiAYRINTICizelgesi.tekliCek(sistemHatasiKimlik, vari); }
        }
        public static List<SistemHatasiAYRINTI> ara(SistemHatasiAYRINTIArama kosul)
        {
            return veriTabani.SistemHatasiAYRINTICizelgesi.ara(kosul);
        }
        public static List<SistemHatasiAYRINTI> ara(params Predicate<SistemHatasiAYRINTI>[] kosullar)
        {
            return veriTabani.SistemHatasiAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "SistemHatasiAYRINTI";
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
