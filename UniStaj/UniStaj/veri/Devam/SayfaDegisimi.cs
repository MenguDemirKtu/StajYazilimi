using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class SayfaDegisimi : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SayfaDegisimiAYRINTI> bilesenler = SayfaDegisimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<SayfaDegisimiAYRINTI> bilesenler = SayfaDegisimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            if (ad == null)
                return "";
            else
                return ad.ToString();
        }
        public static SayfaDegisimi? olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SayfaDegisimi sonuc = new SayfaDegisimi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik())
                {
                    return veriTabani.SayfaDegisimiCizelgesi.tekliCek(kimlik, vari);
                }
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
        public SayfaDegisimiAYRINTI _ayrintisi()
        {
            SayfaDegisimiAYRINTI sonuc = SayfaDegisimiAYRINTI.olustur(sayfaDegisimiKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.SayfaDegisimiCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        public static List<SayfaDegisimi> ara(SayfaDegisimiArama kosul)
        {
            return veriTabani.SayfaDegisimiCizelgesi.ara(kosul);
        }
        public static List<SayfaDegisimi> ara(params Predicate<SayfaDegisimi>[] kosullar)
        {
            return veriTabani.SayfaDegisimiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.SayfaDegisimiCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "SayfaDegisimi";
        }
        public override string _turkceAdi()
        {
            return "Sayfa Değişimi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sayfaDegisimiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return sayfaDegisimiKimlik;
        }
        #endregion
    }
}
