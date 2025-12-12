using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class YuklenenDosyalar : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<YuklenenDosyalarAYRINTI> bilesenler = YuklenenDosyalarAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<YuklenenDosyalarAYRINTI> bilesenler = YuklenenDosyalarAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt64(ilgiliCizelgeKimlik, "İlgili Çizelge Kimlik", dilKimlik);
        }
        public override string _tanimi()
        {
            return dosyaKonumu.ToString();
        }
        public static YuklenenDosyalar olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                YuklenenDosyalar sonuc = new YuklenenDosyalar();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.YuklenenDosyalarCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public static YuklenenDosyalar olustur(veri.Varlik vari, object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                YuklenenDosyalar sonuc = new YuklenenDosyalar();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.YuklenenDosyalarCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        public YuklenenDosyalarAYRINTI _ayrintisi()
        {
            YuklenenDosyalarAYRINTI sonuc = YuklenenDosyalarAYRINTI.olustur(yuklenenDosyalarKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik())
            {
                veriTabani.YuklenenDosyalarCizelgesi.kaydet(this, vari, yedeklensinmi);
            }
        }
        public void kaydet(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            veriTabani.YuklenenDosyalarCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected YuklenenDosyalar cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.YuklenenDosyalarCizelgesi.tekliCek(yuklenenDosyalarKimlik, vari);
            }
        }

        public static List<YuklenenDosyalar> ara(params Predicate<YuklenenDosyalar>[] kosullar)
        {
            return veriTabani.YuklenenDosyalarCizelgesi.ara(kosullar);
        }
        public void sil()
        {
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "YuklenenDosyalar";
        }
        public override string _turkceAdi()
        {
            return "Yüklenen Dosyalar";
        }
        public override string _birincilAnahtarAdi()
        {
            return "yuklenenDosyalarKimlik";
        }
        public override long _birincilAnahtar()
        {
            return yuklenenDosyalarKimlik;
        }
        #endregion
    }
}
