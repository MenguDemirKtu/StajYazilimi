using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class SifreDegisikligi : Bilesen
    {

        [NotMapped]
        public string yeniSifreTekrar { get; set; }

        public SifreDegisikligi()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SifreDegisikligi> bilesenler = SifreDegisikligi.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<SifreDegisikligi> bilesenler = SifreDegisikligi.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<SifreDegisikligi> bilesenler = SifreDegisikligi.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerTarih(tarih, "", dilKimlik);
            uyariVerInt32(i_kullaniciKimlik, "", dilKimlik);
            uyariVerString(eskiSifre, "", dilKimlik);
            uyariVerString(yeniSifre, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return tarih.ToString();
        }


        public static SifreDegisikligi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SifreDegisikligi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SifreDegisikligi sonuc = new SifreDegisikligi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SifreDegisikligis.FirstOrDefaultAsync(p => p.sifreDegisikligiKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.SifreDegisikligiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SifreDegisikligiCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static SifreDegisikligi olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SifreDegisikligi sonuc = new SifreDegisikligi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SifreDegisikligiCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        public SifreDegisikligiAYRINTI _ayrintisi()
        {
            SifreDegisikligiAYRINTI sonuc = SifreDegisikligiAYRINTI.olustur(sifreDegisikligiKimlik);
            return sonuc;
        }

        public void kaydet(params bool[] yedeklensinmi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kaydet(vari, yedeklensinmi);
            }
        }
        public void kaydet(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            veriTabani.SifreDegisikligiCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected SifreDegisikligi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SifreDegisikligiCizelgesi.tekliCek(sifreDegisikligiKimlik, vari);
            }
        }
        public static List<SifreDegisikligi> ara(params Predicate<SifreDegisikligi>[] kosullar)
        {
            return veriTabani.SifreDegisikligiCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                sil(vari);
            }
        }
        public void sil(veri.Varlik vari)
        {
            veriTabani.SifreDegisikligiCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SifreDegisikligi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sifreDegisikligiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return sifreDegisikligiKimlik;
        }


        #endregion


    }
}

