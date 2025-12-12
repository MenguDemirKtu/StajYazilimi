using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class SmsDosyasiAYRINTI : Bilesen
    {

        public SmsDosyasiAYRINTI()
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


        public static SmsDosyasiAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static SmsDosyasiAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SmsDosyasiAYRINTI sonuc = new SmsDosyasiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SmsDosyasiAYRINTICizelgesi.tekliCek(kimlik, vari);
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
        public SmsDosyasi _verisi()
        {
            SmsDosyasi sonuc = SmsDosyasi.olustur(smsDosyasiKimlik);
            return sonuc;
        }

        protected SmsDosyasiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SmsDosyasiAYRINTICizelgesi.tekliCek(smsDosyasiKimlik, vari);
            }
        }
        public static List<SmsDosyasiAYRINTI> ara(params Predicate<SmsDosyasiAYRINTI>[] kosullar)
        {
            return veriTabani.SmsDosyasiAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SmsDosyasiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "";
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

