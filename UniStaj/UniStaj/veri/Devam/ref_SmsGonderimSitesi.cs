using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class ref_SmsGonderimSitesi : Bilesen
    {

        public ref_SmsGonderimSitesi()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(SmsGonderimSitesiAdi, "", dilKimlik);
        }




        [NotMapped]
        public enumref_SmsGonderimSitesi _Smsgonderimsitesi
        {
            get
            {
                return (enumref_SmsGonderimSitesi)this.SmsGonderimSitesiKimlik;
            }
            set
            {
                SmsGonderimSitesiKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(SmsGonderimSitesiAdi);
        }



        public async static Task<ref_SmsGonderimSitesi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_SmsGonderimSitesi sonuc = new ref_SmsGonderimSitesi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_SmsGonderimSitesis.FirstOrDefaultAsync(p => p.SmsGonderimSitesiKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_SmsGonderimSitesi>> ara(params Expression<Func<ref_SmsGonderimSitesi, bool>>[] kosullar)
        {
            return await veriTabani.ref_SmsGonderimSitesiCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_SmsGonderimSitesi>> ara(veri.Varlik vari, params Expression<Func<ref_SmsGonderimSitesi, bool>>[] kosullar)
        {
            return await veriTabani.ref_SmsGonderimSitesiCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_SmsGonderimSitesi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "SmsGonderimSitesiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.SmsGonderimSitesiKimlik;
        }


        #endregion


    }
}

