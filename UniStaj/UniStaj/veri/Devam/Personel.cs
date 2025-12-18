using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class Personel : Bilesen
    {

        public Personel()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }




        [NotMapped]
        public enumref_Cinsiyet _Cinsiyet
        {
            get
            {
                return (enumref_Cinsiyet)this.i_cinsiyetKimlik;
            }
            set
            {
                i_cinsiyetKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(sicilNo);
        }



        public async static Task<Personel?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Personel sonuc = new Personel();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Personels.FirstOrDefaultAsync(p => p.personelKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.PersonelCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.PersonelCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.y_kisiKimlik = 0;
            this.varmi = true;
        }

        public static async Task<List<Personel>> ara(params Expression<Func<Personel, bool>>[] kosullar)
        {
            return await veriTabani.PersonelCizelgesi.ara(kosullar);
        }
        public static async Task<List<Personel>> ara(veri.Varlik vari, params Expression<Func<Personel, bool>>[] kosullar)
        {
            return await veriTabani.PersonelCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Personel";
        }


        public override string _turkceAdi()
        {
            return "Personel";
        }
        public override string _birincilAnahtarAdi()
        {
            return "personelKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.personelKimlik;
        }


        #endregion


    }
}

