using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class DuyuruRolBagiAYRINTI : Bilesen
    {

        public DuyuruRolBagiAYRINTI()
        {
            _varSayilan();
        }

        public string zamanIfadesi()
        {
            return baslangic.ToShortDateString();

        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<DuyuruRolBagiAYRINTI> bilesenler = DuyuruRolBagiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<DuyuruRolBagiAYRINTI> bilesenler = DuyuruRolBagiAYRINTI.ara();
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


        public static DuyuruRolBagiAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static DuyuruRolBagiAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                DuyuruRolBagiAYRINTI sonuc = new DuyuruRolBagiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.DuyuruRolBagiAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }


        /// <summary>
        /// Veri tabanından kayıtlı olan verisini çeker. Dolayısıyla yapılan bir değişikliği aktaran işlemi burada yeniden seçemeyiz.  
        /// </summary>
        /// <returns></returns>
        public DuyuruRolBagi _verisi()
        {
            DuyuruRolBagi sonuc = DuyuruRolBagi.olustur(duyuruRolBagiKimlik);
            return sonuc;
        }

        protected DuyuruRolBagiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.DuyuruRolBagiAYRINTICizelgesi.tekliCek(duyuruRolBagiKimlik, vari);
            }
        }
        public static List<DuyuruRolBagiAYRINTI> ara(params Predicate<DuyuruRolBagiAYRINTI>[] kosullar)
        {
            return veriTabani.DuyuruRolBagiAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "DuyuruRolBagiAYRINTI";
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

