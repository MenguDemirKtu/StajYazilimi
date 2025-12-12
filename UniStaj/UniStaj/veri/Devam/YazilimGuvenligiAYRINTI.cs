using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class YazilimGuvenligiAYRINTI : Bilesen
    {

        public YazilimGuvenligiAYRINTI()
        {
            _varSayilan();
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
            return sonKullanimTarihi.ToString();
        }


        public static YazilimGuvenligiAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<YazilimGuvenligiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                YazilimGuvenligiAYRINTI sonuc = new YazilimGuvenligiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.YazilimGuvenligiAYRINTIs.FirstOrDefaultAsync(p => p.yazilimGuvenligikimlik == kimlik);
            }
        }


        public static YazilimGuvenligiAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                YazilimGuvenligiAYRINTI sonuc = new YazilimGuvenligiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.YazilimGuvenligiAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected YazilimGuvenligiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.YazilimGuvenligiAYRINTICizelgesi.tekliCek(yazilimGuvenligikimlik, vari);
            }
        }
        public static List<YazilimGuvenligiAYRINTI> ara(params Predicate<YazilimGuvenligiAYRINTI>[] kosullar)
        {
            return veriTabani.YazilimGuvenligiAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "YazilimGuvenligiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "YazilimGuvenligiAYRINTI";
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

