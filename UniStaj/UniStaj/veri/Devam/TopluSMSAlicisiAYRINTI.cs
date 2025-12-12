using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class TopluSMSAlicisiAYRINTI : Bilesen
    {

        public TopluSMSAlicisiAYRINTI()
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


        public static TopluSMSAlicisiAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static TopluSMSAlicisiAYRINTI olustur(Varlik vari, object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                TopluSMSAlicisiAYRINTI sonuc = new TopluSMSAlicisiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.TopluSMSAlicisiAYRINTICizelgesi.tekliCek(kimlik, vari);
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
        public TopluSMSAlicisi _verisi()
        {
            TopluSMSAlicisi sonuc = TopluSMSAlicisi.olustur(toplSMSAlicisiKimlik);
            return sonuc;
        }

        protected TopluSMSAlicisiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.TopluSMSAlicisiAYRINTICizelgesi.tekliCek(toplSMSAlicisiKimlik, vari);
            }
        }
        public static List<TopluSMSAlicisiAYRINTI> ara(params Predicate<TopluSMSAlicisiAYRINTI>[] kosullar)
        {
            return veriTabani.TopluSMSAlicisiAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "TopluSMSAlicisiAYRINTI";
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

