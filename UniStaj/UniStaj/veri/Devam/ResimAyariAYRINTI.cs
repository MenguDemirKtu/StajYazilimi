using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class ResimAyariAYRINTI : Bilesen
    {

        public ResimAyariAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ResimAyariAYRINTI> bilesenler = ResimAyariAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ResimAyariAYRINTI> bilesenler = ResimAyariAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<ResimAyariAYRINTI> bilesenler = ResimAyariAYRINTI.ara();
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
            return ilgiliCizelge.ToString();
        }


        public static ResimAyariAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<ResimAyariAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ResimAyariAYRINTI sonuc = new ResimAyariAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ResimAyariAYRINTIs.FirstOrDefaultAsync(p => p.resimAyariKimlik == kimlik && p.varmi == true);
            }
        }


        public static ResimAyariAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ResimAyariAYRINTI sonuc = new ResimAyariAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ResimAyariAYRINTICizelgesi.tekliCek(kimlik, vari) ?? new ResimAyariAYRINTI();
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

        public static List<ResimAyariAYRINTI> ara(params Predicate<ResimAyariAYRINTI>[] kosullar)
        {
            return veriTabani.ResimAyariAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ResimAyariAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "ResimAyariAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "resimAyariKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.resimAyariKimlik;
        }


        #endregion


    }
}

