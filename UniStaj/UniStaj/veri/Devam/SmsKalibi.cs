using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UniStaj.veri
{
    public partial class SmsKalibi : Bilesen
    {

        public SmsKalibi()
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


        public static SmsKalibi olustur(object deger)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return olustur(vari, deger);
            }
        }


        public async static Task<SmsKalibi?> olusturKos(Varlik vari, object deger)
        {
            Int64 kimlik = Convert.ToInt64(deger);
            if (kimlik <= 0)
            {
                SmsKalibi sonuc = new SmsKalibi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return await vari.SmsKalibis.FirstOrDefaultAsync(p => p.smsKalibikimlik == kimlik && p.varmi == true);
            }
        }


        public async Task kaydetKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = true;
            bicimlendir(vari);
            await veriTabani.SmsKalibiCizelgesi.kaydetKos(this, vari, yedeklensinmi);
        }
        public async Task silKos(veri.Varlik vari, params bool[] yedeklensinmi)
        {
            varmi = false;
            await veriTabani.SmsKalibiCizelgesi.silKos(this, vari, yedeklensinmi);
        }


        public static SmsKalibi olustur(Varlik vari, object deger)
        {
            Int32 kimlik = Convert.ToInt32(deger);
            if (kimlik <= 0)
            {
                SmsKalibi sonuc = new SmsKalibi();
                sonuc._varSayilan();
                return sonuc;
            }
            else
            {
                return veriTabani.SmsKalibiCizelgesi.tekliCek(kimlik, vari);
            }
        }
        public override void _kontrolEt(int dilKimlik, veri.Varlik vari)
        {
            _icDenetim(dilKimlik, vari);
        }


        public override void _varSayilan()
        {
        }

        public SmsKalibiAYRINTI _ayrintisi()
        {
            SmsKalibiAYRINTI sonuc = SmsKalibiAYRINTI.olustur(this.smsKalibikimlik);
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
            if (this.varmi == null)
                this.varmi = true;
            this.bicimlendir(vari);
            veriTabani.SmsKalibiCizelgesi.kaydet(this, vari, yedeklensinmi);
        }
        protected SmsKalibi cek()
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                return veriTabani.SmsKalibiCizelgesi.tekliCek(this.smsKalibikimlik, vari);
            }
        }
        public static List<SmsKalibi> ara(params Predicate<SmsKalibi>[] kosullar)
        {
            return veriTabani.SmsKalibiCizelgesi.ara(kosullar);
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
            veriTabani.SmsKalibiCizelgesi.sil(this, vari);
        }


        #region ozluk


        public override string _cizelgeAdi()
        {
            return "SmsKalibi";
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
            return this.smsKalibikimlik;
        }


        #endregion


    }
}

