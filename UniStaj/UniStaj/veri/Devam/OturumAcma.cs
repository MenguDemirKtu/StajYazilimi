using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class OturumAcma : Bilesen
    {

        public OturumAcma()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<OturumAcmaAYRINTI> bilesenler = OturumAcmaAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<OturumAcmaAYRINTI> bilesenler = OturumAcmaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<OturumAcmaAYRINTI> bilesenler = OturumAcmaAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_kullaniciKimlik, ".", dilKimlik);
            uyariVerTarih(tarih, ".", dilKimlik);
            uyariVerInt32(gunlukSayi, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }


        public static OturumAcma olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<OturumAcma?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                OturumAcma sonuc = new OturumAcma();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.OturumAcmas.FirstOrDefaultAsync(p => p.oturumAcmaKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.OturumAcmaCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.OturumAcmaCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static OturumAcma olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                OturumAcma sonuc = new OturumAcma();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.OturumAcmaCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.gunlukSayi = 0;
            this.varmi = true;
        }

        public OturumAcmaAYRINTI _ayrintisi()
        {
            OturumAcmaAYRINTI sonuc = OturumAcmaAYRINTI.olustur(this.oturumAcmaKimlik);
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
            if (this.varmi == null)
                this.varmi = true;
            this.bicimlendir(vari);
            veriTabani.OturumAcmaCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected OturumAcma cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.OturumAcmaCizelgesi.tekliCek(this.oturumAcmaKimlik, vari);
            }
        }
        public static List<OturumAcma> ara(params Predicate<OturumAcma>[] kosullar)
        {
            return veriTabani.OturumAcmaCizelgesi.ara(kosullar);
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
            veriTabani.OturumAcmaCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "OturumAcma";
        }


        public override string _turkceAdi()
        {
            return "Oturum Açma";
        }
        public override string _birincilAnahtarAdi()
        {
            return "oturumAcmaKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.oturumAcmaKimlik;
        }


        #endregion


    }
}

