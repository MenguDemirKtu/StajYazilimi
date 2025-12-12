using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class BelgeTuruAYRINTI : Bilesen
    {

        public BelgeTuruAYRINTI()
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


        public static BelgeTuruAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static BelgeTuruAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                BelgeTuruAYRINTI sonuc = new BelgeTuruAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.BelgeTuruAYRINTICizelgesi.tekliCek(kimlik, vari);
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
        public BelgeTuru _verisi()
        {
            BelgeTuru sonuc = BelgeTuru.olustur(belgeTurukimlik);
            return sonuc;
        }

        protected BelgeTuruAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.BelgeTuruAYRINTICizelgesi.tekliCek(belgeTurukimlik, vari);
            }
        }
        public static List<BelgeTuruAYRINTI> ara(params Predicate<BelgeTuruAYRINTI>[] kosullar)
        {
            return veriTabani.BelgeTuruAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "BelgeTuruAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "Belge Türü AYRINTI";
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

