using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class GaleriFotosu : Bilesen
    {

        public GaleriFotosu()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GaleriFotosu> bilesenler = GaleriFotosu.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<GaleriFotosu> bilesenler = GaleriFotosu.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<GaleriFotosu> bilesenler = GaleriFotosu.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_galeriKimlik, "", dilKimlik);
            uyariVerInt64(i_fotoKimlik, "", dilKimlik);
            uyariVerBool(e_gosterimdemi, "", dilKimlik);
            uyariVerInt32(sirasi, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return i_galeriKimlik.ToString();
        }


        public static GaleriFotosu olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<GaleriFotosu?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                GaleriFotosu sonuc = new GaleriFotosu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.GaleriFotosus.FirstOrDefaultAsync(p => p.galeriFotosuKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.GaleriFotosuCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.GaleriFotosuCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static GaleriFotosu olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                GaleriFotosu sonuc = new GaleriFotosu();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.GaleriFotosuCizelgesi.tekliCek(kimlik, vari) ?? new GaleriFotosu();
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_gosterimdemi = true;
            this.varmi = true;
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
            veriTabani.GaleriFotosuCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        public static List<GaleriFotosu> ara(params Predicate<GaleriFotosu>[] kosullar)
        {
            return veriTabani.GaleriFotosuCizelgesi.ara(kosullar);
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
            veriTabani.GaleriFotosuCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "GaleriFotosu";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "galeriFotosuKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.galeriFotosuKimlik;
        }


        #endregion


    }
}

