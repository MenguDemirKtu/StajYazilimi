using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class ref_SmsGonderimTuru : Bilesen
    {

        public ref_SmsGonderimTuru()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_SmsGonderimTuru> bilesenler = ref_SmsGonderimTuru.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ref_SmsGonderimTuru> bilesenler = ref_SmsGonderimTuru.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(SmsGonderimTuruAdi, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return SmsGonderimTuruAdi.ToString();
        }


        public static ref_SmsGonderimTuru olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static ref_SmsGonderimTuru olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_SmsGonderimTuru sonuc = new ref_SmsGonderimTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ref_SmsGonderimTuruCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        protected ref_SmsGonderimTuru cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ref_SmsGonderimTuruCizelgesi.tekliCek(SmsGonderimTuruKimlik, vari);
            }
        }
        public static List<ref_SmsGonderimTuru> ara(params Predicate<ref_SmsGonderimTuru>[] kosullar)
        {
            return veriTabani.ref_SmsGonderimTuruCizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_SmsGonderimTuru";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "SmsGonderimTuruKimlik";
        }


        public override long _birincilAnahtar()
        {
            return SmsGonderimTuruKimlik;
        }


        #endregion


    }
}

