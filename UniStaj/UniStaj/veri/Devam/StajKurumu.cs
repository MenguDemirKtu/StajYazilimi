using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class StajKurumu : Bilesen
    {

        public StajKurumu()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajkurumturukimlik, "stajKurumTuru", dilKimlik);

        }


        public override string _tanimi()
        {
            return bossaDoldur(stajKurumAdi);
        }



        public async static Task<StajKurumu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajKurumu sonuc = new StajKurumu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajKurumus.FirstOrDefaultAsync(p => p.stajKurumukimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.StajKurumuCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.StajKurumuCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
            if (stajKurumAdi == null)
            {
                throw new Exception("Kurum adý boþ býrakýlamaz.");
            }

            if (vergiNo == null)
            {
                throw new Exception("Vergi numarasý boþ býrakýlamaz.");
            }
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_karaListedeMi = true;
            this.varmi = true;
        }

        public static async Task<List<StajKurumu>> ara(params Expression<Func<StajKurumu, bool>>[] kosullar)
        {
            return await veriTabani.StajKurumuCizelgesi.ara(kosullar);
        }
        public static async Task<List<StajKurumu>> ara(veri.Varlik vari, params Expression<Func<StajKurumu, bool>>[] kosullar)
        {
            return await veriTabani.StajKurumuCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajKurumu";
        }


        public override string _turkceAdi()
        {
            return "Staj Kurumu";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajKurumukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajKurumukimlik;
        }


        #endregion


    }
}

