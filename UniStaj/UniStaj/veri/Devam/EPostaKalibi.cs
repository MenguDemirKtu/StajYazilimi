using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class EPostaKalibi : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<EPostaKalibiAYRINTI> bilesenler = EPostaKalibiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<EPostaKalibiAYRINTI> bilesenler = EPostaKalibiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        /// <summary>
        /// ref_EPostaTuru tablosunda yer alan enumu ifade eder.
        /// kendisi doğrudan veri tabanında yer almaz.
        /// i_ePostaTuruKimlik sütunundan veri çeker.
        /// </summary>
        public enumref_EPostaTuru _ePostaTuru()
        {
            return (enumref_EPostaTuru)i_ePostaTuruKimlik;
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_ePostaTuruKimlik, "", dilKimlik);
            uyariVerBool(e_gecerlimi, "", dilKimlik);
        }
        public override string _tanimi()
        {
            return kalipBasligi.ToString();
        }
        public static EPostaKalibi olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                EPostaKalibi sonuc = new EPostaKalibi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.EPostaKalibiCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            e_gecerlimi = true;
            varmi = true;
        }
        #region bu_sinifin_bagli_oldugu_sinif
        public ref_EPostaTuru _ref_EPostaTuruBilgisi()
        {
            return ref_EPostaTuru.olustur(i_ePostaTuruKimlik);
        }
        #endregion bu_sinifin_bagli_oldugu_sinif
        public EPostaKalibiAYRINTI _ayrintisi()
        {
            EPostaKalibiAYRINTI sonuc = EPostaKalibiAYRINTI.olustur(ePostaKalibiKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.EPostaKalibiCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected EPostaKalibi cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.EPostaKalibiCizelgesi.tekliCek(ePostaKalibiKimlik, vari); }
        }
        public static List<EPostaKalibi> ara(EPostaKalibiArama kosul)
        {
            return veriTabani.EPostaKalibiCizelgesi.ara(kosul);
        }
        public static List<EPostaKalibi> ara(params Predicate<EPostaKalibi>[] kosullar)
        {
            return veriTabani.EPostaKalibiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.EPostaKalibiCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "EPostaKalibi";
        }
        public override string _turkceAdi()
        {
            return "E-Posta Kalıbı";
        }
        public override string _birincilAnahtarAdi()
        {
            return "ePostaKalibiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return ePostaKalibiKimlik;
        }
        #endregion
    }
}
