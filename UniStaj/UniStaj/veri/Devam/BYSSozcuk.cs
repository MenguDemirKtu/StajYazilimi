using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class BYSSozcuk : Bilesen
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
            uyariVerString(sozcuk, "Sözcük");
        }
        public override string _tanimi()
        {
            return sozcuk.ToString();
        }
        public static BYSSozcuk olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                BYSSozcuk sonuc = new BYSSozcuk();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.BYSSozcukCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            varmi = true;
        }
        public BYSSozcukAYRINTI _ayrintisi()
        {
            BYSSozcukAYRINTI sonuc = BYSSozcukAYRINTI.olustur(bysSozcukKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.BYSSozcukCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected BYSSozcuk cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.BYSSozcukCizelgesi.tekliCek(bysSozcukKimlik, vari); }
        }
        public static List<BYSSozcuk> ara(BYSSozcukArama kosul)
        {
            return veriTabani.BYSSozcukCizelgesi.ara(kosul);
        }
        public static List<BYSSozcuk> ara(params Predicate<BYSSozcuk>[] kosullar)
        {
            return veriTabani.BYSSozcukCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.BYSSozcukCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "BYSSozcuk";
        }
        public override string _turkceAdi()
        {
            return "BYSSozcuk";
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
