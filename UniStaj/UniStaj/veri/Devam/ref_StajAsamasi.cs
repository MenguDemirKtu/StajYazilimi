using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class ref_StajAsamasi : Bilesen
    {

        public ref_StajAsamasi()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(StajAsamasiAdi, "", dilKimlik);
        }




        [NotMapped]
        public enumref_StajAsamasi _Stajasamasi
        {
            get
            {
                return (enumref_StajAsamasi)this.StajAsamasiKimlik;
            }
            set
            {
                StajAsamasiKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(StajAsamasiAdi);
        }



        public async static Task<ref_StajAsamasi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_StajAsamasi sonuc = new ref_StajAsamasi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_StajAsamasis.FirstOrDefaultAsync(p => p.StajAsamasiKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_StajAsamasi>> ara(params Expression<Func<ref_StajAsamasi, bool>>[] kosullar)
        {
            return await veriTabani.ref_StajAsamasiCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_StajAsamasi>> ara(veri.Varlik vari, params Expression<Func<ref_StajAsamasi, bool>>[] kosullar)
        {
            return await veriTabani.ref_StajAsamasiCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_StajAsamasi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "StajAsamasiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.StajAsamasiKimlik;
        }


        #endregion


    }
}

