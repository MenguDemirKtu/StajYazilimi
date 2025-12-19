using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajyerYukumluluk : Bilesen
    {

        public StajyerYukumluluk()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajyerKimlik, "Stajyer", dilKimlik);
            uyariVerInt32(i_stajTuruKimlik, "Staj Türü", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_stajyerKimlik);
        }



        public async static Task<StajyerYukumluluk?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajyerYukumluluk sonuc = new StajyerYukumluluk();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajyerYukumluluks.FirstOrDefaultAsync(p => p.stajyerYukumlulukkimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.StajyerYukumlulukCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.StajyerYukumlulukCizelgesi.silKos(this, vari, yedeklensinmi);
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

        public static async Task<List<StajyerYukumluluk>> ara(params Expression<Func<StajyerYukumluluk, bool>>[] kosullar)
        {
            return await veriTabani.StajyerYukumlulukCizelgesi.ara(kosullar);
        }
        public static async Task<List<StajyerYukumluluk>> ara(veri.Varlik vari, params Expression<Func<StajyerYukumluluk, bool>>[] kosullar)
        {
            return await veriTabani.StajyerYukumlulukCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajyerYukumluluk";
        }


        public override string _turkceAdi()
        {
            return "Stajyer Yükümlülük";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajyerYukumlulukkimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajyerYukumlulukkimlik;
        }


        #endregion


    }
}