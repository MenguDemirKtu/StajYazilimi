using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class SehirAYRINTI : Bilesen
    {

        public SehirAYRINTI()
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
            return bossaDoldur(sehirAdi);
        }



        public async static Task<SehirAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SehirAYRINTI sonuc = new SehirAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SehirAYRINTIs.FirstOrDefaultAsync(p => p.sehirKimlik == kimlik && p.varmi == true);
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

        public static async Task<List<SehirAYRINTI>> ara(params Expression<Func<SehirAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SehirAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<SehirAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SehirAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SehirAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SehirAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Sehir";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sehirKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sehirKimlik;
        }


        #endregion


    }
}

