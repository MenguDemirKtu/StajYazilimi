using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class TopluSmsGonderim : Bilesen
    {

        public TopluSmsGonderim()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<TopluSmsGonderimAYRINTI> bilesenler = TopluSmsGonderimAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<TopluSmsGonderimAYRINTI> bilesenler = TopluSmsGonderimAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {
            if (string.IsNullOrEmpty(kodu))
                kodu = Guid.NewGuid().ToString();
        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            if (string.IsNullOrEmpty(metin))
                return "";
            return metin.ToString();
        }


        public static TopluSmsGonderim olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static TopluSmsGonderim olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                TopluSmsGonderim sonuc = new TopluSmsGonderim();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.TopluSmsGonderimCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            varmi = true;
            tarih = DateTime.Now;
            kodu = Guid.NewGuid().ToString();
        }

        public TopluSmsGonderimAYRINTI _ayrintisi()
        {
            TopluSmsGonderimAYRINTI sonuc = TopluSmsGonderimAYRINTI.olustur(topluSMSGonderimKimlik);
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
            veriTabani.TopluSmsGonderimCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected TopluSmsGonderim cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.TopluSmsGonderimCizelgesi.tekliCek(topluSMSGonderimKimlik, vari);
            }
        }
        public static List<TopluSmsGonderim> ara(params Predicate<TopluSmsGonderim>[] kosullar)
        {
            return veriTabani.TopluSmsGonderimCizelgesi.ara(kosullar);
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
            veriTabani.TopluSmsGonderimCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "TopluSmsGonderim";
        }


        public override string _turkceAdi()
        {
            return "Toplu SMS Gönderim";
        }
        public override string _birincilAnahtarAdi()
        {
            return "topluSMSGonderimKimlik";
        }


        public override long _birincilAnahtar()
        {
            return topluSMSGonderimKimlik;
        }


        #endregion


    }
}

