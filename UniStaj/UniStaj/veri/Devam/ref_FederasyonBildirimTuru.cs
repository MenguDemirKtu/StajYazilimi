using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class ref_FederasyonBildirimTuru : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_FederasyonBildirimTuru> bilesenler = ref_FederasyonBildirimTuru.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<ref_FederasyonBildirimTuru> bilesenler = ref_FederasyonBildirimTuru.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return federasyonBildirimTuruAdi.ToString();
        }
        public static ref_FederasyonBildirimTuru olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_FederasyonBildirimTuru sonuc = new ref_FederasyonBildirimTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.ref_FederasyonBildirimTuruCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        protected ref_FederasyonBildirimTuru cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.ref_FederasyonBildirimTuruCizelgesi.tekliCek(federasyonBildirimTuruKimlik, vari); }
        }
        public static List<ref_FederasyonBildirimTuru> ara(ref_FederasyonBildirimTuruArama kosul)
        {
            return veriTabani.ref_FederasyonBildirimTuruCizelgesi.ara(kosul);
        }
        public static List<ref_FederasyonBildirimTuru> ara(params Predicate<ref_FederasyonBildirimTuru>[] kosullar)
        {
            return veriTabani.ref_FederasyonBildirimTuruCizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "ref_FederasyonBildirimTuru";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "federasyonBildirimTuruKimlik";
        }
        public override long _birincilAnahtar()
        {
            return federasyonBildirimTuruKimlik;
        }
        #endregion
    }
}
