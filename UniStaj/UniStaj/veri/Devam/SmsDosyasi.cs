using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class SmsDosyasi : Bilesen
    {

        public SmsDosyasi()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SmsDosyasiAYRINTI> bilesenler = SmsDosyasiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<SmsDosyasiAYRINTI> bilesenler = SmsDosyasiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return i_dosyaKimlik.ToString();
        }


        public static SmsDosyasi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static SmsDosyasi olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SmsDosyasi sonuc = new SmsDosyasi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SmsDosyasiCizelgesi.tekliCek(kimlik, vari);
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

        public SmsDosyasiAYRINTI _ayrintisi()
        {
            SmsDosyasiAYRINTI sonuc = SmsDosyasiAYRINTI.olustur(smsDosyasiKimlik);
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
            veriTabani.SmsDosyasiCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected SmsDosyasi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SmsDosyasiCizelgesi.tekliCek(smsDosyasiKimlik, vari);
            }
        }
        public static List<SmsDosyasi> ara(params Predicate<SmsDosyasi>[] kosullar)
        {
            return veriTabani.SmsDosyasiCizelgesi.ara(kosullar);
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
            veriTabani.SmsDosyasiCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SmsDosyasi";
        }


        public override string _turkceAdi()
        {
            return "SMS Dosyası";
        }
        public override string _birincilAnahtarAdi()
        {
            return "smsDosyasiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return smsDosyasiKimlik;
        }


        #endregion


    }
}

