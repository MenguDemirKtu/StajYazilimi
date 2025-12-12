using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class FotografAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<FotografAYRINTI> bilesenler = FotografAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<FotografAYRINTI> bilesenler = FotografAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return ilgiliCizelge.ToString();
        }
        public static FotografAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                FotografAYRINTI sonuc = new FotografAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.FotografAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public Fotograf _verisi()
        {
            Fotograf sonuc = Fotograf.olustur(fotografKimlik);
            return sonuc;
        }
        protected FotografAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.FotografAYRINTICizelgesi.tekliCek(fotografKimlik, vari); }
        }
        public static List<FotografAYRINTI> ara(FotografAYRINTIArama kosul)
        {
            return veriTabani.FotografAYRINTICizelgesi.ara(kosul);
        }
        public static List<FotografAYRINTI> ara(params Predicate<FotografAYRINTI>[] kosullar)
        {
            return veriTabani.FotografAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "FotografAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "fotografKimlik";
        }
        public override long _birincilAnahtar()
        {
            return fotografKimlik;
        }
        #endregion
    }
}
