using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajTuruAYRINTI : Bilesen
    {

        public StajTuruAYRINTI()
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
            return bossaDoldur(stajTuruAdi);
        }



        public async static Task<StajTuruAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajTuruAYRINTI sonuc = new StajTuruAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajTuruAYRINTIs.FirstOrDefaultAsync(p => p.stajTurukimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajTuruAYRINTI>> ara(params Expression<Func<StajTuruAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajTuruAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajTuruAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajTuruAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajTuruAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajTuruAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajTuru";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajTurukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajTurukimlik;
        }


        #endregion


    }
}

