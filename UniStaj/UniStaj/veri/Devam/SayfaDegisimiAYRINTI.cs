using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class SayfaDegisimiAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SayfaDegisimiAYRINTI> bilesenler = SayfaDegisimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<SayfaDegisimiAYRINTI> bilesenler = SayfaDegisimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            if (ad == null)
                return "";
            return ad.ToString();
        }
        public static SayfaDegisimiAYRINTI olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SayfaDegisimiAYRINTI sonuc = new SayfaDegisimiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.SayfaDegisimiAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }


        protected SayfaDegisimiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.SayfaDegisimiAYRINTICizelgesi.tekliCek(sayfaDegisimiKimlik, vari); }
        }
        public static List<SayfaDegisimiAYRINTI> ara(SayfaDegisimiAYRINTIArama kosul)
        {
            return veriTabani.SayfaDegisimiAYRINTICizelgesi.ara(kosul);
        }
        public static List<SayfaDegisimiAYRINTI> ara(params Predicate<SayfaDegisimiAYRINTI>[] kosullar)
        {
            return veriTabani.SayfaDegisimiAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "SayfaDegisimiAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sayfaDegisimiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return sayfaDegisimiKimlik;
        }
        #endregion
    }
}
