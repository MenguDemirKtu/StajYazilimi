using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KisiAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KisiAYRINTI> bilesenler = KisiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KisiAYRINTI> bilesenler = KisiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return tcKimlikNo.ToString();
        }
        public static KisiAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KisiAYRINTI sonuc = new KisiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KisiAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        #region bu_sinifina_bagli_siniflar
        #endregion bu_sinifina_bagli_siniflar
        /// <summary>
        /// Veri tabanından kayıtlı olan verisini çeker. Dolayısıyla yapılan bir değişikliği aktaran işlemi burada yeniden seçemeyiz.
        /// </summary>
        /// <returns></returns>
        public Kisi _verisi()
        {
            Kisi sonuc = Kisi.olustur(kisiKimlik);
            return sonuc;
        }
        protected KisiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KisiAYRINTICizelgesi.tekliCek(kisiKimlik, vari); }
        }
        public static List<KisiAYRINTI> ara(KisiAYRINTIArama kosul)
        {
            return veriTabani.KisiAYRINTICizelgesi.ara(kosul);
        }
        public static List<KisiAYRINTI> ara(params Predicate<KisiAYRINTI>[] kosullar)
        {
            return veriTabani.KisiAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KisiAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kisiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return kisiKimlik;
        }
        #endregion
    }
}
