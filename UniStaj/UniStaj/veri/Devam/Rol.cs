using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class Rol : Bilesen
    {

        public Rol()
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
            if (string.IsNullOrEmpty(kodu))
                kodu = Guid.NewGuid().ToString();
        }


        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerBool(e_gecerlimi, "Geçerli mi", dilKimlik);
        }


        public override string _tanimi()
        {
            if (string.IsNullOrEmpty(rolAdi))
                return "";
            else
                return rolAdi.ToString();
        }


        public static Rol? olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<Rol?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                Rol sonuc = new Rol();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.Rols.FirstOrDefaultAsync(p => p.rolKimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.RolCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.RolCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static Rol? olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                Rol sonuc = new Rol();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.RolCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            e_varsayilanmi = false;
            e_rolIslemiIcinmi = false;
            i_rolIslemiKimlik = 0;
        }

        public RolAYRINTI _ayrintisi()
        {
            RolAYRINTI sonuc = RolAYRINTI.olustur(rolKimlik);
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
            bicimlendir(vari);
            veriTabani.RolCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        public static List<Rol> ara(params Predicate<Rol>[] kosullar)
        {
            return veriTabani.RolCizelgesi.ara(kosullar);
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
            veriTabani.RolCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "Rol";
        }


        public override string _turkceAdi()
        {
            return "Rol";
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

