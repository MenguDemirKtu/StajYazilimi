using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class ref_SozlesmeTuru : Bilesen
    {

        public ref_SozlesmeTuru()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ref_SozlesmeTuru> bilesenler = ref_SozlesmeTuru.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ref_SozlesmeTuru> bilesenler = ref_SozlesmeTuru.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<ref_SozlesmeTuru> bilesenler = ref_SozlesmeTuru.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(SozlesmeTuruAdi, "", dilKimlik);
        }


        public override string _tanimi()
        {
            return SozlesmeTuruAdi.ToString();
        }


        public static ref_SozlesmeTuru olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<ref_SozlesmeTuru?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ref_SozlesmeTuru sonuc = new ref_SozlesmeTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ref_SozlesmeTurus.FirstOrDefaultAsync(p => p.SozlesmeTuruKimlik == kimlik);
            }
        }


        public static ref_SozlesmeTuru olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ref_SozlesmeTuru sonuc = new ref_SozlesmeTuru();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ref_SozlesmeTuruCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }
        protected ref_SozlesmeTuru cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ref_SozlesmeTuruCizelgesi.tekliCek(SozlesmeTuruKimlik, vari);
            }
        }
        public static List<ref_SozlesmeTuru> ara(params Predicate<ref_SozlesmeTuru>[] kosullar)
        {
            return veriTabani.ref_SozlesmeTuruCizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ref_SozlesmeTuru";
        }


        public override string _turkceAdi()
        {
            return "";
        }
        public override string _birincilAnahtarAdi()
        {
            return "SozlesmeTuruKimlik";
        }


        public override long _birincilAnahtar()
        {
            return SozlesmeTuruKimlik;
        }


        #endregion


    }
}

