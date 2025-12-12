using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class SifremiUnuttumAYRINTI : Bilesen
    {

        public SifremiUnuttumAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SifremiUnuttumAYRINTI> bilesenler = SifremiUnuttumAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<SifremiUnuttumAYRINTI> bilesenler = SifremiUnuttumAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<SifremiUnuttumAYRINTI> bilesenler = SifremiUnuttumAYRINTI.ara();
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
            return tcKimlikNo.ToString();
        }


        public static SifremiUnuttumAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SifremiUnuttumAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SifremiUnuttumAYRINTI sonuc = new SifremiUnuttumAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SifremiUnuttumAYRINTIs.FirstOrDefaultAsync(p => p.sifremiUnuttumKimlik == kimlik && p.varmi == true);
            }
        }


        public static SifremiUnuttumAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SifremiUnuttumAYRINTI sonuc = new SifremiUnuttumAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SifremiUnuttumAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected SifremiUnuttumAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SifremiUnuttumAYRINTICizelgesi.tekliCek(this.sifremiUnuttumKimlik, vari);
            }
        }
        public static List<SifremiUnuttumAYRINTI> ara(params Predicate<SifremiUnuttumAYRINTI>[] kosullar)
        {
            return veriTabani.SifremiUnuttumAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SifremiUnuttumAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SifremiUnuttumAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sifremiUnuttumKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.sifremiUnuttumKimlik;
        }


        #endregion


    }
}

