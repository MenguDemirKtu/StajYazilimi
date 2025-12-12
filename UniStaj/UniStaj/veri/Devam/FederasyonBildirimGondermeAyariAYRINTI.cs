using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class FederasyonBildirimGondermeAyariAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<FederasyonBildirimGondermeAyariAYRINTI> bilesenler = FederasyonBildirimGondermeAyariAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<FederasyonBildirimGondermeAyariAYRINTI> bilesenler = FederasyonBildirimGondermeAyariAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt64(i_kullaniciKimlik, "", dilKimlik);
            uyariVerString(kullaniciAdi, "", dilKimlik);
            uyariVerInt32(i_federasyonBildirimTuruKimlik, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }
        public static FederasyonBildirimGondermeAyariAYRINTI olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                FederasyonBildirimGondermeAyariAYRINTI sonuc = new FederasyonBildirimGondermeAyariAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.FederasyonBildirimGondermeAyariAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public FederasyonBildirimGondermeAyari _verisi()
        {
            FederasyonBildirimGondermeAyari sonuc = FederasyonBildirimGondermeAyari.olustur(federasyonBildirimGondermeAyariKimlik);
            return sonuc;
        }
        protected FederasyonBildirimGondermeAyariAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.FederasyonBildirimGondermeAyariAYRINTICizelgesi.tekliCek(federasyonBildirimGondermeAyariKimlik, vari); }
        }
        public static List<FederasyonBildirimGondermeAyariAYRINTI> ara(FederasyonBildirimGondermeAyariAYRINTIArama kosul)
        {
            return veriTabani.FederasyonBildirimGondermeAyariAYRINTICizelgesi.ara(kosul);
        }
        public static List<FederasyonBildirimGondermeAyariAYRINTI> ara(params Predicate<FederasyonBildirimGondermeAyariAYRINTI>[] kosullar)
        {
            return veriTabani.FederasyonBildirimGondermeAyariAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "FederasyonBildirimGondermeAyariAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "federasyonBildirimGondermeAyariKimlik";
        }
        public override long _birincilAnahtar()
        {
            return federasyonBildirimGondermeAyariKimlik;
        }
        #endregion
    }
}
