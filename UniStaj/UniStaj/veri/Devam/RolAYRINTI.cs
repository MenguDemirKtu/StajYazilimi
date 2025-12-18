using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class RolAYRINTI : Bilesen
    {

        public RolAYRINTI()
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
            uyariVerBool(e_gecerlimi, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(rolAdi);
        }



        public async static Task<RolAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                RolAYRINTI sonuc = new RolAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.RolAYRINTIs.FirstOrDefaultAsync(p => p.rolKimlik == kimlik && p.varmi == true);
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

        public static async Task<List<RolAYRINTI>> ara(params Expression<Func<RolAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.RolAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<RolAYRINTI>> ara(veri.Varlik vari, params Expression<Func<RolAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.RolAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "RolAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "RolA";
        }
        public override string _birincilAnahtarAdi()
        {
            return "rolKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.rolKimlik;
        }


        #endregion


    }
}

