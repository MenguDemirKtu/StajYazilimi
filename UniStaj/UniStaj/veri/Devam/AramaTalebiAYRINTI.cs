using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class AramaTalebiAYRINTI : Bilesen
    {

        public AramaTalebiAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<AramaTalebiAYRINTI> bilesenler = AramaTalebiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<AramaTalebiAYRINTI> bilesenler = AramaTalebiAYRINTI.ara();
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
            return kodu.ToString();
        }


        public static AramaTalebiAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static AramaTalebiAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                AramaTalebiAYRINTI sonuc = new AramaTalebiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.AramaTalebiAYRINTICizelgesi.tekliCek(kimlik, vari);
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
        public AramaTalebi _verisi()
        {
            AramaTalebi sonuc = AramaTalebi.olustur(aramaTalebiKimlik);
            return sonuc;
        }

        protected AramaTalebiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.AramaTalebiAYRINTICizelgesi.tekliCek(aramaTalebiKimlik, vari);
            }
        }
        public static List<AramaTalebiAYRINTI> ara(params Predicate<AramaTalebiAYRINTI>[] kosullar)
        {
            return veriTabani.AramaTalebiAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "AramaTalebiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "AramaTalebiAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "aramaTalebiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return aramaTalebiKimlik;
        }


        #endregion


    }
}

