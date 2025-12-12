using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class HataBildirimiAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<HataBildirimiAYRINTI> bilesenler = HataBildirimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<HataBildirimiAYRINTI> bilesenler = HataBildirimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return i_yoneticiKimlik.ToString();
        }
        public static HataBildirimiAYRINTI olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                HataBildirimiAYRINTI sonuc = new HataBildirimiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.HataBildirimiAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public HataBildirimi _verisi()
        {
            HataBildirimi sonuc = HataBildirimi.olustur(hataBildirimiKimlik);
            return sonuc;
        }
        protected HataBildirimiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.HataBildirimiAYRINTICizelgesi.tekliCek(hataBildirimiKimlik, vari); }
        }
        public static List<HataBildirimiAYRINTI> ara(HataBildirimiAYRINTIArama kosul)
        {
            return veriTabani.HataBildirimiAYRINTICizelgesi.ara(kosul);
        }
        public static List<HataBildirimiAYRINTI> ara(params Predicate<HataBildirimiAYRINTI>[] kosullar)
        {
            return veriTabani.HataBildirimiAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "HataBildirimiAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "hataBildirimiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return hataBildirimiKimlik;
        }
        #endregion
    }
}
