using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class IlceAYRINTI : Bilesen
    {

        public IlceAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_sehirKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(ilceAdi);
        }



        public async static Task<IlceAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                IlceAYRINTI sonuc = new IlceAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.IlceAYRINTIs.FirstOrDefaultAsync(p => p.ilcekimlik == kimlik && p.varmi == true);
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

        public static async Task<List<IlceAYRINTI>> ara(params Expression<Func<IlceAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.IlceAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<IlceAYRINTI>> ara(veri.Varlik vari, params Expression<Func<IlceAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.IlceAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "IlceAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "IlceA";
        }
        public override string _birincilAnahtarAdi()
        {
            return "ilcekimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.ilcekimlik;
        }


        #endregion


    }
}

