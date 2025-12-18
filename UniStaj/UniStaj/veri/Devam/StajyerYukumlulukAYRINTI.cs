using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class StajyerYukumlulukAYRINTI : Bilesen
    {

        public StajyerYukumlulukAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajyerKimlik, ".", dilKimlik);
            uyariVerInt32(i_stajTuruKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_stajyerKimlik);
        }



        public async static Task<StajyerYukumlulukAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajyerYukumlulukAYRINTI sonuc = new StajyerYukumlulukAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajyerYukumlulukAYRINTIs.FirstOrDefaultAsync(p => p.stajyerYukumlulukkimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajyerYukumlulukAYRINTI>> ara(params Expression<Func<StajyerYukumlulukAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajyerYukumlulukAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajyerYukumlulukAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajyerYukumlulukAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajyerYukumlulukAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajyerYukumlulukAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajyerYukumluluk";
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

