using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class SozlesmeOnayi : Bilesen
    {

        public SozlesmeOnayi()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SozlesmeOnayi> bilesenler = SozlesmeOnayi.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<SozlesmeOnayi> bilesenler = SozlesmeOnayi.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<SozlesmeOnayi> bilesenler = SozlesmeOnayi.ara();
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
            return i_kullaniciKimlik.ToString();
        }


        public static SozlesmeOnayi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SozlesmeOnayi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SozlesmeOnayi sonuc = new SozlesmeOnayi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SozlesmeOnayis.FirstOrDefaultAsync(p => p.sozlesmeOnayiKimlik == kimlik && p.varmi == true);
            }
        }


        public static SozlesmeOnayi olustur(Varlik vari, object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SozlesmeOnayi sonuc = new SozlesmeOnayi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SozlesmeOnayiCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected SozlesmeOnayi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SozlesmeOnayiCizelgesi.tekliCek(sozlesmeOnayiKimlik, vari);
            }
        }
        public static List<SozlesmeOnayi> ara(params Predicate<SozlesmeOnayi>[] kosullar)
        {
            return veriTabani.SozlesmeOnayiCizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SozlesmeOnayi";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sozlesmeOnayiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return sozlesmeOnayiKimlik;
        }


        #endregion


    }
}

