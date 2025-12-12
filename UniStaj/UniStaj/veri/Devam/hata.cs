using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class Hata : Bilesen
    {
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            if (string.IsNullOrEmpty(ifadesi))
                return "";
            else
                return ifadesi.ToString();
        }
        public static Hata olustur(object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                Hata sonuc = new Hata();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.hataCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.hataCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected Hata cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.hataCizelgesi.tekliCek(hataKimlik, vari); }
        }
        public static List<Hata> ara(hataArama kosul)
        {
            return veriTabani.hataCizelgesi.ara(kosul);
        }
        public static List<Hata> ara(params Predicate<Hata>[] kosullar)
        {
            return veriTabani.hataCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.hataCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "hata";
        }
        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "hataKimlik";
        }
        public override long _birincilAnahtar()
        {
            return hataKimlik;
        }
        #endregion
    }
}
