using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class OturumAcmaAYRINTI : Bilesen
    {

        public OturumAcmaAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<OturumAcmaAYRINTI> bilesenler = OturumAcmaAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<OturumAcmaAYRINTI> bilesenler = OturumAcmaAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<OturumAcmaAYRINTI> bilesenler = OturumAcmaAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_kullaniciKimlik, ".", dilKimlik);
            uyariVerString(kullaniciAdi, ".", dilKimlik);
            uyariVerTarih(tarih, ".", dilKimlik);
            uyariVerInt32(gunlukSayi, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return i_kullaniciKimlik.ToString();
        }


        public static OturumAcmaAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<OturumAcmaAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                OturumAcmaAYRINTI sonuc = new OturumAcmaAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.OturumAcmaAYRINTIs.FirstOrDefaultAsync(p => p.oturumAcmaKimlik == kimlik && p.varmi == true);
            }
        }


        public static OturumAcmaAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                OturumAcmaAYRINTI sonuc = new OturumAcmaAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.OturumAcmaAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected OturumAcmaAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.OturumAcmaAYRINTICizelgesi.tekliCek(this.oturumAcmaKimlik, vari);
            }
        }
        public static List<OturumAcmaAYRINTI> ara(params Predicate<OturumAcmaAYRINTI>[] kosullar)
        {
            return veriTabani.OturumAcmaAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "OturumAcmaAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "OturumAcmaAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "oturumAcmaKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.oturumAcmaKimlik;
        }


        #endregion


    }
}

