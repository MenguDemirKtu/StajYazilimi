using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class BYSSozcukAciklamaAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<BYSSozcukAciklamaAYRINTI> bilesenler = BYSSozcukAciklamaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<BYSSozcukAciklamaAYRINTI> bilesenler = BYSSozcukAciklamaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_bysSozcukKimlik, "", dilKimlik);
            uyariVerInt32(i_dilKimlik, "", dilKimlik);
            uyariVerString(sozcuk, "", dilKimlik);
            uyariVerInt32(bysSozcukKimlik, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_bysSozcukKimlik.ToString();
        }
        public static BYSSozcukAciklamaAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                BYSSozcukAciklamaAYRINTI sonuc = new BYSSozcukAciklamaAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.BYSSozcukAciklamaAYRINTICizelgesi.tekliCek(kimlik, vari); }
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
        public BYSSozcukAciklama _verisi()
        {
            BYSSozcukAciklama sonuc = BYSSozcukAciklama.olustur(bysSozcukAciklamaKimlik);
            return sonuc;
        }
        protected BYSSozcukAciklamaAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.BYSSozcukAciklamaAYRINTICizelgesi.tekliCek(bysSozcukAciklamaKimlik, vari); }
        }
        public static List<BYSSozcukAciklamaAYRINTI> ara(BYSSozcukAciklamaAYRINTIArama kosul)
        {
            return veriTabani.BYSSozcukAciklamaAYRINTICizelgesi.ara(kosul);
        }
        public static List<BYSSozcukAciklamaAYRINTI> ara(params Predicate<BYSSozcukAciklamaAYRINTI>[] kosullar)
        {
            return veriTabani.BYSSozcukAciklamaAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "BYSSozcukAciklamaAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "bysSozcukAciklamaKimlik";
        }
        public override long _birincilAnahtar()
        {
            return bysSozcukAciklamaKimlik;
        }
        #endregion
    }
}
