using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class Galeri : Bilesen
    {

        public Galeri()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GaleriAYRINTI> bilesenler = GaleriAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<GaleriAYRINTI> bilesenler = GaleriAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<GaleriAYRINTI> bilesenler = GaleriAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {
            if (string.IsNullOrEmpty(this.galeriUrl))
                this.galeriUrl = Genel.urlDuzenle(this.galeriAdi ?? "");

            if (string.IsNullOrEmpty(this.kodu))
                kodu = Guid.NewGuid().ToString();
        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt64(ilgiliKimlik, "İlgili Kimlik ", dilKimlik);
        }


        public override string _tanimi()
        {
            return galeriAdi.ToString();
        }


        public static Galeri olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<Galeri?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Galeri sonuc = new Galeri();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Galeris.FirstOrDefaultAsync(p => p.galeriKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.GaleriCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.GaleriCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static Galeri olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                Galeri sonuc = new Galeri();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.GaleriCizelgesi.tekliCek(kimlik, vari) ?? new Galeri();
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.ilgiliKimlik = 0;
            this.genislik = 800;
            this.yukseklik = 600;
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
            veriTabani.GaleriCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        public static List<Galeri> ara(params Predicate<Galeri>[] kosullar)
        {
            return veriTabani.GaleriCizelgesi.ara(kosullar);
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
            veriTabani.GaleriCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Galeri";
        }


        public override string _turkceAdi()
        {
            return "Galeri";
        }
        public override string _birincilAnahtarAdi()
        {
            return "galeriKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.galeriKimlik;
        }


        #endregion


    }
}

