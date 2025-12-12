using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class TopluSmsGonderimAYRINTI : Bilesen
    {

        public TopluSmsGonderimAYRINTI()
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

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return metin.ToString();
        }


        public static TopluSmsGonderimAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static TopluSmsGonderimAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                TopluSmsGonderimAYRINTI sonuc = new TopluSmsGonderimAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.TopluSmsGonderimAYRINTICizelgesi.tekliCek(kimlik, vari);
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
        public TopluSmsGonderim _verisi()
        {
            TopluSmsGonderim sonuc = TopluSmsGonderim.olustur(topluSMSGonderimKimlik);
            return sonuc;
        }

        protected TopluSmsGonderimAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.TopluSmsGonderimAYRINTICizelgesi.tekliCek(topluSMSGonderimKimlik, vari);
            }
        }
        public static List<TopluSmsGonderimAYRINTI> ara(params Predicate<TopluSmsGonderimAYRINTI>[] kosullar)
        {
            return veriTabani.TopluSmsGonderimAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "TopluSmsGonderimAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "";
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

