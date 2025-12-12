using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class ref_KisiTuru : Bilesen
    {

        public ref_KisiTuru()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_KisiTuru> bilesenler = ref_KisiTuru.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ref_KisiTuru> bilesenler = ref_KisiTuru.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(KisiTuruAdi, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return KisiTuruAdi.ToString();
        }


        public static ref_KisiTuru olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static ref_KisiTuru olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_KisiTuru sonuc = new ref_KisiTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ref_KisiTuruCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        protected ref_KisiTuru cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ref_KisiTuruCizelgesi.tekliCek(KisiTuruKimlik, vari);
            }
        }
        public static List<ref_KisiTuru> ara(params Predicate<ref_KisiTuru>[] kosullar)
        {
            return veriTabani.ref_KisiTuruCizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_KisiTuru";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "KisiTuruKimlik";
        }


        public override long _birincilAnahtar()
        {
            return KisiTuruKimlik;
        }


        #endregion


    }
}

