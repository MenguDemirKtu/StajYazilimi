using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class BelgeTuru : Bilesen
    {

        public BelgeTuru()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<BelgeTuruAYRINTI> bilesenler = BelgeTuruAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<BelgeTuruAYRINTI> bilesenler = BelgeTuruAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur(int[] secililer)
        {
            List<BelgeTuruAYRINTI> bilesenler = BelgeTuruAYRINTI.ara();
            return doldur2(bilesenler, secililer);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return belgeTuruAdi.ToString();
        }


        public static BelgeTuru olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static BelgeTuru olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                BelgeTuru sonuc = new BelgeTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.BelgeTuruCizelgesi.tekliCek(kimlik, vari);
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

        public BelgeTuruAYRINTI _ayrintisi()
        {
            BelgeTuruAYRINTI sonuc = BelgeTuruAYRINTI.olustur(belgeTurukimlik);
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
            veriTabani.BelgeTuruCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected BelgeTuru cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.BelgeTuruCizelgesi.tekliCek(belgeTurukimlik, vari);
            }
        }
        public static List<BelgeTuru> ara(params Predicate<BelgeTuru>[] kosullar)
        {
            return veriTabani.BelgeTuruCizelgesi.ara(kosullar);
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
            veriTabani.BelgeTuruCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "BelgeTuru";
        }


        public override string _turkceAdi()
        {
            return "Belge Türü";
        }
        public override string _birincilAnahtarAdi()
        {
            return "belgeTurukimlik";
        }


        public override long _birincilAnahtar()
        {
            return belgeTurukimlik;
        }


        #endregion


    }
}

