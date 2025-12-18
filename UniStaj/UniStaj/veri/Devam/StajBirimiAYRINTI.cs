using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class StajBirimiAYRINTI : Bilesen
    {

        public StajBirimiAYRINTI()
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
            return bossaDoldur(stajBirimAdi);
        }



        public async static Task<StajBirimiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajBirimiAYRINTI sonuc = new StajBirimiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajBirimiAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajBirimiAYRINTI>> ara(params Expression<Func<StajBirimiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajBirimiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimiAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajBirimiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajBirimi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajBirimikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajBirimikimlik;
        }


        #endregion


    }
}

