using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajBasvurusu : Bilesen
    {

        public StajBasvurusu()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

            if (string.IsNullOrEmpty(this.kodu))
                this.kodu = Guid.NewGuid().ToString();
        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajyerKimlik, "Stajyer", dilKimlik);
            uyariVerInt32(i_stajTuruKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_stajyerKimlik);
        }



        public async static Task<StajBasvurusu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajBasvurusu sonuc = new StajBasvurusu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajBasvurusus.FirstOrDefaultAsync(p => p.stajBasvurusukimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.StajBasvurusuCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.StajBasvurusuCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<StajBasvurusu>> ara(params Expression<Func<StajBasvurusu, bool>>[] kosullar)
        {
            return await veriTabani.StajBasvurusuCizelgesi.ara(kosullar);
        }
        public static async Task<List<StajBasvurusu>> ara(veri.Varlik vari, params Expression<Func<StajBasvurusu, bool>>[] kosullar)
        {
            return await veriTabani.StajBasvurusuCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajBasvurusu";
        }


        public override string _turkceAdi()
        {
            return "Staj Başvurusu";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajBasvurusukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajBasvurusukimlik;
        }


        #endregion


    }
}

