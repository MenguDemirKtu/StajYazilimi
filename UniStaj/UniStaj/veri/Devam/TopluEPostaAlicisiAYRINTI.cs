using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // 

namespace UniStaj.veri
{
    public partial class TopluEPostaAlicisiAYRINTI : Bilesen
    {

        public TopluEPostaAlicisiAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<TopluEPostaAlicisiAYRINTI> bilesenler = TopluEPostaAlicisiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<TopluEPostaAlicisiAYRINTI> bilesenler = TopluEPostaAlicisiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<TopluEPostaAlicisiAYRINTI> bilesenler = TopluEPostaAlicisiAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerInt32(i_topluSMSGonderimKimlik, ".", dilKimlik);
        }


        public override string _tanimi()
        {
            return i_topluSMSGonderimKimlik.ToString();
        }


        public static TopluEPostaAlicisiAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<TopluEPostaAlicisiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                TopluEPostaAlicisiAYRINTI sonuc = new TopluEPostaAlicisiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.TopluEPostaAlicisiAYRINTIs.FirstOrDefaultAsync(p => p.topluEPostaAlicisiKimlik == kimlik && p.varmi == true);
            }
        }


        public static TopluEPostaAlicisiAYRINTI olustur(Varlik vari, object deger)
        {
            long kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                TopluEPostaAlicisiAYRINTI sonuc = new TopluEPostaAlicisiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.TopluEPostaAlicisiAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected TopluEPostaAlicisiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.TopluEPostaAlicisiAYRINTICizelgesi.tekliCek(this.topluEPostaAlicisiKimlik, vari);
            }
        }
        public static List<TopluEPostaAlicisiAYRINTI> ara(params Predicate<TopluEPostaAlicisiAYRINTI>[] kosullar)
        {
            return veriTabani.TopluEPostaAlicisiAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "TopluEPostaAlicisiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "TopluEPostaAlicisiAYRINTI";
        }
        public override string _birincilAnahtarAdi()
        {
            return "topluEPostaAlicisiKimlik";
        }


        public override long _birincilAnahtar()
        {
            return this.topluEPostaAlicisiKimlik;
        }


        #endregion


    }
}

