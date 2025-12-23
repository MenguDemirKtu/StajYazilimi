using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajDonemi : Bilesen
    {

        public StajDonemi()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return bossaDoldur(stajDonemAdi);
        }



        public async static Task<StajDonemi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajDonemi sonuc = new StajDonemi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajDonemis.FirstOrDefaultAsync(p => p.stajDonemikimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.StajDonemiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.StajDonemiCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<StajDonemi>> ara(params Expression<Func<StajDonemi, bool>>[] kosullar)
        {
            return await veriTabani.StajDonemiCizelgesi.ara(kosullar);
        }
        public static async Task<List<StajDonemi>> ara(veri.Varlik vari, params Expression<Func<StajDonemi, bool>>[] kosullar)
        {
            return await veriTabani.StajDonemiCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajDonemi";
        }


        public override string _turkceAdi()
        {
            return "Staj Dönemi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajDonemikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajDonemikimlik;
        }


        #endregion


    }
}

