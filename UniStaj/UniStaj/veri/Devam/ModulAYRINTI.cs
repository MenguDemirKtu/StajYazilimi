using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class ModulAYRINTI : Bilesen
    {

        public ModulAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<ModulAYRINTI> bilesenler = ModulAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<ModulAYRINTI> bilesenler = ModulAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<ModulAYRINTI> bilesenler = ModulAYRINTI.ara();
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
            return modulAdi.ToString();
        }


        public static ModulAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<ModulAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                ModulAYRINTI sonuc = new ModulAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.ModulAYRINTIs.FirstOrDefaultAsync(p => p.modulKimlik == kimlik && p.varmi == true);
            }
        }


        public static ModulAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                ModulAYRINTI sonuc = new ModulAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ModulAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected ModulAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ModulAYRINTICizelgesi.tekliCek(modulKimlik, vari);
            }
        }
        public static List<ModulAYRINTI> ara(params Predicate<ModulAYRINTI>[] kosullar)
        {
            return veriTabani.ModulAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "ModulAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "ModulA";
        }
        public override string _birincilAnahtarAdi()
        {
            return "modulKimlik";
        }


        public override long _birincilAnahtar()
        {
            return modulKimlik;
        }


        #endregion


    }
}

