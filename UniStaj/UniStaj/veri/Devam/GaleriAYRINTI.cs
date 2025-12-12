using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class GaleriAYRINTI : Bilesen
    {

        public GaleriAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<GaleriAYRINTI> bilesenler = GaleriAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<GaleriAYRINTI> bilesenler = GaleriAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<GaleriAYRINTI> bilesenler = GaleriAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt64(ilgiliKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return galeriAdi.ToString();
        }


        public static GaleriAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<GaleriAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                GaleriAYRINTI sonuc = new GaleriAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.GaleriAYRINTIs.FirstOrDefaultAsync(p => p.galeriKimlik == kimlik && p.varmi == true);
            }
        }


        public static GaleriAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                GaleriAYRINTI sonuc = new GaleriAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.GaleriAYRINTICizelgesi.tekliCek(kimlik, vari) ?? new GaleriAYRINTI();
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

        public static List<GaleriAYRINTI> ara(params Predicate<GaleriAYRINTI>[] kosullar)
        {
            return veriTabani.GaleriAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "GaleriAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "GaleriAYRIN";
        }
        public override string _birincilAnahtarAdi()
        {
            return "galeriKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.galeriKimlik;
        }


        #endregion


    }
}

