using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class SimgeAYRINTI : Bilesen
    {

        public SimgeAYRINTI()
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
            return bossaDoldur(baslik);
        }



        public async static Task<SimgeAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SimgeAYRINTI sonuc = new SimgeAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SimgeAYRINTIs.FirstOrDefaultAsync(p => p.simgekimlik == kimlik && p.varmi == true);
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

        public static async Task<List<SimgeAYRINTI>> ara(params Expression<Func<SimgeAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SimgeAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<SimgeAYRINTI>> ara(veri.Varlik vari, params Expression<Func<SimgeAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.SimgeAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SimgeAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SimgeAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "simgekimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.simgekimlik;
        }


        #endregion


    }
}

