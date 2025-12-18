using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class StajBasvurusuAYRINTI : Bilesen
    {

        public StajBasvurusuAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

            if (string.IsNullOrEmpty(this.kodu))
                this.kodu = Guid.NewGuid().ToString();
        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajyerKimlik, ".", dilKimlik);
            uyariVerInt32(i_stajBirimiKimlik, ".", dilKimlik);
            uyariVerInt32(i_stajTuruKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_stajyerKimlik);
        }



        public async static Task<StajBasvurusuAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajBasvurusuAYRINTI sonuc = new StajBasvurusuAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajBasvurusuAYRINTIs.FirstOrDefaultAsync(p => p.stajBasvurusukimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajBasvurusuAYRINTI>> ara(params Expression<Func<StajBasvurusuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBasvurusuAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajBasvurusuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBasvurusuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBasvurusuAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajBasvurusuAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajBasvurusu";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajBasvurusukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajBasvurusukimlik;
        }


        #endregion


    }
}

