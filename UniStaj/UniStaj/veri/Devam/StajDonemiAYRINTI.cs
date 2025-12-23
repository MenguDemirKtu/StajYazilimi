using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajDonemiAYRINTI : Bilesen
    {

        public StajDonemiAYRINTI()
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
            return bossaDoldur(stajDonemAdi);
        }



        public async static Task<StajDonemiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajDonemiAYRINTI sonuc = new StajDonemiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajDonemiAYRINTIs.FirstOrDefaultAsync(p => p.stajDonemikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajDonemiAYRINTI>> ara(params Expression<Func<StajDonemiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajDonemiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajDonemiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajDonemiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajDonemiAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajDonemiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajDonemi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajDonemikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajDonemikimlik;
        }


        #endregion


    }
}

