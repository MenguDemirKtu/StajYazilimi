using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class GonderilenSMSAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GonderilenSMSAYRINTI> bilesenler = GonderilenSMSAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<GonderilenSMSAYRINTI> bilesenler = GonderilenSMSAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return telefon.ToString();
        }
        public static GonderilenSMSAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                GonderilenSMSAYRINTI sonuc = new GonderilenSMSAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.GonderilenSMSAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public GonderilenSMS _verisi()
        {
            GonderilenSMS sonuc = GonderilenSMS.olustur(gonderilenSMSKimlik);
            return sonuc;
        }
        protected GonderilenSMSAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.GonderilenSMSAYRINTICizelgesi.tekliCek(gonderilenSMSKimlik, vari); }
        }
        public static List<GonderilenSMSAYRINTI> ara(GonderilenSMSAYRINTIArama kosul)
        {
            return veriTabani.GonderilenSMSAYRINTICizelgesi.ara(kosul);
        }
        public static List<GonderilenSMSAYRINTI> ara(params Predicate<GonderilenSMSAYRINTI>[] kosullar)
        {
            return veriTabani.GonderilenSMSAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "GonderilenSMSAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "gonderilenSMSKimlik";
        }
        public override long _birincilAnahtar()
        {
            return gonderilenSMSKimlik;
        }
        #endregion
    }
}
