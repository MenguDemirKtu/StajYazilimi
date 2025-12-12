using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class BYSSozcukAciklama : Bilesen
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
            uyariVerInt32(i_bysSozcukKimlik, "BYS Sözcüğü", dilKimlik);
            uyariVerInt32(i_dilKimlik, "Dil", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_bysSozcukKimlik.ToString();
        }
        public static BYSSozcukAciklama olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                BYSSozcukAciklama sonuc = new BYSSozcukAciklama();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.BYSSozcukAciklamaCizelgesi.tekliCek(kimlik, vari); }
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
        public BYSSozcukAciklamaAYRINTI _ayrintisi()
        {
            BYSSozcukAciklamaAYRINTI sonuc = BYSSozcukAciklamaAYRINTI.olustur(bysSozcukAciklamaKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.BYSSozcukAciklamaCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected BYSSozcukAciklama cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.BYSSozcukAciklamaCizelgesi.tekliCek(bysSozcukAciklamaKimlik, vari); }
        }
        public static List<BYSSozcukAciklama> ara(BYSSozcukAciklamaArama kosul)
        {
            return veriTabani.BYSSozcukAciklamaCizelgesi.ara(kosul);
        }
        public static List<BYSSozcukAciklama> ara(params Predicate<BYSSozcukAciklama>[] kosullar)
        {
            return veriTabani.BYSSozcukAciklamaCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.BYSSozcukAciklamaCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "BYSSozcukAciklama";
        }
        public override string _turkceAdi()
        {
            return "BYS Sözcük Açıklaması";
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
