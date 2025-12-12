using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class Duyuru : Bilesen
    {

        public Duyuru()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<DuyuruAYRINTI> bilesenler = DuyuruAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<DuyuruAYRINTI> bilesenler = DuyuruAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerTarih(baslangic, "Başlangıç", dilKimlik);
            uyariVerTarih(baslangic, "Bitiş", dilKimlik);
            if (bitis < baslangic)
                uyariVer("Başlangıç tarihi bitiş tarihinden önce olmalıdır", dilKimlik);

        }


        public override string _tanimi()
        {
            return baslik.ToString();
        }


        public static Duyuru olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static Duyuru olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                Duyuru sonuc = new Duyuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.DuyuruCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            varmi = true;
            e_yayindami = true;
            baslangic = DateTime.Today;
            bitis = DateTime.Today.AddDays(30);

        }

        public DuyuruAYRINTI _ayrintisi()
        {
            DuyuruAYRINTI sonuc = DuyuruAYRINTI.olustur(duyurukimlik);
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
            veriTabani.DuyuruCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected Duyuru cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.DuyuruCizelgesi.tekliCek(duyurukimlik, vari);
            }
        }
        public static List<Duyuru> ara(params Predicate<Duyuru>[] kosullar)
        {
            return veriTabani.DuyuruCizelgesi.ara(kosullar);
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
            veriTabani.DuyuruCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Duyuru";
        }


        public override string _turkceAdi()
        {
            return "Duyuru";
        }
        public override string _birincilAnahtarAdi()
        {
            return "duyurukimlik";
        }


        public override long _birincilAnahtar()
        {
            return duyurukimlik;
        }


        #endregion


    }
}

