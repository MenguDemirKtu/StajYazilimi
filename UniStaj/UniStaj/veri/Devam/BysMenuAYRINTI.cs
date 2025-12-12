using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class BysMenuAYRINTI : Bilesen
    {

        public BysMenuAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_ustMenuKimlik, ".", dilKimlik);
            uyariVerInt32(i_webSayfasiKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(bysMenuAdi);
        }



        public async static Task<BysMenuAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                BysMenuAYRINTI sonuc = new BysMenuAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.BysMenuAYRINTIs.FirstOrDefaultAsync(p => p.bysMenuKimlik == kimlik && p.varmi == true);
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

        public static async Task<List<BysMenuAYRINTI>> ara(params Expression<Func<BysMenuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.BysMenuAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<BysMenuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<BysMenuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.BysMenuAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "BysMenuAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "BysMenuAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "bysMenuKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.bysMenuKimlik;
        }


        #endregion


    }
}

