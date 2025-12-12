using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class ref_sayfaTuru : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_sayfaTuru> bilesenler = ref_sayfaTuru.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<ref_sayfaTuru> bilesenler = ref_sayfaTuru.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return sayfaTuru.ToString();
        }
        public static ref_sayfaTuru olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_sayfaTuru sonuc = new ref_sayfaTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.ref_sayfaTuruCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        protected ref_sayfaTuru cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.ref_sayfaTuruCizelgesi.tekliCek(sayfaTuruKimlik, vari); }
        }
        public static List<ref_sayfaTuru> ara(ref_sayfaTuruArama kosul)
        {
            return veriTabani.ref_sayfaTuruCizelgesi.ara(kosul);
        }
        public static List<ref_sayfaTuru> ara(params Predicate<ref_sayfaTuru>[] kosullar)
        {
            return veriTabani.ref_sayfaTuruCizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "ref_sayfaTuru";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sayfaTuruKimlik";
        }
        public override long _birincilAnahtar()
        {
            return sayfaTuruKimlik;
        }
        #endregion
    }
}
