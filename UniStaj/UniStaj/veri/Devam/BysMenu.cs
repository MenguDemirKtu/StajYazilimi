using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class BysMenu : Bilesen
    {
        public BysMenu()
        {
            _varSayilan();
        }
        public void bicimlendir(veri.Varlik vari)
        {
            if (e_gosterilsimmi == null)
                e_gosterilsimmi = true;
            if (e_modulSayfasimi == true)
            {
                i_ustMenuKimlik = 0;
                i_webSayfasiKimlik = 0;
            }
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            //uyariVerInt32(i_ustMenuKimlik, "Üst Menü", dilKimlik);
            //uyariVerInt32(i_webSayfasiKimlik, "Web Sayfası", dilKimlik);
        }
        public override string _tanimi()
        {
            return bossaDoldur(bysMenuAdi);
        }
        public static BysMenu olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }
        public static BysMenu olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                BysMenu sonuc = new BysMenu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.BysMenuCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            varmi = true;
            sirasi = 100;
            e_modulSayfasimi = false;
            i_modulKimlik = 0;
            e_gosterilsimmi = true;
        }
        public void kaydet(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            veriTabani.BysMenuCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected BysMenu cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.BysMenuCizelgesi.tekliCek(bysMenuKimlik, vari);
            }
        }
        public static List<BysMenu> ara(params Predicate<BysMenu>[] kosullar)
        {
            return veriTabani.BysMenuCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                sil(vari);
            }
        }
        public void sil(veri.Varlik vari)
        {
            veriTabani.BysMenuCizelgesi.sil(this, vari);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "BysMenu";
        }
        public override string _turkceAdi()
        {
            return "BYS Menü";
        }
        public override string _birincilAnahtarAdi()
        {
            return "bysMenuKimlik";
        }
        public override long _birincilAnahtar()
        {
            return bysMenuKimlik;
        }
        #endregion
        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.BysMenuCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.BysMenuCizelgesi.silKos(this, vari, yedeklensinmi);
        }
        public async static Task<BysMenu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                BysMenu sonuc = new BysMenu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.BysMenus.FirstOrDefaultAsync(p => p.bysMenuKimlik == kimlik && p.varmi == true);
            }
        }
    }
}
