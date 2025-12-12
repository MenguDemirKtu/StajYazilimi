using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class ref_Cinsiyet : Bilesen
    {

        public ref_Cinsiyet()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_Cinsiyet> bilesenler = ref_Cinsiyet.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ref_Cinsiyet> bilesenler = ref_Cinsiyet.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(CinsiyetAdi, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return CinsiyetAdi.ToString();
        }


        public static ref_Cinsiyet olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static ref_Cinsiyet olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_Cinsiyet sonuc = new ref_Cinsiyet();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ref_CinsiyetCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        protected ref_Cinsiyet cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ref_CinsiyetCizelgesi.tekliCek(CinsiyetKimlik, vari);
            }
        }
        public static List<ref_Cinsiyet> ara(params Predicate<ref_Cinsiyet>[] kosullar)
        {
            return veriTabani.ref_CinsiyetCizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_Cinsiyet";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "CinsiyetKimlik";
        }


        public override long _birincilAnahtar()
        {
            return CinsiyetKimlik;
        }


        #endregion


    }
}

