using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class HataBildirimi : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<HataBildirimiAYRINTI> bilesenler = HataBildirimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<HataBildirimiAYRINTI> bilesenler = HataBildirimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            if (string.IsNullOrEmpty(baslik))
                uyariVer("Hata başlığını giriniz", dilKimlik);
            if (baslik.Length < 5)
                uyariVer("Başlık yeterli uzunlukta değil", dilKimlik);
            if (string.IsNullOrEmpty(hataAciklamasi))
                uyariVer("Lütfen hata açıklaması giriniz", dilKimlik);
            if (hataAciklamasi.Length < 15)
                uyariVer("Hata açıklamanız yeterli uzunlukta değil. Daha ayrıntılı bir hata açıklaması yazınız?", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_yoneticiKimlik.ToString();
        }
        public static HataBildirimi olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                HataBildirimi sonuc = new HataBildirimi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.HataBildirimiCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            varmi = true;
            kodu = null;
        }
        public HataBildirimiAYRINTI _ayrintisi()
        {
            HataBildirimiAYRINTI sonuc = HataBildirimiAYRINTI.olustur(hataBildirimiKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.HataBildirimiCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected HataBildirimi cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.HataBildirimiCizelgesi.tekliCek(hataBildirimiKimlik, vari); }
        }
        public static List<HataBildirimi> ara(HataBildirimiArama kosul)
        {
            return veriTabani.HataBildirimiCizelgesi.ara(kosul);
        }
        public static List<HataBildirimi> ara(params Predicate<HataBildirimi>[] kosullar)
        {
            return veriTabani.HataBildirimiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.HataBildirimiCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "HataBildirimi";
        }
        public override string _turkceAdi()
        {
            return "Hata Bildirimi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "hataBildirimiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return hataBildirimiKimlik;
        }
        #endregion
    }
}
