using Microsoft.AspNetCore.Mvc.Rendering;

namespace UniStaj.veri
{
    public partial class GonderilenSMS : Bilesen
    {
        public void bicimlendir(veri.Varlik vari)
        {

        }
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GonderilenSMSAYRINTI> bilesenler = GonderilenSMSAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<GonderilenSMSAYRINTI> bilesenler = GonderilenSMSAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return telefon.ToString();
        }
        public static GonderilenSMS olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                GonderilenSMS sonuc = new GonderilenSMS();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.GonderilenSMSCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            varmi = true;
            e_gonderildimi = false;
        }
        public GonderilenSMSAYRINTI _ayrintisi()
        {
            GonderilenSMSAYRINTI sonuc = GonderilenSMSAYRINTI.olustur(gonderilenSMSKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.GonderilenSMSCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        public void kaydet(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            veriTabani.GonderilenSMSCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected GonderilenSMS cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.GonderilenSMSCizelgesi.tekliCek(gonderilenSMSKimlik, vari); }
        }

        public static List<GonderilenSMS> ara(params Predicate<GonderilenSMS>[] kosullar)
        {
            return veriTabani.GonderilenSMSCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.GonderilenSMSCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "GonderilenSMS";
        }
        public override string _turkceAdi()
        {
            return "Gönderilen SMS";
        }
        public override string _birincilAnahtarAdi()
        {
            return "gonderilenSMSKimlik";
        }
        public override long _birincilAnahtar()
        {
            return gonderilenSMSKimlik;
        }
        #endregion
    }
}
