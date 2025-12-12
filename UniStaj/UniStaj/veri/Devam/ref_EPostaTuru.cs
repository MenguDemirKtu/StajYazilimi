using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class ref_EPostaTuru : Bilesen
    {

        public ref_EPostaTuru()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_EPostaTuru> bilesenler = ref_EPostaTuru.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ref_EPostaTuru> bilesenler = ref_EPostaTuru.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<ref_EPostaTuru> bilesenler = ref_EPostaTuru.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return String.Format("{0}/{1}", ePostaTuru, alanAdlari);
        }


        public static ref_EPostaTuru olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<ref_EPostaTuru?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_EPostaTuru sonuc = new ref_EPostaTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_EPostaTurus.FirstOrDefaultAsync(p => p.ePostaTuruKimlik == kimlik);
            }
        }


        public static ref_EPostaTuru olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_EPostaTuru sonuc = new ref_EPostaTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ref_EPostaTuruCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        protected ref_EPostaTuru cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ref_EPostaTuruCizelgesi.tekliCek(ePostaTuruKimlik, vari);
            }
        }
        public static List<ref_EPostaTuru> ara(params Predicate<ref_EPostaTuru>[] kosullar)
        {
            return veriTabani.ref_EPostaTuruCizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_EPostaTuru";
        }


        public override string _turkceAdi()
        {
            return "eposta türü";
        }
        public override string _birincilAnahtarAdi()
        {
            return "ePostaTuruKimlik";
        }


        public override long _birincilAnahtar()
        {
            return ePostaTuruKimlik;
        }


        #endregion


    }
}

