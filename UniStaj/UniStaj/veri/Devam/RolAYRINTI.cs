using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class RolAYRINTI : Bilesen
    {

        public RolAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<RolAYRINTI> bilesenler = RolAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<RolAYRINTI> bilesenler = RolAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<RolAYRINTI> bilesenler = RolAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerBool(e_gecerlimi, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return rolAdi.ToString();
        }


        public static RolAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<RolAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                RolAYRINTI sonuc = new RolAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.RolAYRINTIs.FirstOrDefaultAsync(p => p.rolKimlik == kimlik && p.varmi == true);
            }
        }


        public static RolAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                RolAYRINTI sonuc = new RolAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.RolAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected RolAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.RolAYRINTICizelgesi.tekliCek(rolKimlik, vari);
            }
        }
        public static List<RolAYRINTI> ara(params Predicate<RolAYRINTI>[] kosullar)
        {
            return veriTabani.RolAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "RolAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "RolAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "rolKimlik";
        }


        public override long _birincilAnahtar()
        {
            return rolKimlik;
        }


        #endregion


    }
}

