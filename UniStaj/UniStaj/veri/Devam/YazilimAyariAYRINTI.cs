using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class YazilimAyariAYRINTI : Bilesen
    {

        public YazilimAyariAYRINTI()
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
        public enumref_SmsGonderimSitesi _Smsgonderimsitesi
        {
            get
            {
                return (enumref_SmsGonderimSitesi)this.i_smsGonderimSitesiKimlik;
            }
            set
            {
                i_smsGonderimSitesiKimlik = (int)value;
            }
        }
        [NotMapped]
        public enumref_OturumAcmaTuru _Oturumacmaturu
        {
            get
            {
                return (enumref_OturumAcmaTuru)(this.i_oturumAcmaTuruKimlik ?? 1);
            }
            set
            {
                i_oturumAcmaTuruKimlik = (int)value;
            }
        }



        public override string _tanimi()
        {
            return bossaDoldur(ayarAdi);
        }



        public async static Task<YazilimAyariAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                YazilimAyariAYRINTI sonuc = new YazilimAyariAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.YazilimAyariAYRINTIs.FirstOrDefaultAsync(p => p.yazilimAyariKimlik == kimlik && p.varmi == true);
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

        public static async Task<List<YazilimAyariAYRINTI>> ara(params Expression<Func<YazilimAyariAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.YazilimAyariAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<YazilimAyariAYRINTI>> ara(veri.Varlik vari, params Expression<Func<YazilimAyariAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.YazilimAyariAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "YazilimAyariAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "YazilimAyariAYRIN";
        }
        public override string _birincilAnahtarAdi()
        {
            return "yazilimAyariKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.yazilimAyariKimlik;
        }


        #endregion


    }
}

