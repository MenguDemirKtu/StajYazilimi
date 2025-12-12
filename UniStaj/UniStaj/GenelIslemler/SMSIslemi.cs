using Microsoft.EntityFrameworkCore; // 
using Newtonsoft.Json;
using System.Text;
using UniStaj.veri;


namespace UniStaj.GenelIslemler
{
    public class SMSIslemi
    {
        //private static string NetGSMXMLPost(string PostAddress, string xmlData)
        //{
        //    WebClient wUpload = new WebClient();
        //    HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
        //    Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
        //    Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
        //    string sWebPage = new string(sReturnChars);
        //    return sWebPage;
        //}

        private static async Task<string> netGsm(string metin, string tel, string baslik, string kadi, string sifre)
        {
            var data = new
            {
                msgheader = baslik,
                messages = new[]         {
               new { msg = metin , no =  tel }           },
                encoding = "TR",
                iysfilter = "",
                partnercode = ""
            };

            string url = "https://api.netgsm.com.tr/sms/rest/v2/send";
            string username = kadi;
            string password = sifre;

            using (HttpClient client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, jsonContent);
                    string responseString = await response.Content.ReadAsStringAsync();
                    return responseString;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        private static async Task<string> netGSMGonder(string kAdi, string sifre, string metin, string kime, string baslik)
        {
            return await netGsm(metin, kime, baslik, kAdi, sifre);
            //string ss = "";
            //ss += "<?xml version='1.0' encoding='UTF-8'?>";
            //ss += "<mainbody>";
            //ss += "<header>";
            //ss += "<company dil='TR'>Netgsm</company>";
            //ss += "<usercode>" + kAdi + "</usercode>";
            //ss += "<password>" + sifre + "</password>";
            //ss += "<type>1:n</type>";
            //ss += "<msgheader>" + kAdi + "</msgheader>";
            //ss += "<appkey></appkey>";
            //ss += "</header>";
            //ss += "<body>";
            //ss += "<msg>";
            //ss += "<![CDATA[" + metin + "]]>";
            //ss += "</msg>";
            //ss += "<no>" + kime + " </no>";
            //ss += "</body>  ";
            //ss += "</mainbody>";
            //return NetGSMXMLPost("https://api.netgsm.com.tr/sms/send/xml", ss);
        }


        /// <summary>
        /// SMS Gönderim İşlemi, yazılım ayarında hangi firma tanımlı ise ona göre gönderir.
        /// </summary>
        /// <param name="kAdi"></param>
        /// <param name="sifre"></param>
        /// <param name="tel"></param>
        /// <param name="metin"></param>
        public static async Task<string> smsGonder(string tel, string metin)
        {
            string sonuc = "";
            if (Genel.yazilimAyari.i_smsGonderimSitesiKimlik == (int)enumref_SmsGonderimSitesi.NETGSM)
            {
                sonuc = await netGSMGonder(Genel.yazilimAyari.smsKullaniciAdi ?? "", Genel.yazilimAyari.smsSifre ?? "", metin, tel, Genel.yazilimAyari.smsBaslik ?? "");
            }
            return sonuc;
        }

        public async static Task<string> smsGonder(veri.Varlik vari, string tel, string metin)
        {
            string sonuc = "";
            if (Genel.yazilimAyari.i_smsGonderimSitesiKimlik == (int)enumref_SmsGonderimSitesi.NETGSM)
            {
                sonuc = await netGSMGonder(Genel.yazilimAyari.smsKullaniciAdi ?? "", Genel.yazilimAyari.smsSifre ?? "", metin, tel, Genel.yazilimAyari.smsBaslik ?? "");
            }
            GonderilenSMS sms = new GonderilenSMS();
            sms.tarih = DateTime.Now;
            sms.telefon = tel;
            sms.metin = metin;
            sms.tcKimlikNo = "";
            sms.e_gonderildimi = true;
            sms.varmi = true;
            sms.donusKodu = sonuc;
            sms.bicimlendir(vari);
            await veriTabani.GonderilenSMSCizelgesi.kaydetKos(sms, vari, false);
            return sonuc;
        }

        public async static Task<bool> dogrulamaKodu(veri.Varlik vari, Kullanici kullaniciBilgi)
        {
            int tur = (int)enumref_EPostaTuru.Cift_Yonlu_Dogrulama;
            SmsKalibiAYRINTI? kalip = await vari.SmsKalibiAYRINTIs.FirstOrDefaultAsync(p => p.i_epostaturukimlik == tur && p.varmi == true && p.e_gecerlimi == true);
            if (kalip == null)
                return false;

            if (kullaniciBilgi == null) return false;
            if (kullaniciBilgi.telefon == null) return false;


            string metin = kalip.kalip;

            metin = metin.Replace("{kod}", kullaniciBilgi.ciftOnayKodu);
            metin = metin.Replace("{ad}", kullaniciBilgi.gercekAdi);
            string donusKodu = await SMSIslemi.smsGonder(kullaniciBilgi.telefon, metin);

            GonderilenSMS sms = new GonderilenSMS();
            sms.tarih = DateTime.Now;
            sms.telefon = kullaniciBilgi.telefon;
            sms.metin = metin;
            sms.tcKimlikNo = kullaniciBilgi.tcKimlikNo;
            sms.e_gonderildimi = true;
            sms.varmi = true;
            sms.donusKodu = donusKodu;
            sms.bicimlendir(vari);
            await veriTabani.GonderilenSMSCizelgesi.kaydetKos(sms, vari, false);
            return true;


        }


        public async static Task<bool> sifreAnimsatma(veri.Varlik vari, KullaniciAYRINTI? kullaniciBilgi, string tel)
        {
            if (kullaniciBilgi == null)
                return false;
            int tur = (int)enumref_EPostaTuru.Sifremi_Unuttum;
            var kalip = await vari.SmsKalibiAYRINTIs.FirstOrDefaultAsync(p => p.i_epostaturukimlik == tur && p.varmi == true && p.e_gecerlimi == true);
            if (kalip == null)
                return false;

            if (kullaniciBilgi.sifre == null)
                return false;

            var ayar = Genel.yazilimAyari;
            string ifade = kalip.kalip;
            ifade = ifade.Replace("{ad}", kullaniciBilgi.gercekAdi);
            ifade = ifade.Replace("{kullaniciAdi}", kullaniciBilgi.kullaniciAdi);
            ifade = ifade.Replace("{sifre}", GenelIslemler.GuvenlikIslemi.sifreCoz(kullaniciBilgi.sifre));
            string donusKodu = await smsGonder(tel, ifade);

            GonderilenSMS sms = new GonderilenSMS();
            sms.tarih = DateTime.Now;
            sms.telefon = tel;
            sms.metin = ifade;
            sms.tcKimlikNo = kullaniciBilgi.tcKimlikNo;
            sms.e_gonderildimi = true;
            sms.varmi = true;
            sms.donusKodu = donusKodu;
            sms.bicimlendir(vari);
            await veriTabani.GonderilenSMSCizelgesi.kaydetKos(sms, vari, false);
            return true;

        }
    }
}
