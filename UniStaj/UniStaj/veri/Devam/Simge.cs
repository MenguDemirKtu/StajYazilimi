using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class Simge : Bilesen
    {

        public Simge()
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
            return bossaDoldur(baslik);
        }



        public async static Task<Simge?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Simge sonuc = new Simge();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Simges.FirstOrDefaultAsync(p => p.simgekimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.SimgeCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SimgeCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
        }

        public static async Task<List<Simge>> ara(params Expression<Func<Simge, bool>>[] kosullar)
        {
            return await veriTabani.SimgeCizelgesi.ara(kosullar);
        }
        public static async Task<List<Simge>> ara(veri.Varlik vari, params Expression<Func<Simge, bool>>[] kosullar)
        {
            return await veriTabani.SimgeCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Simge";
        }


        public override string _turkceAdi()
        {
            return "Simge";
        }
        public override string _birincilAnahtarAdi()
        {
            return "simgekimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.simgekimlik;
        }


        #endregion


    }
}

