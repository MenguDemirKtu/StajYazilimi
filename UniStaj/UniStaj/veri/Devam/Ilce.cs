using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class Ilce : Bilesen
    {

        public Ilce()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {
            if (varmi == null)
                varmi = true;

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_sehirKimlik, "Şehir", dilKimlik);

            if (i_sehirKimlik > 81)
                uyariVer("İl 81'den büyük olamaz", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(ilceAdi);
        }



        public async static Task<Ilce?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Ilce sonuc = new Ilce();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Ilces.FirstOrDefaultAsync(p => p.ilcekimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.IlceCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.IlceCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
        }

        public static async Task<List<Ilce>> ara(params Expression<Func<Ilce, bool>>[] kosullar)
        {
            return await veriTabani.IlceCizelgesi.ara(kosullar);
        }
        public static async Task<List<Ilce>> ara(veri.Varlik vari, params Expression<Func<Ilce, bool>>[] kosullar)
        {
            return await veriTabani.IlceCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Ilce";
        }


        public override string _turkceAdi()
        {
            return "İlçe";
        }
        public override string _birincilAnahtarAdi()
        {
            return "ilcekimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.ilcekimlik;
        }


        #endregion


    }
}

