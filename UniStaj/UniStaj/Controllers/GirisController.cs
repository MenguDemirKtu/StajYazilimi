using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // 
using UniStaj.GenelIslemler;
using UniStaj.Models;
using UniStaj.veri;


namespace UniStaj.Controllers
{
    public class GirisController : Sayfa
    {
        public IActionResult Index()
        {
            try
            {
                int dil = 1;
                ViewBag.Sonuc = "";
                GirisModel model = new GirisModel();
                try
                {
                    if (String.IsNullOrEmpty(Request.Query["tur"]) == false)
                    {
                        if (Request.Query["tur"] == "0")
                        {
                            dil = 2;
                            model.yabancimi = true;
                        }
                    }
                }
                catch
                {
                }
                ViewBag.dil = dil;
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
        }
        private void sozlesmeOnaylandiYeniSayfayaGit(int kullaniciKimlik)
        {

        }
        protected JsonResult kaydedildiBildirimi(GirisModel kaydedilecek, string ileti)
        {
            return Json(new
            {
                success = true,
                message = ileti,
                izinVarmi = kaydedilecek.izinVarmi,
                adres = kaydedilecek.yonlendirmeAdresi,
                otuzSaniyeVarmi = false,
                kimlik = 0
            });
        }
        protected JsonResult kaydedildiBildirimi(GirisModel kaydedilecek, string ileti, bool _otuzSaniyeVarmi)
        {
            return Json(new
            {
                success = true,
                message = ileti,
                izinVarmi = kaydedilecek.izinVarmi,
                otuzSaniyeVarmi = _otuzSaniyeVarmi,
                adres = kaydedilecek.yonlendirmeAdresi,
                kimlik = 0
            });
        }

        string kisiAdi = "";
        public int dilAyar()
        {

            int dil = 1;

            try
            {
                dil = Convert.ToInt32(ViewBag.dil);
            }
            catch
            {

            }
            return dil;
        }
        [HttpPost]
        public async Task<ActionResult> izinAl(Models.GirisModel gelen)
        {
            try
            {
                otuzSaniyeVarmi = false;
                kisiAdi = "";
                gelen.kullaniciAdi = gelen.kullaniciAdi.Trim();
                gelen.sifre = gelen.sifre.Trim();
                gelen.yonlendirmeAdresi = await kontrolEt(gelen);
                gelen.izinVarmi = true;
                return kaydedildiBildirimi(gelen, Ayar.sozcuk("Hoş geldiniz ", dilAyar()) + kisiAdi);
            }
            catch (Exception istisna)
            {
                if (otuzSaniyeVarmi)
                {

                }
                else
                {

                }
                gelen.izinVarmi = false;
                return kaydedildiBildirimi(gelen, istisna.Message, otuzSaniyeVarmi);
            }
        }

        private string temizle(string kaynak)
        {

            kaynak = kaynak.Replace("K", "");
            kaynak = kaynak.Replace("H", "");
            kaynak = kaynak.Replace("İ", "");
            kaynak = kaynak.Replace("I", "");
            kaynak = kaynak.Replace("AF", "");
            kaynak = kaynak.Replace("S", "");
            kaynak = kaynak.Replace("A", "");
            return kaynak;
        }


        bool otuzSaniyeVarmi = false;
        private async Task<string> kontrolEt(Models.GirisModel gelen)
        {
            otuzSaniyeVarmi = false;
            string sonuc = "/anasayfa";
            string ePosta = gelen.kullaniciAdi;
            string pasaport = gelen.kullaniciAdi;
            string sifre = gelen.sifre;
            ePosta = temizle(ePosta);
            kayitKonumuBelirle();
            string adres = Request.GetEncodedUrl();
            int yer = adres.IndexOf("localhost");
            Genel.lokalmi = false;
            if (yer != -1)
            {
                Genel.lokalmi = true;
                if (ePosta == null)
                {
                    //ePosta = "17233686130";
                    //sifre = "86130";
                }
            }
            using (veri.Varlik vari = new veri.Varlik())
            {
                ViewBag.saniyeBeklet = false;
                GirisModel model = new GirisModel();
                string _sifre = GenelIslemler.GuvenlikIslemi.sifrele(sifre);
                Random rg = new Random();

                var mevcut = await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciAdi == ePosta && p.sifre == _sifre);

                if (mevcut != null)
                {
                    Response.Cookies.Append("yanlisGirisSayisi", "0", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, // HTTPS için
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddDays(1)
                    });


                    Kullanici? tekil = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == mevcut.kullaniciKimlik);
                    if (tekil != null)
                    {
                        // Çift yönlü onay kodu
                        tekil.ciftOnayKodu = rg.Next(100000, 900000).ToString();
                        await veriTabani.KullaniciCizelgesi.kaydetKos(tekil, vari, false);
                        await SqlIslemi.sonGunGirisiBelirle(vari, tekil.kullaniciKimlik);

                        int sayi = await vari.OturumAcmaAYRINTIs.Where(p => p.i_kullaniciKimlik == tekil.kullaniciKimlik && p.gun == DateTime.Today).CountAsync();
                        veri.OturumAcma otur = new OturumAcma();
                        otur.i_kullaniciKimlik = tekil.kullaniciKimlik;
                        otur.tarih = DateTime.Now;
                        otur.gunlukSayi = sayi;
                        await otur.kaydetKos(vari, false);

                        if (sayi >= Genel.yazilimAyari.gunlukOturumSiniri)
                        {
                            await EPostaIslemi.oturumSuresiAsildi(vari, tekil, sayi);
                        }
                    }



                    await kullaniciYenile(mevcut.kullaniciKimlik);


                    enumref_OturumAcmaTuru _oturumTuru = (enumref_OturumAcmaTuru)(Genel.yazilimAyari.i_oturumAcmaTuruKimlik ?? 1);


                    if (_oturumTuru == enumref_OturumAcmaTuru.SMS_E_Posta_ve_Sifre_ile)
                    {
                        await GuvenlikIslemi.dogrulamaKoduGonder(mevcut, true, true);
                        return "/KimlikDogrula";
                    }
                    if (_oturumTuru == enumref_OturumAcmaTuru.SMS_ve_sifre_ile)
                    {
                        await GuvenlikIslemi.dogrulamaKoduGonder(mevcut, true, false);
                        return "/KimlikDogrula";
                    }
                    if (_oturumTuru == enumref_OturumAcmaTuru.E_Posta_ve_Sifre_ile)
                    {
                        await GuvenlikIslemi.dogrulamaKoduGonder(mevcut, false, true);
                        return "/KimlikDogrula";
                    }


                    if (mevcut.e_sifreDegisecekmi == true)
                        return "/SifreDegistir?kod=" + mevcut.kodu;



                    if (mevcut.sonSifreDegistirmeTarihi != null)
                    {
                        int fark = (DateTime.Today - mevcut.sonSifreDegistirmeTarihi.Value).Days;
                        if (fark > Genel.yazilimAyari.sifreDegistirmeGunSayisi)
                        {
                            return "/SifreDegistir?kod=" + mevcut.kodu;
                        }
                    }

                    if (mevcut.e_sozlesmeOnaylandimi == false)
                        return "/Sozlesme/Onayla?kod=" + mevcut.kodu;
                }
                else
                {
                    if (Request.Cookies.ContainsKey("yanlisGirisSayisi"))
                    {
                        string? cookieValue = Request.Cookies["yanlisGirisSayisi"];
                        int sayi = 0;
                        Int32.TryParse(cookieValue, out sayi);

                        if (sayi > 2)
                        {
                            otuzSaniyeVarmi = true;
                            ViewBag.saniyeBeklet = true;
                            throw new Exception("Şifrenizi ya da kullanıcı adınızı birçok kez yanlış girdiniz. Lütfen 30 saniye sonra tekrar deneyiniz.");
                        }
                        sayi++;

                        Response.Cookies.Append("yanlisGirisSayisi", sayi.ToString(), new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true, // HTTPS için
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.UtcNow.AddDays(1)
                        });

                    }
                    else
                    {
                        Response.Cookies.Append("yanlisGirisSayisi", "1", new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.UtcNow.AddDays(7)
                        });

                    }



                    string ifade = Ayar.sozcuk("Kullanıcı adı veya şifre hatalı!", 1);
                    throw new Exception(ifade);
                }
            }

            return sonuc;
        }

        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _environment;

        public GirisController(Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment)
        {
            _environment = environment;
            Genel.kayitKonumu = _environment.WebRootPath;
        }
        private void kayitKonumuBelirle()
        {
            Genel.kayitKonumu = _environment.WebRootPath;
            string nedir = Genel.kayitKonumu;
        }
    }
}
