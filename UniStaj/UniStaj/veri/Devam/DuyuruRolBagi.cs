using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class DuyuruRolBagi : Bilesen
    {

        public DuyuruRolBagi()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<DuyuruRolBagi> bilesenler = DuyuruRolBagi.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<DuyuruRolBagi> bilesenler = DuyuruRolBagi.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_duyuruKimlik, "", dilKimlik);
            uyariVerInt32(i_rolKimlik, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return i_duyuruKimlik.ToString();
        }


        public static DuyuruRolBagi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static DuyuruRolBagi olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                DuyuruRolBagi sonuc = new DuyuruRolBagi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.DuyuruRolBagiCizelgesi.tekliCek(kimlik, vari);
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

        public DuyuruRolBagiAYRINTI _ayrintisi()
        {
            DuyuruRolBagiAYRINTI sonuc = DuyuruRolBagiAYRINTI.olustur(duyuruRolBagiKimlik);
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
            veriTabani.DuyuruRolBagiCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected DuyuruRolBagi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.DuyuruRolBagiCizelgesi.tekliCek(duyuruRolBagiKimlik, vari);
            }
        }
        public static List<DuyuruRolBagi> ara(params Predicate<DuyuruRolBagi>[] kosullar)
        {
            return veriTabani.DuyuruRolBagiCizelgesi.ara(kosullar);
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
            veriTabani.DuyuruRolBagiCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "DuyuruRolBagi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "duyuruRolBagiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return duyuruRolBagiKimlik;
        }


        #endregion


    }
}

