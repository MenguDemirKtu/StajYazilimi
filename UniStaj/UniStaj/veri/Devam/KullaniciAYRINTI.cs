using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class KullaniciAYRINTI : Bilesen
    {

        public KullaniciAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

            if (string.IsNullOrEmpty(this.kodu))
                this.kodu = Guid.NewGuid().ToString();
        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(kullaniciAdi, ".", dilKimlik);
            uyariVerString(kullaniciTuru, ".", dilKimlik);
        }




        [NotMapped]
        public enumref_KullaniciTuru _Kullanicituru
        {
            get
            {
                return (enumref_KullaniciTuru)(this.i_kullaniciTuruKimlik ?? 1);
            }
            set
            {
                i_kullaniciTuruKimlik = (int)value;
            }
        }
        [NotMapped]
        public enumref_Dil _Dil
        {
            get
            {
                return (enumref_Dil)this.i_dilKimlik;
            }
            set
            {
                i_dilKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(kullaniciAdi);
        }



        public async static Task<KullaniciAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciAYRINTI sonuc = new KullaniciAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciKimlik == kimlik && p.varmi == true);
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



        public static async Task<List<KullaniciAYRINTI>> ara(params Expression<Func<KullaniciAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.KullaniciAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<KullaniciAYRINTI>> ara(veri.Varlik vari, params Expression<Func<KullaniciAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.KullaniciAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "KullaniciAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Kullanıcı";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.kullaniciKimlik;
        }


        #endregion


    }
}

