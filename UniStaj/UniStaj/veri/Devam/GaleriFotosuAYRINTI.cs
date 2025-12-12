using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class GaleriFotosuAYRINTI : Bilesen
    {

        public GaleriFotosuAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GaleriFotosuAYRINTI> bilesenler = GaleriFotosuAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<GaleriFotosuAYRINTI> bilesenler = GaleriFotosuAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<GaleriFotosuAYRINTI> bilesenler = GaleriFotosuAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_galeriKimlik, ".", dilKimlik);
            uyariVerInt64(i_fotoKimlik, ".", dilKimlik);
            uyariVerBool(e_gosterimdemi, ".", dilKimlik);
            uyariVerInt32(sirasi, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return i_galeriKimlik.ToString();
        }


        public static GaleriFotosuAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<GaleriFotosuAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                GaleriFotosuAYRINTI sonuc = new GaleriFotosuAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.GaleriFotosuAYRINTIs.FirstOrDefaultAsync(p => p.galeriFotosuKimlik == kimlik && p.varmi == true);
            }
        }


        public static GaleriFotosuAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                GaleriFotosuAYRINTI sonuc = new GaleriFotosuAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.GaleriFotosuAYRINTICizelgesi.tekliCek(kimlik, vari) ?? new GaleriFotosuAYRINTI();
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

        public static List<GaleriFotosuAYRINTI> ara(params Predicate<GaleriFotosuAYRINTI>[] kosullar)
        {
            return veriTabani.GaleriFotosuAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "GaleriFotosuAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "GaleriFotosuAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "galeriFotosuKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.galeriFotosuKimlik;
        }


        #endregion


    }
}

