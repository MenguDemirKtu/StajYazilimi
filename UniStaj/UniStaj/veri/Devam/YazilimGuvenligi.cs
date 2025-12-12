using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class YazilimGuvenligi : Bilesen
    {

        public YazilimGuvenligi()
        {
            _varSayilan();
            baslik = "";
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<YazilimGuvenligiAYRINTI> bilesenler = YazilimGuvenligiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<YazilimGuvenligiAYRINTI> bilesenler = YazilimGuvenligiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<YazilimGuvenligiAYRINTI> bilesenler = YazilimGuvenligiAYRINTI.ara();
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
            if (sonKullanimTarihi != null)
                return sonKullanimTarihi.Value.ToString();
            else
                return "";
        }


        public static YazilimGuvenligi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<YazilimGuvenligi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                YazilimGuvenligi sonuc = new YazilimGuvenligi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.YazilimGuvenligis.FirstOrDefaultAsync(p => p.yazilimGuvenligikimlik == kimlik);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            bicimlendir(vari);
            await veriTabani.YazilimGuvenligiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            await veriTabani.YazilimGuvenligiCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static YazilimGuvenligi olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                YazilimGuvenligi sonuc = new YazilimGuvenligi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.YazilimGuvenligiCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
            e_gecerliMi = true;
            baslik = "";
        }

        public YazilimGuvenligiAYRINTI _ayrintisi()
        {
            YazilimGuvenligiAYRINTI sonuc = YazilimGuvenligiAYRINTI.olustur(yazilimGuvenligikimlik);
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
            veriTabani.YazilimGuvenligiCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected YazilimGuvenligi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.YazilimGuvenligiCizelgesi.tekliCek(yazilimGuvenligikimlik, vari);
            }
        }
        public static List<YazilimGuvenligi> ara(params Predicate<YazilimGuvenligi>[] kosullar)
        {
            return veriTabani.YazilimGuvenligiCizelgesi.ara(kosullar);
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
            veriTabani.YazilimGuvenligiCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "YazilimGuvenligi";
        }


        public override string _turkceAdi()
        {
            return "Yazılım Güvenliği";
        }
        public override string _birincilAnahtarAdi()
        {
            return "yazilimGuvenligikimlik";
        }


        public override long _birincilAnahtar()
        {
            return yazilimGuvenligikimlik;
        }


        #endregion


    }
}

