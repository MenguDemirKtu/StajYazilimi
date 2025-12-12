using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class EPostaKalibiAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<EPostaKalibiAYRINTI> bilesenler = EPostaKalibiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<EPostaKalibiAYRINTI> bilesenler = EPostaKalibiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_ePostaTuruKimlik, "", dilKimlik);
            uyariVerBool(e_gecerlimi, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return kalipBasligi.ToString();
        }
        public static EPostaKalibiAYRINTI olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                EPostaKalibiAYRINTI sonuc = new EPostaKalibiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.EPostaKalibiAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        #region bu_sinifin_bagli_oldugu_sinif
        public ref_EPostaTuru _ref_EPostaTuruBilgisi()
        {
            return ref_EPostaTuru.olustur(i_ePostaTuruKimlik);
        }
        #endregion bu_sinifin_bagli_oldugu_sinif
        /// <summary>
        /// Veri tabanından kayıtlı olan verisini çeker. Dolayısıyla yapılan bir değişikliği aktaran işlemi burada yeniden seçemeyiz.
        /// </summary>
        /// <returns></returns>
        public EPostaKalibi _verisi()
        {
            EPostaKalibi sonuc = EPostaKalibi.olustur(ePostaKalibiKimlik);
            return sonuc;
        }
        protected EPostaKalibiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.EPostaKalibiAYRINTICizelgesi.tekliCek(ePostaKalibiKimlik, vari); }
        }
        public static List<EPostaKalibiAYRINTI> ara(EPostaKalibiAYRINTIArama kosul)
        {
            return veriTabani.EPostaKalibiAYRINTICizelgesi.ara(kosul);
        }
        public static List<EPostaKalibiAYRINTI> ara(params Predicate<EPostaKalibiAYRINTI>[] kosullar)
        {
            return veriTabani.EPostaKalibiAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "EPostaKalibiAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "ePostaKalibiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return ePostaKalibiKimlik;
        }
        #endregion
    }
}
