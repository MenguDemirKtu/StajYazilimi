using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class TopluEPostaAlicisi : Bilesen
    {

        public TopluEPostaAlicisi()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<TopluEPostaAlicisiAYRINTI> bilesenler = TopluEPostaAlicisiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<TopluEPostaAlicisiAYRINTI> bilesenler = TopluEPostaAlicisiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<TopluEPostaAlicisiAYRINTI> bilesenler = TopluEPostaAlicisiAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_topluSMSGonderimKimlik, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return i_topluSMSGonderimKimlik.ToString();
        }


        public static TopluEPostaAlicisi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<TopluEPostaAlicisi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                TopluEPostaAlicisi sonuc = new TopluEPostaAlicisi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.TopluEPostaAlicisis.FirstOrDefaultAsync(p => p.topluEPostaAlicisiKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.TopluEPostaAlicisiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.TopluEPostaAlicisiCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static TopluEPostaAlicisi olustur(Varlik vari, object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                TopluEPostaAlicisi sonuc = new TopluEPostaAlicisi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.TopluEPostaAlicisiCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        public TopluEPostaAlicisiAYRINTI _ayrintisi()
        {
            TopluEPostaAlicisiAYRINTI sonuc = TopluEPostaAlicisiAYRINTI.olustur(this.topluEPostaAlicisiKimlik);
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
            veriTabani.TopluEPostaAlicisiCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected TopluEPostaAlicisi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.TopluEPostaAlicisiCizelgesi.tekliCek(this.topluEPostaAlicisiKimlik, vari);
            }
        }
        public static List<TopluEPostaAlicisi> ara(params Predicate<TopluEPostaAlicisi>[] kosullar)
        {
            return veriTabani.TopluEPostaAlicisiCizelgesi.ara(kosullar);
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
            veriTabani.TopluEPostaAlicisiCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "TopluEPostaAlicisi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "topluEPostaAlicisiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.topluEPostaAlicisiKimlik;
        }


        #endregion


    }
}

