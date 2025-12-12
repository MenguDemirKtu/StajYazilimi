using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class Kullanici : Bilesen
    {

        public Kullanici()
        {
            _varSayilan();
        }

        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.KullaniciCizelgesi.silKos(this, vari, yedeklensinmi);
        }
        public async static Task<Kullanici?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Kullanici sonuc = new Kullanici();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kimlik && p.varmi == true);
            }
        }
        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<KullaniciAYRINTI> bilesenler = KullaniciAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<KullaniciAYRINTI> bilesenler = KullaniciAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {
            if (e_sifreDegisecekmi == null)
                e_sifreDegisecekmi = true;

            if (string.IsNullOrEmpty(fotoBilgisi))
                fotoBilgisi = "/genel.png";

            if (e_sozlesmeOnaylandimi == null)
                e_sozlesmeOnaylandimi = false;

            if (string.IsNullOrEmpty(kodu))
                kodu = Guid.NewGuid().ToString();

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(kullaniciAdi, "Kullanıcı Adı", dilKimlik);
        }


        public override string _tanimi()
        {
            return kullaniciAdi.ToString();
        }


        public static Kullanici olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }

        public static Kullanici olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                Kullanici sonuc = new Kullanici();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.KullaniciCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            i_dilKimlik = 1;
            e_faalmi = true;
            e_rolTabanlimi = true;
            e_sozlesmeOnaylandimi = false;
            e_sifreDegisecekmi = true;
        }

        #region bu_sinifina_bagli_siniflar
        public List<KullaniciRolu> _KullaniciRoluBilgileri()
        {
            return veriTabani.KullaniciRoluCizelgesi.ara(p => p.i_kullaniciKimlik == kullaniciKimlik, p => p.varmi == true);
        }


        public List<KullaniciRoluAYRINTI> _KullaniciRoluAYRINTIBilgileri()
        {
            return veriTabani.KullaniciRoluAYRINTICizelgesi.ara(p => p.i_kullaniciKimlik == kullaniciKimlik);
        }
        #endregion bu_sinifina_bagli_siniflar


        public KullaniciAYRINTI _ayrintisi()
        {
            KullaniciAYRINTI sonuc = KullaniciAYRINTI.olustur(kullaniciKimlik);
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
            veriTabani.KullaniciCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected Kullanici cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.KullaniciCizelgesi.tekliCek(kullaniciKimlik, vari);
            }
        }
        public static List<Kullanici> ara(params Predicate<Kullanici>[] kosullar)
        {
            return veriTabani.KullaniciCizelgesi.ara(kosullar);
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
            veriTabani.KullaniciCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Kullanici";
        }


        public override string _turkceAdi()
        {
            return "Kullanıcı";
        }
        public override string _birincilAnahtarAdi()
        {
            return "kullaniciKimlik";
        }


        public override long _birincilAnahtar()
        {
            return kullaniciKimlik;
        }


        #endregion


    }
}

