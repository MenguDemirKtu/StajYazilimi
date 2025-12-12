using System.Net;
using System.Net.Mail;
using UniStaj.veri;

namespace UniStaj
{
    public class EPostaIslemi
    {


        public static void kullancininHataBildirimineYanitVerildi(HataYazismasi yazisma, HataBildirimi bildir, Kullanici kime)
        {
            try
            {


                //veri.varlik vari = new varlik();
                //int tur = (int)enumref_EPostaTuru.Hata_Bildirimine_Yetkili_Tarafından_Yanıt_Verildi;

                //var ayari = Genel.yazilimAyari;
                //var kalip = vari.EPostaKalibis.FirstOrDefault(p => p.i_ePostaTuruKimlik == tur && p.varmi == true && p.e_gecerlimi == true);
                //if (kalip == null)
                //    return;

                ////var ayar = vari.YazilimAyariAYRINTIs.FirstOrDefault(p => p.e_gecerlimi == true);
                ////if (ayar == null)
                ////    return;

                //var mevcut = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.kullaniciKimlik == kime.kullaniciKimlik);
                //string ifade = kalip.kalip;

                //// String ifade = "{kullanici}{tarih}{baslik}{adres}";

                //ifade = ifade.Replace("{tarih}", yazisma.tarih.Value.ToString());
                //ifade = ifade.Replace("{baslik}", bildir.baslik);
                //ifade = ifade.Replace("{kullanici}", kime.gercekAdi);
                //ifade = ifade.Replace("{adres}", ayari.yazilimAdresi + "HataBildirimlerim/Yazisma/?id=" + bildir.kodu);


                ////bool gittimi = postaGonderveKaydet(ayari, enumref_EPostaTuru.Hata_Bildirimine_Yetkili_Tarafından_Yanıt_Verildi, mevcut.ePostaAdresi, kalip.kalipBasligi, ifade, kime.gercekAdi);

            }
            catch
            {

            }
        }



        public static void KullanciHataBildirimindeBulundu(List<FederasyonBildirimGondermeAyariAYRINTI> alicilar, HataBildirimiAYRINTI hatasi)
        {
            //try
            //{
            //    veri.varlik vari = new varlik();
            //    int tur = (int)enumref_EPostaTuru.Hata_Bildirimi;

            //    var ayari = Genel.yazilimAyari;
            //    var kalip = vari.EPostaKalibis.FirstOrDefault(p => p.i_ePostaTuruKimlik == tur && p.varmi == true && p.e_gecerlimi == true);
            //    if (kalip == null)
            //        return;

            //    var ayar = vari.YazilimAyariAYRINTIs.FirstOrDefault(p => p.e_gecerlimi == true);
            //    if (ayar == null)
            //        return;

            //    //var mevcut = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.kullaniciKimlik == insan.i_kullaniciKimlik);

            //    string ifade = kalip.kalip;

            //    for (int i = 0; i < alicilar.Count; i++)
            //    {
            //        var insan = alicilar[i];
            //        if (string.IsNullOrEmpty(insan.ePostaAdresi))
            //            continue;
            //        ifade = ifade.Replace("{tarih}", hatasi.tarih.Value.ToString());
            //        ifade = ifade.Replace("{hataMetni}", hatasi.hataAciklamasi);
            //        ifade = ifade.Replace("{kullaniciAdi}", hatasi.kullaniciAdi);
            //        ifade = ifade.Replace("{ad}", hatasi.gercekAdi);
            //        ifade = ifade.Replace("{adres}", ayar.yazilimAdresi + "HataBildirimlerim/Yazisma/?id=" + hatasi.kodu);
            //        //bool gittimi = postaGonderveKaydet(ayar, enumref_EPostaTuru.Hata_Bildirimi, insan.ePostaAdresi, kalip.kalipBasligi, ifade, insan.gercekAdi);
            //    }
            //}
            //catch
            //{
            //}
        }


        public static async Task oturumSuresiAsildi(veri.Varlik vari, Kullanici kullaniciBilgi, int sayi)
        {
            try
            {
                int tur = (int)enumref_EPostaTuru.Oturum_Sinir_Asimi;

                var kalip = vari.EPostaKalibis.FirstOrDefault(p => p.i_ePostaTuruKimlik == tur && p.varmi == true && p.e_gecerlimi == true);
                if (kalip == null)
                    return;

                var ayar = vari.YazilimAyariAYRINTIs.FirstOrDefault(p => p.e_gecerlimi == true);
                if (ayar == null)
                    return;

                var mevcut = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.ePostaAdresi == kullaniciBilgi.ePostaAdresi);

                if (kalip == null)
                    return;

                if (kalip.kalip == null) return;

                string metin = kalip.kalip;
                metin = metin.Replace("{kullaniciAdi}", kullaniciBilgi.kullaniciAdi);
                metin = metin.Replace("{tarih}", DateTime.Now.ToString());
                metin = metin.Replace("{sayi}", DateTime.Now.ToString());
                if (Genel.yazilimAyari.guvenlikIhlalEPosta != null && kalip.kalipBasligi != null)
                {
                    bool gittimi = await postaGonderveKaydetKos(vari, enumref_EPostaTuru.Oturum_Sinir_Asimi, Genel.yazilimAyari.guvenlikIhlalEPosta, kalip.kalipBasligi, metin, "Sistem Yetkilisi");
                }
            }
            catch
            {
            }
        }


        public static async Task dogrulamaKodu(veri.Varlik vari, Kullanici kullaniciBilgi)
        {
            try
            {
                int tur = (int)enumref_EPostaTuru.Cift_Yonlu_Dogrulama;

                var kalip = vari.EPostaKalibis.FirstOrDefault(p => p.i_ePostaTuruKimlik == tur && p.varmi == true && p.e_gecerlimi == true);
                if (kalip == null)
                    return;
                if (kalip.kalip == null) return;

                var ayar = vari.YazilimAyariAYRINTIs.FirstOrDefault(p => p.e_gecerlimi == true);
                if (ayar == null)
                    return;

                var mevcut = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.ePostaAdresi == kullaniciBilgi.ePostaAdresi);

                string metin = kalip.kalip;
                metin = metin.Replace("{kod}", kullaniciBilgi.ciftOnayKodu);
                metin = metin.Replace("{ad}", kullaniciBilgi.gercekAdi);

                if (kalip.kalipBasligi == null || kullaniciBilgi.gercekAdi == null || kullaniciBilgi.ePostaAdresi == null)
                    return;
                bool gittimi = await postaGonderveKaydetKos(vari, enumref_EPostaTuru.Cift_Yonlu_Dogrulama, kullaniciBilgi.ePostaAdresi, kalip.kalipBasligi, metin, kullaniciBilgi.gercekAdi);

            }
            catch
            {
            }
        }


        public static void SifreHatirlatma(veri.Varlik vari, Kullanici kullaniciBilgi)
        {
            try
            {
                int tur = (int)enumref_EPostaTuru.Sifremi_Unuttum;

                var kalip = vari.EPostaKalibis.FirstOrDefault(p => p.i_ePostaTuruKimlik == tur && p.varmi == true && p.e_gecerlimi == true);
                if (kalip == null)
                    return;

                var ayar = vari.YazilimAyariAYRINTIs.FirstOrDefault(p => p.e_gecerlimi == true);
                if (ayar == null)
                    return;

                var mevcut = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.ePostaAdresi == kullaniciBilgi.ePostaAdresi);
                if (kalip.kalip == null)
                    return;
                if (mevcut == null || kullaniciBilgi == null)
                    return;
                string ifade = kalip.kalip;
                ifade = ifade.Replace("{ad}", mevcut.gercekAdi);
                ifade = ifade.Replace("{kullaniciAdi}", mevcut.kullaniciAdi);
                ifade = ifade.Replace("{sifre}", mevcut.sifre);

                if (kullaniciBilgi.ePostaAdresi == null || kalip.kalipBasligi == null || kullaniciBilgi.gercekAdi == null)
                    return;
                bool gittimi = postaGonderveKaydet(ayar, enumref_EPostaTuru.Sifremi_Unuttum, kullaniciBilgi.ePostaAdresi, kalip.kalipBasligi, ifade, kullaniciBilgi.gercekAdi);

            }
            catch
            {
            }
        }

        public static bool postaGonder2(YazilimAyariAYRINTI ayar, string kime, string konu, string metin)
        {
            try
            {
                if (string.IsNullOrEmpty(kime))
                    return false;
                if (ayar.hesapEPostaAdresi == null || ayar.hesapEPostaHost == null)
                    return false;

                kime = kime.ToLower();
                kime = kime.Replace('ı', 'i');
                MailAddress to = new MailAddress(kime);
                MailAddress from = new MailAddress(ayar.hesapEPostaAdresi);
                MailMessage email = new MailMessage(from, to);
                email.Subject = konu;
                email.Body = metin;
                SmtpClient smtp = new SmtpClient();
                email.IsBodyHtml = true;
                smtp.Host = ayar.hesapEPostaHost;
                smtp.Port = Convert.ToInt16(ayar.hesapEPostaPort);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ayar.hesapEPostaAdresi, ayar.hesapEPostaSifresi);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(email);
                return true;
            }
            catch (Exception hata)
            {
                string nedir = hata.ToString();
                return false;
            }
        }
        public async static Task<bool> postaGonderveKaydetKos(veri.Varlik vari, enumref_EPostaTuru tur, string kime, string konu, string metin, string aliciAdi)
        {
            try
            {
                if (string.IsNullOrEmpty(kime))
                    return false;
                YazilimAyariAYRINTI ayar = Genel.yazilimAyari;
                kime = kime.ToLower();
                kime = kime.Replace('ı', 'i');
                MailAddress to = new MailAddress(kime);
                MailAddress from = new MailAddress(ayar.hesapEPostaAdresi ?? "");
                MailMessage email = new MailMessage(from, to);
                email.Subject = konu;
                email.Body = metin;
                SmtpClient smtp = new SmtpClient();
                email.IsBodyHtml = true;
                smtp.Host = ayar.hesapEPostaHost ?? "";
                smtp.Port = Convert.ToInt16(ayar.hesapEPostaPort);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ayar.hesapEPostaAdresi, ayar.hesapEPostaSifresi);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(email);

                veri.GonderilenEPosta gonderilen = new GonderilenEPosta();
                gonderilen._varSayilan();
                gonderilen.i_ePostaTuruKimlik = (int)tur;
                gonderilen.gonderimTarihi = DateTime.Now;
                gonderilen.metin = metin;
                gonderilen.adres = kime;
                gonderilen.baslik = konu;
                gonderilen.kisiAdi = aliciAdi;
                await veriTabani.GonderilenEPostaCizelgesi.kaydetKos(gonderilen, vari, false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool postaGonderveKaydet(YazilimAyariAYRINTI ayar, enumref_EPostaTuru tur, string kime, string konu, string metin, string aliciAdi)
        {
            try
            {
                if (string.IsNullOrEmpty(kime))
                    return false;

                if (ayar.hesapEPostaAdresi == null)
                    return false;

                if (ayar.hesapEPostaHost == null)
                    return false;

                if (ayar == null)
                    return false;
                kime = kime.ToLower();
                kime = kime.Replace('ı', 'i');
                MailAddress to = new MailAddress(kime);
                MailAddress from = new MailAddress(ayar.hesapEPostaAdresi);
                MailMessage email = new MailMessage(from, to);
                email.Subject = konu;
                email.Body = metin;
                SmtpClient smtp = new SmtpClient();
                email.IsBodyHtml = true;
                smtp.Host = ayar.hesapEPostaHost;
                smtp.Port = Convert.ToInt16(ayar.hesapEPostaPort);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ayar.hesapEPostaAdresi, ayar.hesapEPostaSifresi);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Send(email);


                veri.GonderilenEPosta gonderilen = new GonderilenEPosta();
                gonderilen._varSayilan();
                gonderilen.i_ePostaTuruKimlik = (int)tur;
                gonderilen.gonderimTarihi = DateTime.Now;
                gonderilen.metin = metin;
                gonderilen.adres = kime;
                gonderilen.baslik = konu;
                gonderilen.kisiAdi = aliciAdi;
                gonderilen.kaydet(false, false);


                return true;
            }
            catch (Exception hata)
            {
                string nedir = hata.ToString();
                return false;
            }
        }

    }
}
