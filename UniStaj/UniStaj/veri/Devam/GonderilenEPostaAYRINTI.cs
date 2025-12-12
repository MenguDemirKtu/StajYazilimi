using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class GonderilenEPostaAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GonderilenEPostaAYRINTI> bilesenler = GonderilenEPostaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<GonderilenEPostaAYRINTI> bilesenler = GonderilenEPostaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return adres.ToString();
        }
        public static GonderilenEPostaAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                GonderilenEPostaAYRINTI sonuc = new GonderilenEPostaAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.GonderilenEPostaAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public GonderilenEPosta _verisi()
        {
            GonderilenEPosta sonuc = GonderilenEPosta.olustur(gonderilenEPostaKimlik);
            return sonuc;
        }
        protected GonderilenEPostaAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.GonderilenEPostaAYRINTICizelgesi.tekliCek(gonderilenEPostaKimlik, vari); }
        }
        public static List<GonderilenEPostaAYRINTI> ara(GonderilenEPostaAYRINTIArama kosul)
        {
            return veriTabani.GonderilenEPostaAYRINTICizelgesi.ara(kosul);
        }
        public static List<GonderilenEPostaAYRINTI> ara(params Predicate<GonderilenEPostaAYRINTI>[] kosullar)
        {
            return veriTabani.GonderilenEPostaAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "GonderilenEPostaAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "gonderilenEPostaKimlik";
        }
        public override long _birincilAnahtar()
        {
            return gonderilenEPostaKimlik;
        }
        #endregion
    }
}
