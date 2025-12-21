using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajBirimAsamasi : Bilesen
    {

        public StajBirimAsamasi()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajBirimiKimlik, "Staj Birimi", dilKimlik);
            uyariVerInt32(i_stajAsamasiKimlik, "Staj Aşaması", dilKimlik);
        }




        [NotMapped]
        public enumref_StajAsamasi _Stajasamasi
        {
            get
            {
                return (enumref_StajAsamasi)this.i_stajAsamasiKimlik;
            }
            set
            {
                i_stajAsamasiKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_stajBirimiKimlik);
        }



        public async static Task<StajBirimAsamasi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajBirimAsamasi sonuc = new StajBirimAsamasi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajBirimAsamasis.FirstOrDefaultAsync(p => p.stajBirimAsamasikimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.StajBirimAsamasiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.StajBirimAsamasiCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<StajBirimAsamasi>> ara(params Expression<Func<StajBirimAsamasi, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimAsamasiCizelgesi.ara(kosullar);
        }
        public static async Task<List<StajBirimAsamasi>> ara(veri.Varlik vari, params Expression<Func<StajBirimAsamasi, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimAsamasiCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajBirimAsamasi";
        }


        public override string _turkceAdi()
        {
            return "Staj Birim Aşaması";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajBirimAsamasikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajBirimAsamasikimlik;
        }


        #endregion


    }
}

