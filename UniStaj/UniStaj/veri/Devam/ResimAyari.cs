using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ResimAyari : Bilesen
    {
        [NotMapped]
        public bool e_genislik1Varmi
        {
            get { return true; }
        }

        public ResimAyari()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ResimAyariAYRINTI> bilesenler = ResimAyariAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ResimAyariAYRINTI> bilesenler = ResimAyariAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<ResimAyariAYRINTI> bilesenler = ResimAyariAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
        }


        public override string _tanimi()
        {
            return ilgiliCizelge.ToString();
        }


        public static ResimAyari olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<ResimAyari?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ResimAyari sonuc = new ResimAyari();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ResimAyaris.FirstOrDefaultAsync(p => p.resimAyariKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.ResimAyariCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.ResimAyariCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static ResimAyari olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ResimAyari sonuc = new ResimAyari();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ResimAyariCizelgesi.tekliCek(kimlik, vari) ?? new ResimAyari();
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
            this.e_genislik2Varmi = true;
            this.e_genislik3Varmi = false;
            this.e_genislik4Varmi = false;
            this.genislik = 800;
            this.genislik2 = 600;
            this.genislik3 = 400;
            this.genislik4 = 200;
            this.yukseklik = 600;
            this.yukseklik2 = 350;
            this.yukseklik3 = 300;
            this.yukseklik4 = 150;
            this.kalite = 95;
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
            veriTabani.ResimAyariCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        public static List<ResimAyari> ara(params Predicate<ResimAyari>[] kosullar)
        {
            return veriTabani.ResimAyariCizelgesi.ara(kosullar);
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
            veriTabani.ResimAyariCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ResimAyari";
        }


        public override string _turkceAdi()
        {
            return "Resim Ayarı";
        }
        public override string _birincilAnahtarAdi()
        {
            return "resimAyariKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.resimAyariKimlik;
        }


        #endregion


    }
}

