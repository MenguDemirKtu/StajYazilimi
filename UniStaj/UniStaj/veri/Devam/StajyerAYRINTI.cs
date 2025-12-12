using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajyerAYRINTI : Bilesen
    {

        public StajyerAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajBirimiKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(ogrenciNo);
        }



        public async static Task<StajyerAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajyerAYRINTI sonuc = new StajyerAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajyerAYRINTIs.FirstOrDefaultAsync(p => p.stajyerkimlik == kimlik && p.varmi == true);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
        }

        public static async Task<List<StajyerAYRINTI>> ara(params Expression<Func<StajyerAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajyerAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajyerAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajyerAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajyerAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajyerAYRINTI";
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

