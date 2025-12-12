using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class ref_OturumAcmaTuru : Bilesen
    {

        public ref_OturumAcmaTuru()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }




        [NotMapped]
        public enumref_OturumAcmaTuru _Oturumacmaturu
        {
            get
            {
                return (enumref_OturumAcmaTuru)this.oturumAcmaTuruKimlik;
            }
            set
            {
                oturumAcmaTuruKimlik = (int)value;
            }
        }
        public override string _tanimi()
        {
            return bossaDoldur(oturumAcmaTuru);
        }



        public async static Task<ref_OturumAcmaTuru?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_OturumAcmaTuru sonuc = new ref_OturumAcmaTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_OturumAcmaTurus.FirstOrDefaultAsync(p => p.oturumAcmaTuruKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_OturumAcmaTuru>> ara(params Expression<Func<ref_OturumAcmaTuru, bool>>[] kosullar)
        {
            return await veriTabani.ref_OturumAcmaTuruCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_OturumAcmaTuru>> ara(veri.Varlik vari, params Expression<Func<ref_OturumAcmaTuru, bool>>[] kosullar)
        {
            return await veriTabani.ref_OturumAcmaTuruCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_OturumAcmaTuru";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "oturumAcmaTuruKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.oturumAcmaTuruKimlik;
        }


        #endregion


    }
}

