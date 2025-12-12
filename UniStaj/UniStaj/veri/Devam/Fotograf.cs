using Microsoft.AspNetCore.Mvc.Rendering;
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class Fotograf : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<FotografAYRINTI> bilesenler = FotografAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<FotografAYRINTI> bilesenler = FotografAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            if (string.IsNullOrEmpty(ilgiliCizelge))
                return "";
            else
                return ilgiliCizelge.ToString();
        }
        public static Fotograf olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Fotograf sonuc = new Fotograf();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.FotografCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            e_sabitmi = false;
        }
        public FotografAYRINTI _ayrintisi()
        {
            FotografAYRINTI sonuc = FotografAYRINTI.olustur(fotografKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.FotografCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected Fotograf cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.FotografCizelgesi.tekliCek(fotografKimlik, vari); }
        }
        public static List<Fotograf> ara(FotografArama kosul)
        {
            return veriTabani.FotografCizelgesi.ara(kosul);
        }
        public static List<Fotograf> ara(params Predicate<Fotograf>[] kosullar)
        {
            return veriTabani.FotografCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.FotografCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "Fotograf";
        }
        public override string _turkceAdi()
        {
            return "Fotoğraf";
        }
        public override string _birincilAnahtarAdi()
        {
            return "fotografKimlik";
        }
        public override long _birincilAnahtar()
        {
            return fotografKimlik;
        }
        #endregion
    }
}
