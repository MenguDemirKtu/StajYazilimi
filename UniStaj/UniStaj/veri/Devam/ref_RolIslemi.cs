using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class ref_RolIslemi : Bilesen
    {

        public ref_RolIslemi()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_RolIslemi> bilesenler = ref_RolIslemi.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ref_RolIslemi> bilesenler = ref_RolIslemi.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(rolIslemiAdi, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return rolIslemiAdi.ToString();
        }


        public static ref_RolIslemi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static ref_RolIslemi olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_RolIslemi sonuc = new ref_RolIslemi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ref_RolIslemiCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        protected ref_RolIslemi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ref_RolIslemiCizelgesi.tekliCek(rolIslemiKimlik, vari);
            }
        }
        public static List<ref_RolIslemi> ara(params Predicate<ref_RolIslemi>[] kosullar)
        {
            return veriTabani.ref_RolIslemiCizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_RolIslemi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "rolIslemiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return rolIslemiKimlik;
        }


        #endregion


    }
}

