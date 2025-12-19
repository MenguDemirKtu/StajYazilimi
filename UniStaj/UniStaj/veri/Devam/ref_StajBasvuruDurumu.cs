using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace UniStaj.veri
{
    public partial class ref_StajBasvuruDurumu : Bilesen
    {

        public ref_StajBasvuruDurumu()
        {
            _varSayilan();
        }


        public void bicimlendir(veri.Varlik vari)
        {

        }

        public void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(StajBasvuruDurumuAdi, "", dilKimlik);
        }




        [NotMapped]
        public enumref_StajBasvuruDurumu _Stajbasvurudurumu
        {
            get
            {
                return (enumref_StajBasvuruDurumu)this.StajBasvuruDurumuKimlik;
            }
            set
            {
                StajBasvuruDurumuKimlik = (int)value;
            }
        }


        public override string _tanimi()
        {
            return bossaDoldur(StajBasvuruDurumuAdi);
        }



        public async static Task<ref_StajBasvuruDurumu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_StajBasvuruDurumu sonuc = new ref_StajBasvuruDurumu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_StajBasvuruDurumus.FirstOrDefaultAsync(p => p.StajBasvuruDurumuKimlik == kimlik);
            }
        }


        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        public static async Task<List<ref_StajBasvuruDurumu>> ara(params Expression<Func<ref_StajBasvuruDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_StajBasvuruDurumuCizelgesi.ara(kosullar);
        }
        public static async Task<List<ref_StajBasvuruDurumu>> ara(veri.Varlik vari, params Expression<Func<ref_StajBasvuruDurumu, bool>>[] kosullar)
        {
            return await veriTabani.ref_StajBasvuruDurumuCizelgesi.ara(vari, kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_StajBasvuruDurumu";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "StajBasvuruDurumuKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.StajBasvuruDurumuKimlik;
        }


        #endregion


    }
}

