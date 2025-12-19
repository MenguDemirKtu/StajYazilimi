using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class KullaniciRolu : Bilesen
    {

        public KullaniciRolu()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_kullaniciKimlik, "", dilKimlik);
            uyariVerInt32(i_rolKimlik, "", dilKimlik);
            uyariVerBool(e_gecerlimi, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_kullaniciKimlik);
        }



        public async static Task<KullaniciRolu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciRolu sonuc = new KullaniciRolu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.KullaniciRolus.FirstOrDefaultAsync(p => p.kullaniciRoluKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.KullaniciRoluCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.KullaniciRoluCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
        }

        #region bu_sinifin_bagli_oldugu_sinif
        public Kullanici _KullaniciBilgisi()
        {
            return Kullanici.olustur(this.i_kullaniciKimlik);
        }

        #endregion bu_sinifin_bagli_oldugu_sinif

        public static async Task<List<KullaniciRolu>> ara(params Expression<Func<KullaniciRolu, bool>>[] kosullar)
        {
            return await veriTabani.KullaniciRoluCizelgesi.ara(kosullar);
        }
        public static async Task<List<KullaniciRolu>> ara(veri.Varlik vari, params Expression<Func<KullaniciRolu, bool>>[] kosullar)
        {
            return await veriTabani.KullaniciRoluCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "KullaniciRolu";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciRoluKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.kullaniciRoluKimlik;
        }


        #endregion


    }
}

