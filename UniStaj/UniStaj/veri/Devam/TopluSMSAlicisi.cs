using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class TopluSMSAlicisi : Bilesen
    {

        public TopluSMSAlicisi()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<TopluSMSAlicisiAYRINTI> bilesenler = TopluSMSAlicisiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<TopluSMSAlicisiAYRINTI> bilesenler = TopluSMSAlicisiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_topluSMSGonderimKimlik, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return i_topluSMSGonderimKimlik.ToString();
        }


        public static TopluSMSAlicisi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static TopluSMSAlicisi olustur(Varlik vari, object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                TopluSMSAlicisi sonuc = new TopluSMSAlicisi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.TopluSMSAlicisiCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            varmi = true;
        }

        public TopluSMSAlicisiAYRINTI _ayrintisi()
        {
            TopluSMSAlicisiAYRINTI sonuc = TopluSMSAlicisiAYRINTI.olustur(toplSMSAlicisiKimlik);
            return sonuc;
        }

        public void kaydet(params bool[] yedeklensinmi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kaydet(vari, yedeklensinmi);
            }
        }
        public void kaydet(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            veriTabani.TopluSMSAlicisiCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected TopluSMSAlicisi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.TopluSMSAlicisiCizelgesi.tekliCek(toplSMSAlicisiKimlik, vari);
            }
        }
        public static List<TopluSMSAlicisi> ara(params Predicate<TopluSMSAlicisi>[] kosullar)
        {
            return veriTabani.TopluSMSAlicisiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                sil(vari);
            }
        }
        public void sil(veri.Varlik vari)
        {
            veriTabani.TopluSMSAlicisiCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "TopluSMSAlicisi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "toplSMSAlicisiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return toplSMSAlicisiKimlik;
        }


        #endregion


    }
}

