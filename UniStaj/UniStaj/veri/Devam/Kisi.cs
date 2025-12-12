using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.veriTabani;

namespace UniStaj.veri
{
    public partial class Kisi : Bilesen
    {
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KisiAYRINTI> bilesenler = KisiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur()
        {
            List<KisiAYRINTI> bilesenler = KisiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public async Task<long> kisiKaydet(veri.Varlik vari, string tc, string ad = "..", string soyad = "...")
        {
            KisiAYRINTI? sonuc = await vari.KisiAYRINTIs.FirstOrDefaultAsync(p => p.tcKimlikNo == tc);
            if (sonuc == null)
            {
                Kisi yeni = new Kisi();
                yeni.tcKimlikNo = tc;
                yeni.adi = ad;
                yeni.soyAdi = soyad;
                await vari.Kisis.AddAsync(yeni);
                await vari.SaveChangesAsync();
                return yeni.kisiKimlik;
            }
            else
            {
                return sonuc.kisiKimlik;
            }
        }
        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }
        public override string _tanimi()
        {
            return tcKimlikNo.ToString();
        }
        public static Kisi olustur(object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Kisi sonuc = new Kisi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KisiCizelgesi.tekliCek(kimlik, vari); }
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }
        public override void _varSayilan()
        {
            varmi = true;
            e_PostaDogrulandimi = true;
            e_telefonDogrulandimi = true;
            i_fotoKimlik = 0;
        }
        public KisiAYRINTI _ayrintisi()
        {
            KisiAYRINTI sonuc = KisiAYRINTI.olustur(kisiKimlik);
            return sonuc;
        }
        public void kaydet(params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.KisiCizelgesi.kaydet(this, vari, yedeklensinmi); }
        }
        protected Kisi cek()
        {
            using (veri.Varlik vari = new veri.Varlik()) { return veriTabani.KisiCizelgesi.tekliCek(kisiKimlik, vari); }
        }
        public static List<Kisi> ara(KisiArama kosul)
        {
            return veriTabani.KisiCizelgesi.ara(kosul);
        }
        public static List<Kisi> ara(params Predicate<Kisi>[] kosullar)
        {
            return veriTabani.KisiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik()) { veriTabani.KisiCizelgesi.sil(this, vari); }
        }
        #region ozluk
        public override string _cizelgeAdi()
        {
            return "Kisi";
        }
        public override string _turkceAdi()
        {
            return "Kişi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kisiKimlik";
        }
        public override long _birincilAnahtar()
        {
            return kisiKimlik;
        }
        #endregion
    }
}
