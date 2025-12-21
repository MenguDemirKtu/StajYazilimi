using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class StajBirimAsamasiAYRINTI : Bilesen
    {

        public StajBirimAsamasiAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_stajBirimiKimlik, ".", dilKimlik);
            uyariVerInt32(i_stajAsamasiKimlik, ".", dilKimlik);
            uyariVerString(StajAsamasiAdi, ".", dilKimlik);
        }




        [NotMapped]
        public enumref_StajAsamasi _Stajasamasi
        {
            get
            {
                return (enumref_StajAsamasi)this.i_stajAsamasiKimlik;
            }
            set
            {
                i_stajAsamasiKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_stajBirimiKimlik);
        }



        public async static Task<StajBirimAsamasiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                StajBirimAsamasiAYRINTI sonuc = new StajBirimAsamasiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.StajBirimAsamasiAYRINTIs.FirstOrDefaultAsync(p => p.stajBirimAsamasikimlik == kimlik && p.varmi == true);
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

        public static async Task<List<StajBirimAsamasiAYRINTI>> ara(params Expression<Func<StajBirimAsamasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimAsamasiAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<StajBirimAsamasiAYRINTI>> ara(veri.Varlik vari, params Expression<Func<StajBirimAsamasiAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.StajBirimAsamasiAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "StajBirimAsamasiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "StajBirimAsamasi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "stajBirimAsamasikimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.stajBirimAsamasikimlik;
        }


        #endregion


    }
}

