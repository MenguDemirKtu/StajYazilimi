using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class KayitAYRINTI : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KayitAYRINTI> bilesenler = KayitAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KayitAYRINTI> bilesenler = KayitAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt64(cizelgeKimlik, "", dilKimlik);
            uyariVerInt32(i_kullaniciKimlik, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return islemTuru.ToString();
        }
        public static KayitAYRINTI olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                KayitAYRINTI sonuc = new KayitAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KayitAYRINTICizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        protected KayitAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KayitAYRINTICizelgesi.tekliCek(kayitKimlik, vari); }
        }
        public static List<KayitAYRINTI> ara(KayitAYRINTIArama kosul)
        {
            return veriTabani.KayitAYRINTICizelgesi.ara(kosul);
        }
        public static List<KayitAYRINTI> ara(params Predicate<KayitAYRINTI>[] kosullar)
        {
            return veriTabani.KayitAYRINTICizelgesi.ara(kosullar);
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "KayitAYRINTI";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kayitKimlik";
        }
        public override long _birincilAnahtar()
        {
            return kayitKimlik;
        }
        #endregion
    }
}
