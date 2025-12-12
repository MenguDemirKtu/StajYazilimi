using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class SosyalMedya : Bilesen
    {

        public SosyalMedya()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SosyalMedyaAYRINTI> bilesenler = SosyalMedyaAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<SosyalMedyaAYRINTI> bilesenler = SosyalMedyaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<SosyalMedyaAYRINTI> bilesenler = SosyalMedyaAYRINTI.ara();
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
            return sosyamMedyaAdi.ToString();
        }


        public static SosyalMedya olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SosyalMedya?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SosyalMedya sonuc = new SosyalMedya();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SosyalMedyas.FirstOrDefaultAsync(p => p.sosyalMedyakimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            await veriTabani.SosyalMedyaCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SosyalMedyaCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static SosyalMedya olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SosyalMedya sonuc = new SosyalMedya();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SosyalMedyaCizelgesi.tekliCek(kimlik, vari) ?? new SosyalMedya();
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
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
            veriTabani.SosyalMedyaCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        public static List<SosyalMedya> ara(params Predicate<SosyalMedya>[] kosullar)
        {
            return veriTabani.SosyalMedyaCizelgesi.ara(kosullar);
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
            veriTabani.SosyalMedyaCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SosyalMedya";
        }


        public override string _turkceAdi()
        {
            return "Sosyal Medya";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sosyalMedyakimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sosyalMedyakimlik;
        }


        #endregion


    }
}

