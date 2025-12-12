using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class WebSayfasiAYRINTI : Bilesen
    {

        public WebSayfasiAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_modulKimlik, ".", dilKimlik);
            uyariVerInt32(i_sayfaTuruKimlik, ".", dilKimlik);
        }







        public override string _tanimi()
        {
            return bossaDoldur(hamAdresi);
        }



        public async static Task<WebSayfasiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                WebSayfasiAYRINTI sonuc = new WebSayfasiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.WebSayfasiAYRINTIs.FirstOrDefaultAsync(p => p.webSayfasiKimlik == kimlik && p.varmi == true);
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

        public static async Task<List<WebSayfasiAYRINTI>> ara(params Expression<Func<WebSayfasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.WebSayfasiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<WebSayfasiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<WebSayfasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.WebSayfasiAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "WebSayfasiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "websayfasiA";
        }
        public override string _birincilAnahtarAdi()
        {
            return "webSayfasiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.webSayfasiKimlik;
        }


        #endregion


    }
}

