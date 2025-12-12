using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class SozlesmeAYRINTI : Bilesen
    {

        public SozlesmeAYRINTI()
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
            uyariVerInt32(i_sozlesmeTuruKimlik, ".", dilKimlik);
            uyariVerString(SozlesmeTuruAdi, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return baslik.ToString();
        }


        public static SozlesmeAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SozlesmeAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SozlesmeAYRINTI sonuc = new SozlesmeAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SozlesmeAYRINTIs.FirstOrDefaultAsync(p => p.sozlesmekimlik == kimlik && p.varmi == true);
            }
        }


        public static SozlesmeAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SozlesmeAYRINTI sonuc = new SozlesmeAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SozlesmeAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected SozlesmeAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SozlesmeAYRINTICizelgesi.tekliCek(sozlesmekimlik, vari);
            }
        }
        public static List<SozlesmeAYRINTI> ara(params Predicate<SozlesmeAYRINTI>[] kosullar)
        {
            return veriTabani.SozlesmeAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SozlesmeAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SozlesmeAYRINTI";
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

