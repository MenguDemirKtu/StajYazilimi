using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class AramaTalebi : Bilesen
    {

        public AramaTalebi()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<AramaTalebiAYRINTI> bilesenler = AramaTalebiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<AramaTalebiAYRINTI> bilesenler = AramaTalebiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<AramaTalebiAYRINTI> bilesenler = AramaTalebiAYRINTI.ara();
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
            return kodu.ToString();
        }


        public static AramaTalebi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<AramaTalebi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                AramaTalebi sonuc = new AramaTalebi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.AramaTalebis.FirstOrDefaultAsync(p => p.aramaTalebiKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.AramaTalebiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.AramaTalebiCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static AramaTalebi olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                AramaTalebi sonuc = new AramaTalebi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.AramaTalebiCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            this.varmi = true;
        }

        public AramaTalebiAYRINTI _ayrintisi()
        {
            AramaTalebiAYRINTI sonuc = AramaTalebiAYRINTI.olustur(this.aramaTalebiKimlik);
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
            veriTabani.AramaTalebiCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected AramaTalebi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.AramaTalebiCizelgesi.tekliCek(this.aramaTalebiKimlik, vari);
            }
        }
        public static List<AramaTalebi> ara(params Predicate<AramaTalebi>[] kosullar)
        {
            return veriTabani.AramaTalebiCizelgesi.ara(kosullar);
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
            veriTabani.AramaTalebiCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "AramaTalebi";
        }


        public override string _turkceAdi()
        {
            return "Arama Talebi";
        }
        public override string _birincilAnahtarAdi()
        {
            return "aramaTalebiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.aramaTalebiKimlik;
        }


        #endregion


    }
}

