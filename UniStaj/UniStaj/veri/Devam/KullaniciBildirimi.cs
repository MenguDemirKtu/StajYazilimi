using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KullaniciBildirimi : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KullaniciBildirimiAYRINTI> bilesenler = KullaniciBildirimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KullaniciBildirimiAYRINTI> bilesenler = KullaniciBildirimiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_kullaniciKimlik, "Kullanıcı", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }
        public static KullaniciBildirimi olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciBildirimi sonuc = new KullaniciBildirimi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciBildirimiCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        public KullaniciBildirimiAYRINTI _ayrintisi()
        {
            KullaniciBildirimiAYRINTI sonuc = KullaniciBildirimiAYRINTI.olustur(kullaniciBildirimiKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.KullaniciBildirimiCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected KullaniciBildirimi cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciBildirimiCizelgesi.tekliCek(kullaniciBildirimiKimlik, vari); }
        }
        public static List<KullaniciBildirimi> ara(KullaniciBildirimiArama kosul)
        {
            return veriTabani.KullaniciBildirimiCizelgesi.ara(kosul);
        }
        public static List<KullaniciBildirimi> ara(params Predicate<KullaniciBildirimi>[] kosullar)
        {
            return veriTabani.KullaniciBildirimiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.KullaniciBildirimiCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KullaniciBildirimi";
        }
        public override string _turkceAdi()
        {
            return "KullanıcıBildirimi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciBildirimiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return kullaniciBildirimiKimlik;
        }
        #endregion
    }
}
