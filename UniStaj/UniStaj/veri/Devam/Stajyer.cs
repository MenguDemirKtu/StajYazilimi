using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class Stajyer : Bilesen
    {

        public Stajyer()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajBirimiKimlik, "Staj Birimi", dilKimlik);
            uyariVerString(ogrenciNo, "Öðrenci Numarasý", dilKimlik);
            uyariVerString(tcKimlikNo, "TC Kimlik", dilKimlik);
            uyariVerString(stajyerAdi, "Öðrenci Adý", dilKimlik);
            uyariVerString(stajyerSoyadi, "Öðrenci Soyadý", dilKimlik);
            uyariVerInt32(sinifi, "Öðrenci Adý", dilKimlik);
            uyariVerString(telefon, "Telefon", dilKimlik);
            uyariVerString(ePosta, "E-posta", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(ogrenciNo);
        }



        public async static Task<Stajyer?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Stajyer sonuc = new Stajyer();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Stajyers.FirstOrDefaultAsync(p => p.stajyerkimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.StajyerCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.StajyerCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<Stajyer>> ara(params Expression<Func<Stajyer, bool>>[] kosullar)
        {
            return await veriTabani.StajyerCizelgesi.ara(kosullar);
        }
        public static async Task<List<Stajyer>> ara(veri.Varlik vari, params Expression<Func<Stajyer, bool>>[] kosullar)
        {
            return await veriTabani.StajyerCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Stajyer";
        }


        public override string _turkceAdi()
        {
            return "Stajyer";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajyerkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajyerkimlik;
        }


        #endregion


    }
}

