using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class SmsKalibiAYRINTI : Bilesen
    {

        public SmsKalibiAYRINTI()
        {
            _varSayilan();
        }

        public static List<SelectListItem> doldur(Yonetici? kime)
        {
            List<SmsKalibiAYRINTI> bilesenler = SmsKalibiAYRINTI.ara();
            return doldur2(bilesenler);
        }

        public static List<SelectListItem> doldur()
        {
            List<SmsKalibiAYRINTI> bilesenler = SmsKalibiAYRINTI.ara();
            return doldur2(bilesenler);
        }
        public static List<SelectListItem> doldur(int[] secilenler)
        {
            List<SmsKalibiAYRINTI> bilesenler = SmsKalibiAYRINTI.ara();
            return doldur2(bilesenler, secilenler);
        }

        public void bicimlendir(veri.Varlik vari)
        {

        }

        public override void _icDenetim(int dilKimlik, veri.Varlik vari)
        {
            uyariVerString(baslik, "baslik", dilKimlik);
            uyariVerInt32(i_epostaturukimlik, "ePostaTuru", dilKimlik);
            uyariVerString(kalip, "kalip", dilKimlik);
            uyariVerBool(e_gecerlimi, "gecerlimi", dilKimlik);
        }


        public override string _tanimi()
        {
            return baslik.ToString();
        }


        public static SmsKalibiAYRINTI olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SmsKalibiAYRINTI?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SmsKalibiAYRINTI sonuc = new SmsKalibiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SmsKalibiAYRINTIs.FirstOrDefaultAsync(p => p.smsKalibikimlik == kimlik && p.varmi == true);
            }
        }


        public static SmsKalibiAYRINTI olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SmsKalibiAYRINTI sonuc = new SmsKalibiAYRINTI();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SmsKalibiAYRINTICizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        protected SmsKalibiAYRINTI cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SmsKalibiAYRINTICizelgesi.tekliCek(smsKalibikimlik, vari);
            }
        }
        public static List<SmsKalibiAYRINTI> ara(params Predicate<SmsKalibiAYRINTI>[] kosullar)
        {
            return veriTabani.SmsKalibiAYRINTICizelgesi.ara(kosullar);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SmsKalibiAYRINTI";
        }


        public override string _turkceAdi()
        {
            return "SMS Kalıbı";
        }
        public override string _birincilAnahtarAdi()
        {
            return "smsKalibikimlik";
        }


        public override long _birincilAnahtar()
        {
            return smsKalibikimlik;
        }


        #endregion


    }
}

