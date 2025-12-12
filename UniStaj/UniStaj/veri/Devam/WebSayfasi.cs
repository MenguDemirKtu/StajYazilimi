using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class WebSayfasi : Bilesen
    {
        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            if (string.IsNullOrEmpty(dokumAciklamasi))
                uyariVer("Sayfa üst menüsünde yer alacak olan açıklama girilmelidir", dilKimlik);
            if (string.IsNullOrEmpty(sayfaBasligi))
                uyariVer("Sayfa başlığı girilmelidir", dilKimlik);
        }
        public override string _tanimi()
        {
            if (hamAdresi == null)
                return "";
            return hamAdresi.ToString();
        }
        public static WebSayfasi olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                WebSayfasi sonuc = new WebSayfasi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.WebSayfasiCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            e_izinSayfasindaGorunsunmu = true;
            i_fotoKimlik = 0;
            varmi = true;
            e_varsayilanEklemeAcikmi = false;
            e_varsayilanGorunmeAcikmi = false;
            e_varsayilanGuncellemeAcikmi = false;
            e_varsayilanSilmeAcikmi = false;
            aciklama = "...";
        }
        protected WebSayfasi cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.WebSayfasiCizelgesi.tekliCek(webSayfasiKimlik, vari); }
        }
        public static List<WebSayfasi> ara(params Predicate<WebSayfasi>[] kosullar)
        {
            return veriTabani.WebSayfasiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.WebSayfasiCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "WebSayfasi";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "webSayfasiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return webSayfasiKimlik;
        }
        #endregion

        public void bicimlendir()
        {
            if (string.IsNullOrEmpty(hamAdresi))
                hamAdresi = "";
            hamAdresi = hamAdresi.ToLower().Trim();
            hamAdresi = hamAdresi.Replace("ı", "i");
        }
        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            bicimlendir();
            varmi = true;
            await veriTabani.WebSayfasiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.WebSayfasiCizelgesi.silKos(this, vari, yedeklensinmi);
        }
        public async static Task<WebSayfasi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                WebSayfasi sonuc = new WebSayfasi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.WebSayfasis.FirstOrDefaultAsync(p => p.webSayfasiKimlik == kimlik && p.varmi == true);
            }
        }
    }
}
