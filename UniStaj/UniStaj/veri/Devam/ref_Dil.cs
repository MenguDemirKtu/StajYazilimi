using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class ref_Dil : Bilesen
    {

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_Dil> bilesenler = ref_Dil.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ref_Dil> bilesenler = ref_Dil.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir()
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return dilAdi.ToString();
        }


        public static ref_Dil olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static ref_Dil olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_Dil sonuc = new ref_Dil();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ref_DilCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        protected ref_Dil cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ref_DilCizelgesi.tekliCek(dilKimlik, vari);
            }
        }
        public static List<ref_Dil> ara(ref_DilArama kosul)
        {
            return veriTabani.ref_DilCizelgesi.ara(kosul);
        }
        public static List<ref_Dil> ara(params Predicate<ref_Dil>[] kosullar)
        {
            return veriTabani.ref_DilCizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_Dil";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "dilKimlik";
        }


        public override long _birincilAnahtar()
        {
            return dilKimlik;
        }


        #endregion


    }
}

