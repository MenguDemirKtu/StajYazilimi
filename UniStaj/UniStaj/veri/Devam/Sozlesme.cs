using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class Sozlesme : Bilesen
    {

        public Sozlesme()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SozlesmeAYRINTI> bilesenler = SozlesmeAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<SozlesmeAYRINTI> bilesenler = SozlesmeAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<SozlesmeAYRINTI> bilesenler = SozlesmeAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_sozlesmeTuruKimlik, "Sözleşme Türü", dilKimlik);
        }


        public override string _tanimi()
        {
            return baslik.ToString();
        }


        public static Sozlesme olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<Sozlesme?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Sozlesme sonuc = new Sozlesme();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Sozlesmes.FirstOrDefaultAsync(p => p.sozlesmekimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.SozlesmeCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SozlesmeCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static Sozlesme olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                Sozlesme sonuc = new Sozlesme();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SozlesmeCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            e_gecerliMi = true;
            varmi = true;
        }

        public SozlesmeAYRINTI _ayrintisi()
        {
            SozlesmeAYRINTI sonuc = SozlesmeAYRINTI.olustur(sozlesmekimlik);
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
            veriTabani.SozlesmeCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected Sozlesme cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SozlesmeCizelgesi.tekliCek(sozlesmekimlik, vari);
            }
        }
        public static List<Sozlesme> ara(params Predicate<Sozlesme>[] kosullar)
        {
            return veriTabani.SozlesmeCizelgesi.ara(kosullar);
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
            veriTabani.SozlesmeCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Sozlesme";
        }


        public override string _turkceAdi()
        {
            return "Sözleşme";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sozlesmekimlik";
        }


        public override long _birincilAnahtar()
        {
            return sozlesmekimlik;
        }


        #endregion


    }
}

