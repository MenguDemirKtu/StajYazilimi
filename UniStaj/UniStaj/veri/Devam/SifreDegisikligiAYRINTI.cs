using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class SifreDegisikligiAYRINTI : Bilesen
    {

        public SifreDegisikligiAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SifreDegisikligiAYRINTI> bilesenler = SifreDegisikligiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<SifreDegisikligiAYRINTI> bilesenler = SifreDegisikligiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<SifreDegisikligiAYRINTI> bilesenler = SifreDegisikligiAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerTarih(tarih, ".", dilKimlik);
            uyariVerInt32(i_kullaniciKimlik, ".", dilKimlik);
            uyariVerString(kullaniciAdi, ".", dilKimlik);
            uyariVerString(eskiSifre, ".", dilKimlik);
            uyariVerString(yeniSifre, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return tarih.ToString();
        }


        public static SifreDegisikligiAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SifreDegisikligiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SifreDegisikligiAYRINTI sonuc = new SifreDegisikligiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SifreDegisikligiAYRINTIs.FirstOrDefaultAsync(p => p.sifreDegisikligiKimlik == kimlik && p.varmi == true);
            }
        }


        public static SifreDegisikligiAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SifreDegisikligiAYRINTI sonuc = new SifreDegisikligiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SifreDegisikligiAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected SifreDegisikligiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SifreDegisikligiAYRINTICizelgesi.tekliCek(sifreDegisikligiKimlik, vari);
            }
        }
        public static List<SifreDegisikligiAYRINTI> ara(params Predicate<SifreDegisikligiAYRINTI>[] kosullar)
        {
            return veriTabani.SifreDegisikligiAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SifreDegisikligiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SifreDegisikligiAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "sifreDegisikligiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return sifreDegisikligiKimlik;
        }


        #endregion


    }
}

