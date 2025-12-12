using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class UlkeAYRINTI : Bilesen
    {

        public UlkeAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return bossaDoldur(ulkeAdi);
        }



        public async static Task<UlkeAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                UlkeAYRINTI sonuc = new UlkeAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.UlkeAYRINTIs.FirstOrDefaultAsync(p => p.ulkeKimlik == kimlik && p.varmi == true);
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

        public static async Task<List<UlkeAYRINTI>> ara(params Expression<Func<UlkeAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.UlkeAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<UlkeAYRINTI>> ara(veri.Varlik vari, params Expression<Func<UlkeAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.UlkeAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "UlkeAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Ülke";
        }
        public override string _birincilAnahtarAdi()
        {
            return "ulkeKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.ulkeKimlik;
        }


        #endregion


    }
}

