using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class ref_StajTipi : Bilesen
    {

        public ref_StajTipi()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(StajTipiAdi, "", dilKimlik);
        }




        [NotMapped]
        public enumref_StajTipi _Stajtipi
        {
            get
            {
                return (enumref_StajTipi)this.StajTipiKimlik;
            }
            set
            {
                StajTipiKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(StajTipiAdi);
        }



        public async static Task<ref_StajTipi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_StajTipi sonuc = new ref_StajTipi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_StajTipis.FirstOrDefaultAsync(p => p.StajTipiKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_StajTipi>> ara(params Expression<Func<ref_StajTipi, bool>>[] kosullar)
        {
            return await veriTabani.ref_StajTipiCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_StajTipi>> ara(veri.Varlik vari, params Expression<Func<ref_StajTipi, bool>>[] kosullar)
        {
            return await veriTabani.ref_StajTipiCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_StajTipi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "StajTipiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.StajTipiKimlik;
        }


        #endregion


    }
}

