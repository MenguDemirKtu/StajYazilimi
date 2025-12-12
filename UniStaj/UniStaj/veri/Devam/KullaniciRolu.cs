using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class KullaniciRolu : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KullaniciRoluAYRINTI> bilesenler = KullaniciRoluAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KullaniciRoluAYRINTI> bilesenler = KullaniciRoluAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_kullaniciKimlik, "Kullanıcı", dilKimlik);
            uyariVerInt32(i_rolKimlik, "Rol", dilKimlik);
            uyariVerBool(e_gecerlimi, "Geçerli mi?", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }
        public static KullaniciRolu olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciRolu sonuc = new KullaniciRolu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciRoluCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        #region bu_sinifin_bagli_oldugu_sinif
        public Kullanici _KullaniciBilgisi()
        {
            return Kullanici.olustur(i_kullaniciKimlik);
        }
        #endregion bu_sinifin_bagli_oldugu_sinif
        public KullaniciRoluAYRINTI _ayrintisi()
        {
            KullaniciRoluAYRINTI sonuc = KullaniciRoluAYRINTI.olustur(kullaniciRoluKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.KullaniciRoluCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }

        public void kaydet(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            veriTabani.KullaniciRoluCizelgesi.kaydet(this, vari, yedeklensinmi);
        }

        protected KullaniciRolu cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KullaniciRoluCizelgesi.tekliCek(kullaniciRoluKimlik, vari); }
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.KullaniciRoluCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KullaniciRolu";
        }
        public override string _turkceAdi()
        {
            return "Kullanıcı Rolü";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciRoluKimlik";
        }
        public override long _birincilAnahtar()
        {
            return kullaniciRoluKimlik;
        }
        #endregion
    }
}
