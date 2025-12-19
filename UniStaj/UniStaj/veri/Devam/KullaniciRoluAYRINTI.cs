using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class KullaniciRoluAYRINTI : Bilesen
    {

        public KullaniciRoluAYRINTI()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_kullaniciKimlik, ".", dilKimlik);
            uyariVerInt32(i_rolKimlik, ".", dilKimlik);
            uyariVerBool(e_gecerlimi, ".", dilKimlik);
            uyariVerString(kullaniciAdi, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return bossaDoldur(i_kullaniciKimlik);
        }



        public async static Task<KullaniciRoluAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KullaniciRoluAYRINTI sonuc = new KullaniciRoluAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.KullaniciRoluAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciRoluKimlik == kimlik && p.varmi == true);
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

        #region bu_sinifin_bagli_oldugu_sinif
        public Kullanici _KullaniciBilgisi()
        {
            return Kullanici.olustur(this.i_kullaniciKimlik);
        }

        #endregion bu_sinifin_bagli_oldugu_sinif

        public static async Task<List<KullaniciRoluAYRINTI>> ara(params Expression<Func<KullaniciRoluAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.KullaniciRoluAYRINTICizelgesi.ara(kosullar);
        }
        public static async Task<List<KullaniciRoluAYRINTI>> ara(veri.Varlik vari, params Expression<Func<KullaniciRoluAYRINTI, bool>>[] kosullar)
        {
            return await veriTabani.KullaniciRoluAYRINTICizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "KullaniciRoluAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "KullaniciRoluA";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciRoluKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.kullaniciRoluKimlik;
        }


        #endregion


    }
}

