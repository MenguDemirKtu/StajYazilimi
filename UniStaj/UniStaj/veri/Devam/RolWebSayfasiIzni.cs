using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class RolWebSayfasiIzni : Bilesen
    {
        public void bicimlendir(veri.Varlik vari)
        {
        }
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<RolWebSayfasiIzniAYRINTI> bilesenler = RolWebSayfasiIzniAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<RolWebSayfasiIzniAYRINTI> bilesenler = RolWebSayfasiIzniAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public void bicimlendir()
        {
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_rolKimlik, "", dilKimlik);
            uyariVerInt32(i_webSayfasiKimlik, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return i_rolKimlik.ToString();
        }
        public static RolWebSayfasiIzni olustur(object deger)
        {
            using (veri.Varlik vari = new Varlik())
            {
                return olustur(vari, deger);
            }
        }
        public static RolWebSayfasiIzni olustur(Varlik vari, object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                RolWebSayfasiIzni sonuc = new RolWebSayfasiIzni();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.RolWebSayfasiIzniCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        public RolWebSayfasiIzniAYRINTI _ayrintisi()
        {
            RolWebSayfasiIzniAYRINTI sonuc = RolWebSayfasiIzniAYRINTI.olustur(rolWebSayfasiIzniKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kaydet(vari, yedeklensinmi);
            }
        }
        public void kaydet(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir();
            veriTabani.RolWebSayfasiIzniCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        public static List<RolWebSayfasiIzni> ara(params Predicate<RolWebSayfasiIzni>[] kosullar)
        {
            return veriTabani.RolWebSayfasiIzniCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { sil(vari); }
        ;
        }
        public void sil(veri.Varlik vari)
        {
            veriTabani.RolWebSayfasiIzniCizelgesi.sil(this, vari);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "RolWebSayfasiIzni";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "rolWebSayfasiIzniKimlik";
        }
        public override long _birincilAnahtar()
        {
            return rolWebSayfasiIzniKimlik;
        }
        #endregion
        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.RolWebSayfasiIzniCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.RolWebSayfasiIzniCizelgesi.silKos(this, vari, yedeklensinmi);
        }
        public async static Task<RolWebSayfasiIzni?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                RolWebSayfasiIzni sonuc = new RolWebSayfasiIzni();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.RolWebSayfasiIznis.FirstOrDefaultAsync(p => p.rolWebSayfasiIzniKimlik == kimlik && p.varmi == true);
            }
        }
    }
}
