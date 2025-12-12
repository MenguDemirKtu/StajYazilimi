using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class Ulke : Bilesen
    {

        public Ulke()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return bossaDoldur(ulkeAdi);
        }



        public async static Task<Ulke?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Ulke sonuc = new Ulke();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Ulkes.FirstOrDefaultAsync(p => p.ulkeKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.UlkeCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.UlkeCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<Ulke>> ara(params Expression<Func<Ulke, bool>>[] kosullar)
        {
            return await veriTabani.UlkeCizelgesi.ara(kosullar);
        }
        public static async Task<List<Ulke>> ara(veri.Varlik vari, params Expression<Func<Ulke, bool>>[] kosullar)
        {
            return await veriTabani.UlkeCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Ulke";
        }


        public override string _turkceAdi()
        {
            return "Ülke";
        }
        public override string _birincilAnahtarAdi()
        {
            return "ulkeKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.ulkeKimlik;
        }


        #endregion


    }
}

