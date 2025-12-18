using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class StajBirimiTurleri : Bilesen
    {

        public StajBirimiTurleri()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajBirimiKimlik, "Staj Birimi", dilKimlik);
            uyariVerInt32(i_stajTuruKimlik, "Staj Türü", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_stajBirimiKimlik);
        }



        public async static Task<StajBirimiTurleri?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajBirimiTurleri sonuc = new StajBirimiTurleri();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajBirimiTurleris.FirstOrDefaultAsync(p => p.stajBirimiTurlerikimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.StajBirimiTurleriCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.StajBirimiTurleriCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.varmi = true;
        }

        public static async Task<List<StajBirimiTurleri>> ara(params Expression<Func<StajBirimiTurleri, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimiTurleriCizelgesi.ara(kosullar);
        }
        public static async Task<List<StajBirimiTurleri>> ara(veri.Varlik vari, params Expression<Func<StajBirimiTurleri, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimiTurleriCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajBirimiTurleri";
        }


        public override string _turkceAdi()
        {
            return "Staj Birimi Türleri";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajBirimiTurlerikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajBirimiTurlerikimlik;
        }


        #endregion


    }
}

