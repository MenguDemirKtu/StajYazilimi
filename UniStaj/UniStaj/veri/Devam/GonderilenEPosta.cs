using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class GonderilenEPosta : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GonderilenEPostaAYRINTI> bilesenler = GonderilenEPostaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<GonderilenEPostaAYRINTI> bilesenler = GonderilenEPostaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return adres.ToString();
        }
        public static GonderilenEPosta olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                GonderilenEPosta sonuc = new GonderilenEPosta();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.GonderilenEPostaCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            varmi = true;
            y_topluGonderimKimlik = 0;
        }
        public GonderilenEPostaAYRINTI _ayrintisi()
        {
            GonderilenEPostaAYRINTI sonuc = GonderilenEPostaAYRINTI.olustur(gonderilenEPostaKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.GonderilenEPostaCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected GonderilenEPosta cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.GonderilenEPostaCizelgesi.tekliCek(gonderilenEPostaKimlik, vari); }
        }
        public static List<GonderilenEPosta> ara(GonderilenEPostaArama kosul)
        {
            return veriTabani.GonderilenEPostaCizelgesi.ara(kosul);
        }
        public static List<GonderilenEPosta> ara(params Predicate<GonderilenEPosta>[] kosullar)
        {
            return veriTabani.GonderilenEPostaCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.GonderilenEPostaCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "GonderilenEPosta";
        }
        public override string _turkceAdi()
        {
            return "Gönderilen E-Posta";
        }
        public override string _birincilAnahtarAdi()
        {
            return "gonderilenEPostaKimlik";
        }
        public override long _birincilAnahtar()
        {
            return gonderilenEPostaKimlik;
        }
        #endregion
    }
}
