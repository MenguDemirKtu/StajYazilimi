using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class RolWebSayfasiIzniAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<RolWebSayfasiIzniAYRINTI> bilesenler = RolWebSayfasiIzniAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<RolWebSayfasiIzniAYRINTI> bilesenler = RolWebSayfasiIzniAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return i_rolKimlik.ToString();
        }
        public static RolWebSayfasiIzniAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                RolWebSayfasiIzniAYRINTI sonuc = new RolWebSayfasiIzniAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.RolWebSayfasiIzniAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public RolWebSayfasiIzni _verisi()
        {
            RolWebSayfasiIzni sonuc = RolWebSayfasiIzni.olustur(rolWebSayfasiIzniKimlik);
            return sonuc;
        }
        protected RolWebSayfasiIzniAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.RolWebSayfasiIzniAYRINTICizelgesi.tekliCek(rolWebSayfasiIzniKimlik, vari); }
        }
        public static List<RolWebSayfasiIzniAYRINTI> ara(RolWebSayfasiIzniAYRINTIArama kosul)
        {
            return veriTabani.RolWebSayfasiIzniAYRINTICizelgesi.ara(kosul);
        }
        public static List<RolWebSayfasiIzniAYRINTI> ara(params Predicate<RolWebSayfasiIzniAYRINTI>[] kosullar)
        {
            return veriTabani.RolWebSayfasiIzniAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "RolWebSayfasiIzniAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "rolWebSayfasiIzniKimlik";
        }
        public override long _birincilAnahtar()
        {
            return rolWebSayfasiIzniKimlik;
        }
        #endregion
    }
}
