using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class SosyalMedyaAYRINTI : Bilesen
    {

        public SosyalMedyaAYRINTI()
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
            return bossaDoldur(sosyamMedyaAdi);
        }


        public static SosyalMedyaAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SosyalMedyaAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SosyalMedyaAYRINTI sonuc = new SosyalMedyaAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SosyalMedyaAYRINTIs.FirstOrDefaultAsync(p => p.sosyalMedyakimlik == kimlik && p.varmi == true);
            }
        }


        public static SosyalMedyaAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SosyalMedyaAYRINTI sonuc = new SosyalMedyaAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SosyalMedyaAYRINTICizelgesi.tekliCek(kimlik, vari) ?? new SosyalMedyaAYRINTI();
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

        public static List<SosyalMedyaAYRINTI> ara(params Predicate<SosyalMedyaAYRINTI>[] kosullar)
        {
            return veriTabani.SosyalMedyaAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SosyalMedyaAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SosyalMedya";
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

