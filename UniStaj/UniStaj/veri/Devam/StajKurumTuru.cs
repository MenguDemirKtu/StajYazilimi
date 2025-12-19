using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajKurumTuru : Bilesen
    {

        public StajKurumTuru()
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
            return bossaDoldur(stajKurumTurAdi);
        }



        public async static Task<StajKurumTuru?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajKurumTuru sonuc = new StajKurumTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajKurumTurus.FirstOrDefaultAsync(p => p.stajKurumTurukimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.StajKurumTuruCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.StajKurumTuruCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_argeFirmasiMi = false;
            this.varmi = true;
        }

        public static async Task<List<StajKurumTuru>> ara(params Expression<Func<StajKurumTuru, bool>>[] kosullar)
        {
            return await veriTabani.StajKurumTuruCizelgesi.ara(kosullar);
        }
        public static async Task<List<StajKurumTuru>> ara(veri.Varlik vari, params Expression<Func<StajKurumTuru, bool>>[] kosullar)
        {
            return await veriTabani.StajKurumTuruCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajKurumTuru";
        }


        public override string _turkceAdi()
        {
            return "Staj Kurum Türü";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajKurumTurukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajKurumTurukimlik;
        }


        #endregion


    }
}

