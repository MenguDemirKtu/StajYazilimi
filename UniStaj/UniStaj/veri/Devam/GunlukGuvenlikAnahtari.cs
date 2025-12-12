using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class GunlukGuvenlikAnahtari : Bilesen
    {

        public GunlukGuvenlikAnahtari()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GunlukGuvenlikAnahtari> bilesenler = GunlukGuvenlikAnahtari.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<GunlukGuvenlikAnahtari> bilesenler = GunlukGuvenlikAnahtari.ara();
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
            return tarih.ToString();
        }


        public static GunlukGuvenlikAnahtari olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static GunlukGuvenlikAnahtari olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                GunlukGuvenlikAnahtari sonuc = new GunlukGuvenlikAnahtari();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.GunlukGuvenlikAnahtariCizelgesi.tekliCek(kimlik, vari);
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
            veriTabani.GunlukGuvenlikAnahtariCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected GunlukGuvenlikAnahtari cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.GunlukGuvenlikAnahtariCizelgesi.tekliCek(gunlukGuvenlikAnahtariKimlik, vari);
            }
        }
        public static List<GunlukGuvenlikAnahtari> ara(params Predicate<GunlukGuvenlikAnahtari>[] kosullar)
        {
            return veriTabani.GunlukGuvenlikAnahtariCizelgesi.ara(kosullar);
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
            veriTabani.GunlukGuvenlikAnahtariCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "GunlukGuvenlikAnahtari";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "gunlukGuvenlikAnahtariKimlik";
        }


        public override long _birincilAnahtar()
        {
            return gunlukGuvenlikAnahtariKimlik;
        }


        #endregion


    }
}

