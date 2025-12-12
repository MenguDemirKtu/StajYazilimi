using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class YazilimAyari : Bilesen
    {

        public YazilimAyari()
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
                return (enumref_OturumAcmaTuru)this.i_oturumAcmaTuruKimlik;
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



        public async static Task<YazilimAyari?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                YazilimAyari sonuc = new YazilimAyari();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.YazilimAyaris.FirstOrDefaultAsync(p => p.yazilimAyariKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.YazilimAyariCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.YazilimAyariCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_gecerlimi = true;
            this.varmi = true;
            this.e_menuGuncellenecekmi = false;
            this.gunlukOturumSiniri = 100;
        }

        public static async Task<List<YazilimAyari>> ara(params Expression<Func<YazilimAyari, bool>>[] kosullar)
        {
            return await veriTabani.YazilimAyariCizelgesi.ara(kosullar);
        }
        public static async Task<List<YazilimAyari>> ara(veri.Varlik vari, params Expression<Func<YazilimAyari, bool>>[] kosullar)
        {
            return await veriTabani.YazilimAyariCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "YazilimAyari";
        }


        public override string _turkceAdi()
        {
            return "Yazılım Ayarı";
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

