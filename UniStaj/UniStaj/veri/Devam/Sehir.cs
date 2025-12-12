using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class Sehir : Bilesen
    {

        public Sehir()
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
            return bossaDoldur(sehirAdi);
        }



        public async static Task<Sehir?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Sehir sonuc = new Sehir();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Sehirs.FirstOrDefaultAsync(p => p.sehirKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.SehirCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SehirCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<Sehir>> ara(params Expression<Func<Sehir, bool>>[] kosullar)
        {
            return await veriTabani.SehirCizelgesi.ara(kosullar);
        }
        public static async Task<List<Sehir>> ara(veri.Varlik vari, params Expression<Func<Sehir, bool>>[] kosullar)
        {
            return await veriTabani.SehirCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Sehir";
        }


        public override string _turkceAdi()
        {
            return "Şehir";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sehirKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sehirKimlik;
        }


        #endregion


    }
}

