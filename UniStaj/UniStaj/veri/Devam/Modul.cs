using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class Modul : Bilesen
    {

        public Modul()
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


        public static Modul olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<Modul?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Modul sonuc = new Modul();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Moduls.FirstOrDefaultAsync(p => p.modulKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.ModulCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.ModulCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static Modul olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                Modul sonuc = new Modul();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.ModulCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        public ModulAYRINTI _ayrintisi()
        {
            ModulAYRINTI sonuc = ModulAYRINTI.olustur(modulKimlik);
            return sonuc;
        }

        public void kaydet(params bool[] yedeklensinmi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kaydet(vari, yedeklensinmi);
            }
        }
        public void kaydet(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            if (varmi == null)
                varmi = true;
            bicimlendir(vari);
            veriTabani.ModulCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected Modul cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.ModulCizelgesi.tekliCek(modulKimlik, vari);
            }
        }
        public static List<Modul> ara(params Predicate<Modul>[] kosullar)
        {
            return veriTabani.ModulCizelgesi.ara(kosullar);
        }
        public void sil()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                sil(vari);
            }
        }
        public void sil(veri.Varlik vari)
        {
            veriTabani.ModulCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Modul";
        }


        public override string _turkceAdi()
        {
            return "Modül";
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

