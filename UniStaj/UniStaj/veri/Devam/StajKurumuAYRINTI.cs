using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajKurumuAYRINTI : Bilesen
    {

        public StajKurumuAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajkurumturukimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(stajKurumAdi);
        }



        public async static Task<StajKurumuAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajKurumuAYRINTI sonuc = new StajKurumuAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajKurumuAYRINTIs.FirstOrDefaultAsync(p => p.stajKurumukimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajKurumuAYRINTI>> ara(params Expression<Func<StajKurumuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajKurumuAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajKurumuAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajKurumuAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajKurumuAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajKurumuAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajKurumu";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajKurumukimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajKurumukimlik;
        }


        #endregion


    }
}

