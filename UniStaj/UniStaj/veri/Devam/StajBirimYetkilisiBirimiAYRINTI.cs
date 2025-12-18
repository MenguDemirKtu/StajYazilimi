using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class StajBirimYetkilisiBirimiAYRINTI : Bilesen
    {

        public StajBirimYetkilisiBirimiAYRINTI()
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
            return bossaDoldur(i_stajBirimYetkilisiKimlik);
        }



        public async static Task<StajBirimYetkilisiBirimiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajBirimYetkilisiBirimiAYRINTI sonuc = new StajBirimYetkilisiBirimiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajBirimYetkilisiBirimiAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimYetkilisiBirimikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajBirimYetkilisiBirimiAYRINTI>> ara(params Expression<Func<StajBirimYetkilisiBirimiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimYetkilisiBirimiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajBirimYetkilisiBirimiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimYetkilisiBirimiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimYetkilisiBirimiAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajBirimYetkilisiBirimiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajBirimYetkilisiBirimi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajBirimYetkilisiBirimikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajBirimYetkilisiBirimikimlik;
        }


        #endregion


    }
}

