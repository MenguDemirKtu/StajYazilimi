using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class BYSSozcukAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<BYSSozcukAYRINTI> bilesenler = BYSSozcukAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<BYSSozcukAYRINTI> bilesenler = BYSSozcukAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(sozcuk, "");
        }
        public override string _tanimi()
        {
            return sozcuk.ToString();
        }
        public static BYSSozcukAYRINTI olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                BYSSozcukAYRINTI sonuc = new BYSSozcukAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.BYSSozcukAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public BYSSozcuk _verisi()
        {
            BYSSozcuk sonuc = BYSSozcuk.olustur(bysSozcukKimlik);
            return sonuc;
        }
        protected BYSSozcukAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.BYSSozcukAYRINTICizelgesi.tekliCek(bysSozcukKimlik, vari); }
        }
        public static List<BYSSozcukAYRINTI> ara(BYSSozcukAYRINTIArama kosul)
        {
            return veriTabani.BYSSozcukAYRINTICizelgesi.ara(kosul);
        }
        public static List<BYSSozcukAYRINTI> ara(params Predicate<BYSSozcukAYRINTI>[] kosullar)
        {
            return veriTabani.BYSSozcukAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "BYSSozcukAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "bysSozcukKimlik";
        }
        public override long _birincilAnahtar()
        {
            return bysSozcukKimlik;
        }
        #endregion
    }
}
