using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajBirimYetkilisiAYRINTI : Bilesen
    {

        public StajBirimYetkilisiAYRINTI()
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
            return bossaDoldur(tcKimlikNo);
        }



        public async static Task<StajBirimYetkilisiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajBirimYetkilisiAYRINTI sonuc = new StajBirimYetkilisiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajBirimYetkilisiAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimYetkilisikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajBirimYetkilisiAYRINTI>> ara(params Expression<Func<StajBirimYetkilisiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimYetkilisiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajBirimYetkilisiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimYetkilisiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimYetkilisiAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajBirimYetkilisiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajBirimYetkilisi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajBirimYetkilisikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajBirimYetkilisikimlik;
        }


        #endregion


    }
}

