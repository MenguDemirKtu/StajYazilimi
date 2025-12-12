using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniStaj.Controllers;
using UniStaj.veri;

namespace UniStaj
{
    public class Sayfa : Controller
    {
        public long sayfaKimlik { get; set; }
        public int dilKimlik { get; set; }


        protected string ipAdresi()
        {
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        }

        public async Task<WebSayfasiAYRINTI> adresBelirleKos(Sayfa sayfasi, string cizelgeAdi)
        {
            using (veri.Varlik vari = new Varlik())
            {
                return await adresBelirleKos(vari, sayfasi, cizelgeAdi);
            }
        }

        public async Task<WebSayfasiAYRINTI> adresBelirleKos(veri.Varlik vari, Sayfa sayfasi, string cizelgeAdi)
        {
            yapilanIslemler = new List<KayitAYRINTI>();
            sayfasi.ViewBag.IslemKayitlari = new List<veri.KayitAYRINTI>();
            string adres = Request.GetEncodedUrl();



            string kaynak = HttpContext.Request.Host.Value ?? "xxxx";
            adres = adres.Replace("/favicon.ico", "");
            adres = adres.Replace("\\favicon.ico", "");
            adres = adres.Replace("//favicon.ico", "");
            adres = adres.Replace(kaynak, "");
            adres = adres.Replace("https:////", "").Trim();
            adres = adres.Replace("http:////", "").Trim();
            adres = adres.Replace("https://", "").Trim();
            adres = adres.Replace("http://", "").Trim();
            adres = adres.Replace("https:/", "").Trim();
            adres = adres.Replace("http:/", "").Trim();
            adres = adres.Replace(kaynak, "");

            if (adres.IndexOf("HataBildirimi/Ariza") != -1 || adres.IndexOf("HataBildirimi/Ariza") != -1)
            {
                adres = "Ariza/";
            }

            // HataEkrani
            if (adres.IndexOf("HataEkrani") != -1 || adres.IndexOf("HataBildirimi/Index") != -1)
            {
                adres = "HataEkrani/";
            }




            if (adres.Length > 1)
                if (adres[0] == '/')
                    adres = adres.Substring(1);


            int yer0 = adres.IndexOf("?");
            if (yer0 != -1)
                adres = adres.Substring(0, yer0);


            string gorunen = Request.GetDisplayUrl();


            int yer2 = adres.IndexOf("/Sil");
            if (yer2 != -1 && yer2 == adres.Length - 4)
            {
                adres = adres.Substring(0, yer2 + 1);

            }

            if (adres == "")
                adres = "anasayfa/";

            string son = adres;
            char karakter = '/';
            if (adres[adres.Length - 1] == karakter)
                son = adres.Substring(0, adres.Length - 1);

            string[] parcalar = son.Split(karakter);

            long parametresi = 0;
            string hamAdres = "";
            for (int i = 0; i < parcalar.Length; i++)
            {
                if (string.IsNullOrEmpty(parcalar[i]))
                    continue;

                long kimlik = 0;
                if (long.TryParse(parcalar[i], out kimlik))
                {
                    parametresi = kimlik;
                    continue;
                }
                hamAdres += parcalar[i] + "/";

            }

            hamAdres = hamAdres.Replace("/Kaydet/", "/Kart/");

            int yer = hamAdres.IndexOf("Sil?id");
            if (yer != -1)
            {
                hamAdres = hamAdres.Substring(0, yer);
            }


            string bir = "EtkinlikKatilimciDuzenleme/";
            yer = hamAdres.IndexOf(bir);

            if (yer != -1)
            {
                hamAdres = hamAdres.Substring(0, bir.Length);

            }


            yer = hamAdres.IndexOf("?yarismaKimlik");
            if (yer != -1)
            {
                hamAdres = hamAdres.Substring(0, yer);
            }



            hamAdres = hamAdres.Trim().ToLower();
            hamAdres = hamAdres.Replace("ı", "i");
            WebSayfasiAYRINTI? uyan = await vari.WebSayfasiAYRINTIs.FirstOrDefaultAsync(p => p.hamAdresi == hamAdres);

            int wsKimlik = 0;

            if (uyan == null)
            {
                veri.WebSayfasi sayfa = new WebSayfasi();
                sayfa._varSayilan();
                sayfa.tanitim = "";
                sayfa.i_modulKimlik = 1;
                sayfa.aciklama = "";
                sayfa.i_sayfaTuruKimlik = (int)enum_sayfaTuru.dokum;
                if (hamAdres.IndexOf("Kart") != -1)
                    sayfa.i_sayfaTuruKimlik = (int)enum_sayfaTuru.kart;
                sayfa.varmi = true;
                sayfa.hamAdresi = hamAdres;

                ModulAYRINTI? modulu = await vari.ModulAYRINTIs.FirstOrDefaultAsync();
                if (modulu != null)
                    sayfa.i_modulKimlik = modulu.modulKimlik;
                //                    veriTabani.WebSayfasiCizelgesi.kaydet(sayfa, vari);

                await veriTabani.WebSayfasiCizelgesi.kaydetKos(sayfa, vari, false);
                await WebSayfasiController.bysMenuOlarakKaydet(vari, sayfa);
                await WebSayfasiController.rollereKaydet(vari, sayfa);

                Response.Redirect("/WebSayfasi/Kart/" + sayfa.webSayfasiKimlik.ToString());

                wsKimlik = sayfa.webSayfasiKimlik;
            }
            else
            {
                if (uyan.fotosu != null)
                    sayfasi.ViewBag.SayfaGoruntusu = uyan.fotosu;
                else
                    sayfasi.ViewBag.SayfaGoruntusu = "/Yuklenen/baslikResimleri/varsayilan.png";

                sayfasi.ViewBag.SayfaGorunurAdi = uyan.sayfaBasligi;
                sayfasi.ViewBag.sayfaKimlik = uyan.webSayfasiKimlik.ToString();
                sayfasi.ViewBag.sayfaParametre = parametresi.ToString();
                ViewBag.KullanimKilavuzu = uyan.dokumAciklamasi;

                //var kilavuzu = Genel.sayfaKilavuzlari.FirstOrDefault(p => p.i_webSayfasiKimlik == uyan.webSayfasiKimlik && p.e_gecerlimi == true) ?? new SayfaKilavuzuAYRINTI();
                //if (kilavuzu != null)
                //{
                //    ViewBag.KullanimKilavuzu = kilavuzu.adresFormati();
                //    ViewBag.tanitimVideosu = kilavuzu.adresFormati();
                //}
                //else
                //{
                //    ViewBag.KullanimKilavuzu = "";
                //}


                _sayfaBasligi = sayfasi.ViewBag.SayfaGorunurAdi;

                if (parametresi > 0)
                {
                    yapilanIslemler = await vari.KayitAYRINTIs.Where(p => p.cizelgeAdi == cizelgeAdi && p.cizelgeKimlik == parametresi).ToListAsync();
                    sayfasi.ViewBag.IslemKayitlari = yapilanIslemler;
                }

                wsKimlik = uyan.webSayfasiKimlik;
            }

            sayfasi.ViewBag.SayfaTuru = (int)sayfaTuru;

            if (uyan == null)
            {

                uyan = await vari.WebSayfasiAYRINTIs.FirstOrDefaultAsync(p => p.webSayfasiKimlik == wsKimlik) ?? new WebSayfasiAYRINTI();
                return uyan;
            }
            else
            {
                return uyan;
            }

        }
        protected async Task<string> kullaniciYenile(long kimlik)
        {
            string kisiAdi = "";
            using (veri.Varlik vari = new Varlik())
            {
                var mevcut = await vari.KullaniciAYRINTIs.FirstOrDefaultAsync(p => p.kullaniciKimlik == kimlik);
                if (mevcut != null)
                {
                    if (mevcut.gercekAdi != null)
                        kisiAdi = mevcut.gercekAdi;

                    Yonetici yonetici = new Yonetici();
                    yonetici.kullaniciKimlik = mevcut.kullaniciKimlik;
                    yonetici.kullaniciAdi = mevcut.kullaniciAdi;
                    yonetici.ekKullaniciSayisi = await vari.KullaniciAYRINTIs.Where(p => p.tcKimlikNo == mevcut.tcKimlikNo).CountAsync();
                    yonetici.gercekAdi = mevcut.gercekAdi ?? "";
                    yonetici.tcKimlikNo = mevcut.tcKimlikNo ?? "";
                    yonetici.unvan = mevcut.unvan ?? "";
                    yonetici.ogrenciNo = mevcut.ogrenciNo ?? "";
                    yonetici.yoneticiTuru = "Kullanıcı";
                    yonetici.fotosu = mevcut.fotosu ?? "";
                    if (mevcut.e_sifreDegisecekmi == null)
                        mevcut.e_sifreDegisecekmi = true;
                    yonetici.e_sifreDegisecekmi = mevcut.e_sifreDegisecekmi.Value;
                    if (mevcut.e_sozlesmeOnaylandimi == null)
                        mevcut.e_sozlesmeOnaylandimi = false;
                    yonetici.e_sozlesmeOnaylandimi = mevcut.e_sozlesmeOnaylandimi.Value;
                    if (mevcut.i_fotoKimlik == 0 || mevcut.i_fotoKimlik == null)
                        yonetici.fotosu = mevcut.fotoBilgisi ?? "";

                    if (mevcut.i_kullaniciTuruKimlik == null)
                        throw new Exception("Kullanıcı türü belirlenmemiş");
                    yonetici.i_kullaniciTuruKimlik = (int)mevcut.i_kullaniciTuruKimlik;
                    yonetici.dilKimlik = 1;



                    if (mevcut.e_rolTabanlimi == null)
                        mevcut.e_rolTabanlimi = false;

                    yonetici.e_rolTabanlimi = (bool)mevcut.e_rolTabanlimi;

                    if (mevcut.bysyeIlkGirisTarihi == null)
                    {
                        Kullanici? tekil = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == mevcut.kullaniciKimlik);
                        if (tekil != null)
                        {
                            tekil.bysyeIlkGirisTarihi = DateTime.Now;
                            await veriTabani.KullaniciCizelgesi.kaydetKos(tekil, vari, false);
                        }
                    }


                    if (yonetici.e_rolTabanlimi == false)
                    {
                        enumref_KullaniciTuru tur = (enumref_KullaniciTuru)yonetici.i_kullaniciTuruKimlik;
                        if (tur == enumref_KullaniciTuru.Sistem_Yoneticisi || tur == enumref_KullaniciTuru.Yazilimci)
                        {
                            yonetici.kisitliGosterimmi = false;
                        }
                        else
                        {
                            yonetici.kisitliGosterimmi = true;
                        }
                    }
                    else
                    {
                        yonetici.kisitliGosterimmi = true;
                        List<KullaniciRoluAYRINTI> roller = await vari.KullaniciRoluAYRINTIs.Where(p => p.i_kullaniciKimlik == yonetici.kullaniciKimlik).ToListAsync();
                        if (roller.Count == 0)
                        {
                            List<Rol> varsayilanlar = Rol.ara(p => p.e_gecerlimi == true
                            && p.i_varsayilanOlduguKullaniciTuruKimlik == yonetici.i_kullaniciTuruKimlik
                            && p.e_varsayilanmi == true && p.varmi == true).ToList();
                            foreach (var siradaki in varsayilanlar)
                            {
                                KullaniciRolu rolu = new KullaniciRolu();
                                rolu.i_kullaniciKimlik = yonetici.kullaniciKimlik;
                                rolu.i_rolKimlik = siradaki.rolKimlik;
                                rolu._varSayilan();
                                rolu.e_gecerlimi = true;
                                await rolu.kaydetKos(vari, false);
                            }
                            roller = await vari.KullaniciRoluAYRINTIs.Where(p => p.i_kullaniciKimlik == yonetici.kullaniciKimlik).ToListAsync();
                        }

                        List<int> sa = new List<int>();
                        sa.Add(0);

                        for (int l = 0; l < roller.Count; l++)
                            sa.Add(roller[l].i_rolKimlik);

                        yonetici.rolleri = sa.ToArray();
                    }


                    if (mevcut.e_rolTabanlimi == null)
                        mevcut.e_rolTabanlimi = false;

                    var liste = FotografAYRINTI.ara(p => p.ilgiliCizelge == "ref_KullaniciTuru" && p.ilgiliKimlik == (int)yonetici.i_kullaniciTuruKimlik);
                    if (liste.Count > 0)
                    {
                        yonetici.kullaniciTuruFotosu = liste[0].konum ?? "";
                    }

                    try
                    {
                        Kayit kaydi = new Kayit(mevcut.kullaniciKimlik, "Giriş", yonetici.kullaniciAdi + " adlı kullanıcının sisteme giririşi");
                        kaydi.ipAdresi = ipAdresi();
                        kaydi.i_kullaniciKimlik = yonetici.kullaniciKimlik;
                        kaydi.ekBilgi = yonetici.kullaniciAdi + " adlı kullanıcının sisteme giririşi";
                        kaydi.islemTuru = "G";
                        kaydi.tarih = DateTime.Now;
                        kaydi.cizelgeAdi = "Giriş";
                        vari.Kayits.Add(kaydi);
                        vari.SaveChanges();

                        var tekil = vari.Kullanicis.FirstOrDefault(p => p.kullaniciKimlik == mevcut.kullaniciKimlik);
                        if (tekil != null)
                            veriTabani.KullaniciCizelgesi.kaydet(tekil, vari, false);
                    }
                    catch
                    {
                    }
                    if (HttpContext.Session.GetString("mevcutKullanici") == null)
                        yonetici._bildirimler = vari.KullaniciBildirimis.Where(p => p.i_kullaniciKimlik == yonetici.kullaniciKimlik && p.varmi == true && p.e_goruldumu == 0).OrderBy(p => p.tarihi).ToList();


                    if (Genel.yazilimAyari.i_oturumAcmaTuruKimlik == (int)enumref_OturumAcmaTuru.Dogruan_Sifre_ile)
                        yonetici.e_kodOnaylandimi = true;

                    string jsonser = JsonConvert.SerializeObject(yonetici);
                    HttpContext.Session.SetString("mevcutKullanici", jsonser);
                    HttpContext.Session.SetInt32("kullaniciKimlik", mevcut.kullaniciKimlik);
                    try
                    {
                        //if (mevcut.e_bysyeGirdimi == null || mevcut.e_bysyeGirdimi == false)
                        //{
                        //    KisiAYRINTI insan = vari.KisiAYRINTIs.FirstOrDefault(p => p.tcKimlikNo == mevcut.tcKimlikNo);
                        //    if (insan != null)
                        //    {
                        //        Kisi tekil = Kisi.olustur(insan.kisiKimlik);

                        //    }
                        //    Kullanici kul = Kullanici.olustur(mevcut.kullaniciKimlik);
                        //    kul.e_bysyeGirdimi = true;
                        //    kul.bysyeIlkGirisTarihi = DateTime.Now;
                        //    kul.kaydet();
                        //}
                    }
                    catch
                    {
                    }
                }
            }
            return kisiAdi;
        }
        public static string dosyaKonumDuzelt(string katar, YazilimAyariAYRINTI ayar)
        {
            if (string.IsNullOrEmpty(katar))
                return katar;

            if (string.IsNullOrEmpty(ayar.dosyaPaylasimKonumu))
                return katar;
            katar = katar.Replace(ayar.dosyaPaylasimKonumu, ayar.yeniDosyaPaylasimKonumu);
            if (string.IsNullOrEmpty(ayar.dosyaPaylasimKonumu2))
                return katar;
            katar = katar.Replace(ayar.dosyaPaylasimKonumu2, ayar.yeniDosyaPaylasimKonumu2);
            if (string.IsNullOrEmpty(ayar.resimPaylasimKonumu))
                return katar;
            katar = katar.Replace(ayar.resimPaylasimKonumu, ayar.yeniResimPaylasimKonumu);
            if (string.IsNullOrEmpty(ayar.resimPaylasimKonumu2))
                return katar;
            katar = katar.Replace(ayar.resimPaylasimKonumu2, ayar.yeniResimPaylasimKonumu2);

            return katar;
        }

        public static async Task<string> tamMenuOlustur()
        {
            string sonuc = "";
            using (veri.Varlik vari = new Varlik())
            {
                List<BysMenuAYRINTI> tumu = await vari.BysMenuAYRINTIs.Where(p => p.sirasi != -1).ToListAsync();
                List<BysMenuAYRINTI> analar = tumu.Where(p => p.e_modulSayfasimi == true).OrderBy(p => p.sirasi).ToList();

                for (int i = 0; i < analar.Count; i++)
                {
                    var altlar = tumu.Where(p => p.e_modulSayfasimi == false && p.i_ustMenuKimlik == analar[i].bysMenuKimlik && p.e_anaKullaniciGorsunmu != false).OrderBy(p => p.sirasi).ToList();


                    string ust = @"       <li>
            <a href=""javascript: void(0);"" class=""has-arrow waves-effect"">
                <i class=""dripicons-anchor""></i>
                <span key=""t-crypto"">Tasima</span>
            </a>
            <ul class=""sub-menu"" aria-expanded=""false"">";


                    ust = ust.Replace("dripicons-anchor", analar[i].bysMenuBicim);
                    ust = ust.Replace("Tasima", analar[i].bysMenuAdi);

                    for (int j = 0; j < altlar.Count; j++)
                    {
                        string resim = "";
                        resim = @"bx bx-right-arrow";
                        string ara = String.Format("<li " + resim + "><a href=\"{0}\" >  {1}</a></li>", altlar[j].bysMenuUrl, altlar[j].bysMenuAdi);
                        ust += ara;
                    }
                    ust += " </ul></li>";
                    sonuc += ust;

                }
            }
            return sonuc;
        }
        protected JsonResult kaydedildiBildirimi(long kimlik, string ifade)
        {
            return Json(new
            {
                success = true,
                message = ifade,
                kimlik = kimlik
            });
        }
        protected JsonResult kaydedildiBildirimi(Bilesen kaydedilecek)
        {
            return Json(new
            {
                success = true,
                message = kaydedilecek._tanimi() + " adlı " + kaydedilecek._turkceAdi() + " başarıyla kaydedildi.",
                kimlik = kaydedilecek._birincilAnahtar().ToString()
            });
        }
        protected JsonResult silindiBildirimi()
        {
            return Json(new
            {
                success = true,
                message = "Seçtiğiniz kayıtlar başarıyla silindi",
                satirID = "0"
            });
        }

        protected JsonResult yetkiYokBildirimi()
        {
            return Json(new
            {
                success = false,
                message = "Silme işlemi için yetkiniz yok",
                satirID = "0"
            });
        }
        protected JsonResult yetkiYokBildirimi(int dilKimlik)
        {
            return Json(new
            {
                success = false,
                message = Ayar.sozcuk("Silme işlemi için yetkiniz yok", dilKimlik),
                yonlensinmi = false,
                yonlenmeAdresi = "",
                satirID = "0"
            });
        }

        protected JsonResult hataBildirimi(Exception istisna)
        {
            return Json(new
            {
                success = false,
                message = istisna.Message,
                yonlensinmi = false,
                yonlenmeAdresi = "",
                satirID = "0"
            });
        }


        protected JsonResult hataBildirimi(Exception istisna, string _yonlenmeAdresi)
        {
            return Json(new
            {
                success = false,
                message = istisna.Message,
                yonlensinmi = true,
                yonlenmeAdresi = _yonlenmeAdresi,
                satirID = "0"
            });
        }


        protected JsonResult basariBildirimi(string ifade)
        {
            return Json(new
            {
                success = true,
                message = ifade,
                yonlensinmi = false,
                yonlenmeAdresi = "",
                satirID = "0"
            });
        }

        protected JsonResult basariBildirimi(Bilesen kaydedilecek, int dil)
        {
            string ileti = Ikazlar.bilesenKaydedildi(kaydedilecek, dil);
            return Json(new
            {
                success = true,
                message = ileti,
                yonlensinmi = false,
                yonlenmeAdresi = "",
                kimlik = kaydedilecek._birincilAnahtar().ToString()
            });
        }
        protected JsonResult basariBildirimi(Bilesen kaydedilecek, int dil, string _yonlenmeAdresi)
        {
            string ileti = Ikazlar.bilesenKaydedildi(kaydedilecek, dil);
            return Json(new
            {
                success = true,
                message = ileti,
                yonlensinmi = true,
                yonlenmeAdresi = _yonlenmeAdresi,
                kimlik = kaydedilecek._birincilAnahtar().ToString()
            });
        }


        protected JsonResult basariBildirimi(string ileti, int dil, string _yonlenmeAdresi)
        {
            return Json(new
            {
                success = true,
                message = ileti,
                yonlensinmi = true,
                yonlenmeAdresi = _yonlenmeAdresi,
                kimlik = 0
            });
        }
        protected JsonResult kaydedildiBildirimi(long birincilAnahtar, string ifade, int dil)
        {
            return Json(new
            {
                success = true,
                message = ifade,
                yonlensinmi = false,
                yonlenmeAdresi = "",
                kimlik = birincilAnahtar.ToString()
            }); ;
        }


        protected string? _sayfaBasligi { get; set; }

        protected enumref_YetkiTuru yetkiTuruBelirle(long id)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Gorme;
            if (id <= 0)
                yetkiTuru = enumref_YetkiTuru.Ekleme;
            else
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            return yetkiTuru;
        }


        protected void uyariVer(string ifade)
        {
            throw new Exception(ifade);
        }
        protected void uyariVer(Exception istisna)
        {
            throw istisna;
        }
        protected enum_sayfaTuru sayfaTuru = enum_sayfaTuru.kart;
        public enum_sayfaTuru _sayfaTuru
        {
            get
            {
                return sayfaTuru;
            }
            set
            {
                sayfaTuru = value;
            }
        }
        public List<KayitAYRINTI>? yapilanIslemler { get; set; }


        public static async Task<string> menuOlustur(int kullaniciKimlik, int rolKimlik, Yonetici kim)
        {

            using (veri.Varlik vari = new Varlik())
            {
                List<BysMenuAYRINTI> tumu = await vari.BysMenuAYRINTIs.ToListAsync();

                if (kim.e_rolTabanlimi == false)
                {
                    List<KullaniciWebSayfasiIzniAYRINTI> izinler = await vari.KullaniciWebSayfasiIzniAYRINTIs.Where(p => p.i_kullaniciKimlik == kullaniciKimlik).ToListAsync();
                    //KullaniciWebSayfasiIzniAYRINTI.ara(p => p.i_kullaniciKimlik == kullaniciKimlik).ToList();
                    var ust = tumu.Where(p => p.i_ustMenuKimlik == 0).OrderBy(p => p.sirasi).ToList();
                    string sonuc = @"    <ul class=""metismenu list-unstyled"" id=""side-menu"">
                            <li class=""menu-title"" key=""t-menu"">Menu</li>";

                    for (int i = 0; i < ust.Count; i++)
                    {
                        var alt = tumu.Where(p => p.i_ustMenuKimlik == ust[i].bysMenuKimlik).OrderBy(p => p.sirasi).ToList();
                        string ara = "";
                        ara += String.Format(" <li><a href=\"javascript: void(0);\" ><i class=\"{0}\"></i><span key=\"t-crypto\">{1}</span> </a>", ust[i].bysMenuBicim, ust[i].bysMenuAdi);
                        ara += "     <ul class=\"sub-menu\" aria-expanded=\"false\">";
                        int uyan = 0;
                        foreach (var siradaki in alt)
                        {
                            var izni = izinler.FirstOrDefault(p => p.i_webSayfasiKimlik == siradaki.i_webSayfasiKimlik && p.e_gormeIzniVarmi == true);
                            if (izni != null)
                            {
                                string yeni = String.Format("<li><a href=\"{0}\" key=\"t-light-sidebar\">{1}</a></li>", siradaki.bysMenuUrl, siradaki.bysMenuAdi);
                                ara += yeni;
                                uyan += 1;
                            }
                        }
                        ara += "</ul>";
                        ara += "</li>";
                        if (uyan > 0)
                            sonuc += " " + ara;
                    }
                    sonuc += "</ul>";
                    return sonuc;
                }
                else
                {
                    List<KullaniciRoluIzinAYRINTI> izinler = new List<KullaniciRoluIzinAYRINTI>();

                    var dizi = kim.rolleri;
                    if (dizi != null)
                    {
                        if (dizi.Length > 0)
                        {
                            for (int i = 0; i < dizi.Length; i++)
                            {
                                var liste = await vari.KullaniciRoluIzinAYRINTIs.Where(P => P.i_rolKimlik == dizi[i] && P.e_gormeIzniVarmi == true).ToListAsync();
                                izinler.AddRange(liste);
                            }
                        }
                    }

                    //                    KullaniciRoluIzinAYRINTI.ara(p => p.i_kullaniciKimlik == kullaniciKimlik && p.e_gormeIzniVarmi == true ).ToList();
                    var ust = tumu.Where(p => p.i_ustMenuKimlik == 0).OrderBy(p => p.sirasi).ToList();
                    string sonuc = @"    <ul class=""metismenu list-unstyled"" id=""side-menu"">
                            <li class=""menu-title"" key=""t-menu"">Menu</li>";

                    for (int i = 0; i < ust.Count; i++)
                    {
                        var alt = tumu.Where(p => p.i_ustMenuKimlik == ust[i].bysMenuKimlik).OrderBy(p => p.sirasi).ToList();
                        alt = alt.Where(p => p.sirasi != -1).ToList();
                        string ara = "";
                        if (string.IsNullOrEmpty(ust[i].bysMenuAdi))
                            continue;
                        ara += String.Format(" <li><a href=\"javascript: void(0);\" ><i class=\"{0}\"></i><span key=\"t-crypto\">{1}</span> </a>", ust[i].bysMenuBicim, Ayar.sozcuk(ust[i].bysMenuAdi ?? "xxx", kim.dilKimlik));
                        ara += "     <ul class=\"sub-menu\" aria-expanded=\"false\">";
                        int uyan = 0;
                        foreach (var siradaki in alt)
                        {
                            var izni = izinler.FirstOrDefault(p => p.i_webSayfasiKimlik == siradaki.i_webSayfasiKimlik && p.e_gormeIzniVarmi == true);
                            if (izni != null)
                            {
                                string yeni = String.Format("<li><a href=\"{0}\" key=\"t-light-sidebar\">{1}</a></li>", siradaki.bysMenuUrl, Ayar.sozcuk(siradaki.bysMenuAdi ?? "xxx", kim.dilKimlik));
                                ara += yeni;
                                uyan += 1;
                            }
                        }
                        ara += "</ul>";
                        ara += "</li>";
                        if (uyan > 0)
                            sonuc += " " + ara;
                    }
                    sonuc += "</ul>";
                    return sonuc;
                }
            }

        }
        //public WebSayfasiAYRINTI adresBelirle(Sayfa sayfasi, string cizelgeAdi)
        //{
        //    yapilanIslemler = new List<KayitAYRINTI>();
        //    sayfasi.ViewBag.IslemKayitlari = new List<veri.KayitAYRINTI>();
        //    string adres = Request.GetEncodedUrl();



        //    string kaynak = HttpContext.Request.Host.Value ?? "xxxx";
        //    adres = adres.Replace("/favicon.ico", "");
        //    adres = adres.Replace("\\favicon.ico", "");
        //    adres = adres.Replace("//favicon.ico", "");
        //    adres = adres.Replace(kaynak, "");
        //    adres = adres.Replace("https:////", "").Trim();
        //    adres = adres.Replace("http:////", "").Trim();
        //    adres = adres.Replace("https://", "").Trim();
        //    adres = adres.Replace("http://", "").Trim();
        //    adres = adres.Replace("https:/", "").Trim();
        //    adres = adres.Replace("http:/", "").Trim();
        //    adres = adres.Replace(kaynak, "");

        //    if (adres.IndexOf("HataBildirimi/Ariza") != -1 || adres.IndexOf("HataBildirimi/Ariza") != -1)
        //    {
        //        adres = "Ariza/";
        //    }

        //    // HataEkrani
        //    if (adres.IndexOf("HataEkrani") != -1 || adres.IndexOf("HataBildirimi/Index") != -1)
        //    {
        //        adres = "HataEkrani/";
        //    }




        //    if (adres.Length > 1)
        //        if (adres[0] == '/')
        //            adres = adres.Substring(1);


        //    int yer0 = adres.IndexOf("?");
        //    if (yer0 != -1)
        //        adres = adres.Substring(0, yer0);


        //    string gorunen = Request.GetDisplayUrl();


        //    int yer2 = adres.IndexOf("/Sil");
        //    if (yer2 != -1 && yer2 == adres.Length - 4)
        //    {
        //        adres = adres.Substring(0, yer2 + 1);

        //    }

        //    if (adres == "")
        //        adres = "anasayfa/";

        //    string son = adres;
        //    char karakter = '/';
        //    if (adres[adres.Length - 1] == karakter)
        //        son = adres.Substring(0, adres.Length - 1);

        //    string[] parcalar = son.Split(karakter);

        //    long parametresi = 0;
        //    string hamAdres = "";
        //    for (int i = 0; i < parcalar.Length; i++)
        //    {
        //        if (string.IsNullOrEmpty(parcalar[i]))
        //            continue;

        //        long kimlik = 0;
        //        if (long.TryParse(parcalar[i], out kimlik))
        //        {
        //            parametresi = kimlik;
        //            continue;
        //        }
        //        hamAdres += parcalar[i] + "/";

        //    }

        //    hamAdres = hamAdres.Replace("/Kaydet/", "/Kart/");

        //    int yer = hamAdres.IndexOf("Sil?id");
        //    if (yer != -1)
        //    {
        //        hamAdres = hamAdres.Substring(0, yer);
        //    }


        //    string bir = "EtkinlikKatilimciDuzenleme/";
        //    yer = hamAdres.IndexOf(bir);

        //    if (yer != -1)
        //    {
        //        hamAdres = hamAdres.Substring(0, bir.Length);

        //    }


        //    yer = hamAdres.IndexOf("?yarismaKimlik");
        //    if (yer != -1)
        //    {
        //        hamAdres = hamAdres.Substring(0, yer);
        //    }

        //    veri.Varlik vari = new veri.Varlik();
        //    var uyan = vari.WebSayfasiAYRINTIs.FirstOrDefault(p => p.hamAdresi == hamAdres);

        //    int wsKimlik = 0;

        //    if (uyan == null)
        //    {
        //        veri.WebSayfasi sayfa = new WebSayfasi();
        //        sayfa._varSayilan();
        //        sayfa.tanitim = "";
        //        sayfa.i_modulKimlik = 1;
        //        sayfa.aciklama = "";
        //        sayfa.i_sayfaTuruKimlik = (int)enum_sayfaTuru.dokum;
        //        if (hamAdres.IndexOf("Kart") != -1)
        //            sayfa.i_sayfaTuruKimlik = (int)enum_sayfaTuru.kart;
        //        sayfa.varmi = true;
        //        sayfa.hamAdresi = hamAdres;

        //        ModulAYRINTI? modulu = vari.ModulAYRINTIs.FirstOrDefault();
        //        if (modulu != null)
        //            sayfa.i_modulKimlik = modulu.modulKimlik;
        //        veriTabani.WebSayfasiCizelgesi.kaydet(sayfa, vari);
        //        Response.Redirect("/WebSayfasi/Kart/" + sayfa.webSayfasiKimlik.ToString());

        //        wsKimlik = sayfa.webSayfasiKimlik;
        //    }
        //    else
        //    {
        //        if (uyan.fotosu != null)
        //            sayfasi.ViewBag.SayfaGoruntusu = uyan.fotosu;
        //        else
        //            sayfasi.ViewBag.SayfaGoruntusu = "/Yuklenen/baslikResimleri/varsayilan.png";

        //        sayfasi.ViewBag.SayfaGorunurAdi = uyan.sayfaBasligi;
        //        sayfasi.ViewBag.sayfaKimlik = uyan.webSayfasiKimlik.ToString();
        //        sayfasi.ViewBag.sayfaParametre = parametresi.ToString();
        //        ViewBag.KullanimKilavuzu = uyan.aciklama;

        //        //var kilavuzu = Genel.sayfaKilavuzlari.FirstOrDefault(p => p.i_webSayfasiKimlik == uyan.webSayfasiKimlik && p.e_gecerlimi == true) ?? new SayfaKilavuzuAYRINTI();
        //        //if (kilavuzu != null)
        //        //{
        //        //    ViewBag.KullanimKilavuzu = kilavuzu.adresFormati();
        //        //    ViewBag.tanitimVideosu = kilavuzu.adresFormati();
        //        //}
        //        //else
        //        //{
        //        //    ViewBag.KullanimKilavuzu = "";
        //        //}


        //        _sayfaBasligi = sayfasi.ViewBag.SayfaGorunurAdi;

        //        if (parametresi > 0)
        //        {
        //            yapilanIslemler = vari.KayitAYRINTIs.Where(p => p.cizelgeAdi == cizelgeAdi && p.cizelgeKimlik == parametresi).ToList();
        //            sayfasi.ViewBag.IslemKayitlari = yapilanIslemler;
        //        }

        //        wsKimlik = uyan.webSayfasiKimlik;
        //    }

        //    sayfasi.ViewBag.SayfaTuru = (int)sayfaTuru;

        //    if (uyan == null)
        //    {
        //        uyan = WebSayfasiAYRINTI.olustur(wsKimlik);
        //        return uyan;
        //    }
        //    else
        //    {
        //        return uyan;
        //    }
        //}


        protected void gorunumAyari(string SayfaGorunurAdi, string UstSayfaGorunurAdi, string UstSayfaAdi, string ustSayfaAdresi, string SayfaAdresi, string SayfaAciklamasi)
        {
            string? kAdi = HttpContext.Session.GetString("mevcutKullanici");
            if (kAdi == null)
                return;
            int dil = mevcutKullanici().dilKimlik;
            ViewBag.dil = mevcutKullanici().dilKimlik.ToString();
            ViewBag.UstSayfaGorunurAdi = UstSayfaGorunurAdi;
            ViewBag.UstSayfaAdresi = ustSayfaAdresi;
            ViewBag.SayfaAdresi = SayfaAdresi;
            ViewBag.UstSayfaAdi = UstSayfaAdi;
            ViewBag.SayfaAciklamasi = SayfaAciklamasi;
        }






        protected List<long> secilenKimlikler(string gorme)
        {
            List<long> sonuc = new List<long>();
            gorme = gorme.Replace('[', ' ');
            gorme = gorme.Replace(']', ' ');
            string[] satirlar = gorme.Trim().Split(',');

            for (int i = 0; i < satirlar.Length; i++)
            {
                long kim = 0;
                string sat = satirlar[i].Replace(((char)34), ' ').Trim();
                if (long.TryParse(sat, out kim))
                    sonuc.Add(kim);
            }

            return sonuc;
        }
        protected ActionResult OturumAcilmadi()
        {
            return RedirectToAction("Index", "Giris");
        }
        protected ActionResult AnaSayfayaGit()
        {
            return RedirectToAction("Index", "Anasayfa");
        }

        public Yonetici mevcutKullanici()
        {
            string? kullanici = HttpContext.Session.GetString("mevcutKullanici");
            Yonetici? sonuc = JsonConvert.DeserializeObject<Yonetici>(kullanici ?? "");
            if (sonuc == null)
                throw new Exception("Kullanıcı bulunamadı");
            else
                return sonuc;
        }
        public int mevcutDil()
        {
            try
            {
                string? kullanici = HttpContext.Session.GetString("mevcutKullanici");
                Yonetici? sonuc = JsonConvert.DeserializeObject<Yonetici>(kullanici ?? "");
                if (sonuc == null)
                    return 1;
                else
                    return sonuc.dilKimlik;
            }
            catch
            {
                return 1;
            }
        }
        //protected Yonetici mevcutKullanici(Bilesen kime)
        //{
        //    var kullanici = HttpContext.Session.GetString("mevcutKullanici");
        //    Yonetici sonuc = JsonConvert.DeserializeObject<Yonetici>(kullanici);

        //    if (kime != null)
        //        if (sonuc != null)
        //            kime._kullaniciAta(sonuc.kullaniciKimlik);
        //    return sonuc;
        //}

        protected bool oturumAcildimi()
        {
            if (HttpContext.Session.GetString("mevcutKullanici") == null)
                return false;
            else
                return true;
        }




        ///// <summary>
        ///// Görme izni varmı diye kontrol eder.
        ///// </summary>
        ///// <returns></returns>
        //public bool yetkiVarmi()
        //{
        //    Bilesen onemsi = new Bilesen();
        //    return yetkiVarmi(onemsi, enumref_YetkiTuru.Gorme);
        //}

        public async Task<bool> yetkiVarmiKos()
        {
            Bilesen onemsi = new Bilesen();
            using (veri.Varlik vari = new Varlik())
            {
                return await yetkiVarmiKos(vari, onemsi, enumref_YetkiTuru.Gorme);
            }
        }

        public async Task<bool> yetkiVarmiKos(Bilesen onemsi, enumref_YetkiTuru tur)
        {
            using (veri.Varlik vari = new Varlik())
            {
                return await yetkiVarmiKos(vari, onemsi, tur);
            }
        }

        //if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)


        private bool rolTabanli(Yonetici kim, enumref_YetkiTuru aranan)
        {

            ViewBag.eklemeIznivar = true; // izinler[0].e_eklemeIzniVarmi;
            ViewBag.guncellemeIzniVar = true; // izinler[0].e_guncellemeIzniVarmi; 
            ViewBag.silmeIzniVar = true; //izinler[0].e_silmeIzniVarmi; 
            ViewBag.gormeIzniVar = true; // izinler[0].e_gormeIzniVarmi; 



            veri.Varlik vari = new Varlik();
            if (_sayfaBasligi == "Ayarlar")
            {
                ViewBag.eklemeIznivar = true;
                ViewBag.guncellemeIzniVar = true;
                ViewBag.gormeIzniVar = true;
            }
            else
            {
                List<KullaniciRolWebSayfasiIzniAYRINTI> izinler = vari.KullaniciRolWebSayfasiIzniAYRINTIs.Where(p => p.sayfaBasligi == _sayfaBasligi && p.i_kullaniciKimlik == kim.kullaniciKimlik).ToList();

                if (aranan == enumref_YetkiTuru.Ekleme)
                    izinler = izinler.Where(p => p.e_eklemeIzniVarmi == true).ToList();

                if (aranan == enumref_YetkiTuru.Silme)
                    izinler = izinler.Where(p => p.e_silmeIzniVarmi == true).ToList();


                if (aranan == enumref_YetkiTuru.Guncelleme)
                    izinler = izinler.Where(p => p.e_guncellemeIzniVarmi == true).ToList();


                if (aranan == enumref_YetkiTuru.Gorme)
                    izinler = izinler.Where(p => p.e_gormeIzniVarmi == true).ToList();


                if (izinler == null)
                    return false;

                if (izinler.Count == 0)
                    return false;



                ViewBag.eklemeIznivar = izinler[0].e_eklemeIzniVarmi;
                ViewBag.guncellemeIzniVar = izinler[0].e_guncellemeIzniVarmi;
                ViewBag.silmeIzniVar = izinler[0].e_silmeIzniVarmi;
                ViewBag.gormeIzniVar = izinler[0].e_gormeIzniVarmi;
            }
            return true;
        }

        private async Task<bool> rolTabanliKos(veri.Varlik vari, Yonetici kim, enumref_YetkiTuru aranan)

        {

            ViewBag.eklemeIznivar = true; // izinler[0].e_eklemeIzniVarmi;
            ViewBag.guncellemeIzniVar = true; // izinler[0].e_guncellemeIzniVarmi; 
            ViewBag.silmeIzniVar = true; //izinler[0].e_silmeIzniVarmi; 
            ViewBag.gormeIzniVar = true; // izinler[0].e_gormeIzniVarmi; 



            if (_sayfaBasligi == "Ayarlar")
            {
                ViewBag.eklemeIznivar = true;
                ViewBag.guncellemeIzniVar = true;
                ViewBag.gormeIzniVar = true;
            }
            else
            {
                List<KullaniciRolWebSayfasiIzniAYRINTI> izinler = await vari.KullaniciRolWebSayfasiIzniAYRINTIs.Where(p => p.sayfaBasligi == _sayfaBasligi && p.i_kullaniciKimlik == kim.kullaniciKimlik).ToListAsync();

                if (aranan == enumref_YetkiTuru.Ekleme)
                    izinler = izinler.Where(p => p.e_eklemeIzniVarmi == true).ToList();

                if (aranan == enumref_YetkiTuru.Silme)
                    izinler = izinler.Where(p => p.e_silmeIzniVarmi == true).ToList();


                if (aranan == enumref_YetkiTuru.Guncelleme)
                    izinler = izinler.Where(p => p.e_guncellemeIzniVarmi == true).ToList();


                if (aranan == enumref_YetkiTuru.Gorme)
                    izinler = izinler.Where(p => p.e_gormeIzniVarmi == true).ToList();


                if (izinler == null)
                    return false;

                if (izinler.Count == 0)
                    return false;



                ViewBag.eklemeIznivar = izinler[0].e_eklemeIzniVarmi;
                ViewBag.guncellemeIzniVar = izinler[0].e_guncellemeIzniVarmi;
                ViewBag.silmeIzniVar = izinler[0].e_silmeIzniVarmi;
                ViewBag.gormeIzniVar = izinler[0].e_gormeIzniVarmi;
            }
            return true;
        }
        private bool kisiTabanli(Yonetici kim, enumref_YetkiTuru aranan)
        {
            List<KullaniciWebSayfasiIzniAYRINTI> izinler = KullaniciWebSayfasiIzniAYRINTI.ara(p => p.i_webSayfasiKimlik == sayfaKimlik && p.i_kullaniciKimlik == kim.kullaniciKimlik);


            if (aranan == enumref_YetkiTuru.Ekleme)
                izinler = izinler.Where(p => p.e_eklemeIzniVarmi == true).ToList();

            if (aranan == enumref_YetkiTuru.Silme)
                izinler = izinler.Where(p => p.e_silmeIzniVarmi == true).ToList();


            if (aranan == enumref_YetkiTuru.Guncelleme)
                izinler = izinler.Where(p => p.e_guncellemeIzniVarmi == true).ToList();


            if (aranan == enumref_YetkiTuru.Gorme)
                izinler = izinler.Where(p => p.e_gormeIzniVarmi == true).ToList();


            if (izinler == null)
                return false;

            if (izinler.Count == 0)
                return false;



            ViewBag.eklemeIznivar = izinler[0].e_eklemeIzniVarmi;
            ViewBag.guncellemeIzniVar = izinler[0].e_guncellemeIzniVarmi; ;
            ViewBag.silmeIzniVar = izinler[0].e_silmeIzniVarmi; ;
            ViewBag.gormeIzniVar = izinler[0].e_gormeIzniVarmi; ;

            return true;
        }


        private async Task<bool> kisiTabanliKos(veri.Varlik vari, Yonetici kim, enumref_YetkiTuru aranan)
        {

            List<KullaniciWebSayfasiIzniAYRINTI> izinler = await vari.KullaniciWebSayfasiIzniAYRINTIs.Where(p => p.i_webSayfasiKimlik == sayfaKimlik && p.i_kullaniciKimlik == kim.kullaniciKimlik).ToListAsync();


            if (aranan == enumref_YetkiTuru.Ekleme)
                izinler = izinler.Where(p => p.e_eklemeIzniVarmi == true).ToList();

            if (aranan == enumref_YetkiTuru.Silme)
                izinler = izinler.Where(p => p.e_silmeIzniVarmi == true).ToList();


            if (aranan == enumref_YetkiTuru.Guncelleme)
                izinler = izinler.Where(p => p.e_guncellemeIzniVarmi == true).ToList();


            if (aranan == enumref_YetkiTuru.Gorme)
                izinler = izinler.Where(p => p.e_gormeIzniVarmi == true).ToList();


            if (izinler == null)
                return false;

            if (izinler.Count == 0)
                return false;



            ViewBag.eklemeIznivar = izinler[0].e_eklemeIzniVarmi;
            ViewBag.guncellemeIzniVar = izinler[0].e_guncellemeIzniVarmi; ;
            ViewBag.silmeIzniVar = izinler[0].e_silmeIzniVarmi; ;
            ViewBag.gormeIzniVar = izinler[0].e_gormeIzniVarmi; ;

            return true;
        }
        public async Task<bool> yetkiVarmiKos(enumref_YetkiTuru aranan)
        {
            Bilesen onemsiz = new Bilesen();
            return await yetkiVarmiKos(onemsiz, aranan);
        }
        //public bool yetkiVarmi(Bilesen onemsiz, enumref_YetkiTuru aranan)
        //{
        //    if (!oturumAcildimi())
        //        return false;

        //    var nedir = _sayfaBasligi;

        //    var kim = mevcutKullanici();

        //    dilKimlik = kim.dilKimlik;

        //    if (kim.e_sifreDegisecekmi == true)
        //        return false;

        //    if (kim.e_sozlesmeOnaylandimi == false)
        //        return false;

        //    if (kim.e_kodOnaylandimi == false)
        //        return false;




        //    WebSayfasiAYRINTI ayrintisi = adresBelirle(this, onemsiz._cizelgeAdi());
        //    if (ayrintisi != null)
        //        sayfaKimlik = ayrintisi.webSayfasiKimlik;


        //    if (kim._turu() == enumref_KullaniciTuru.Yazilimci || kim._turu() == enumref_KullaniciTuru.Sistem_Yoneticisi)
        //    {
        //        ViewBag.eklemeIznivar = true;
        //        ViewBag.guncellemeIzniVar = true;
        //        ViewBag.silmeIzniVar = true;
        //        ViewBag.gormeIzniVar = true;
        //        return true;
        //    }
        //    else
        //    {
        //        if (kim.e_rolTabanlimi == true)
        //        {
        //            return rolTabanli(kim, aranan);
        //        }
        //        else
        //        {
        //            ViewBag.eklemeIznivar = true;
        //            ViewBag.guncellemeIzniVar = true;
        //            ViewBag.silmeIzniVar = true;
        //            ViewBag.gormeIzniVar = true;


        //            //return true;

        //            return kisiTabanli(kim, aranan);
        //        }
        //    }
        //}

        /// <summary>
        /// Koşut yordam ile yetki olup olmadığını döndürür
        /// </summary>
        /// <param name="onemsiz"></param>
        /// <param name="aranan"></param>
        /// <returns></returns>
        public async Task<bool> yetkiVarmiKos(veri.Varlik vari, Bilesen onemsiz, enumref_YetkiTuru aranan)
        {
            if (!oturumAcildimi())
                return false;

            var nedir = _sayfaBasligi;

            var kim = mevcutKullanici();

            dilKimlik = kim.dilKimlik;

            if (kim.e_sifreDegisecekmi == true)
                return false;

            if (kim.e_sozlesmeOnaylandimi == false)
                return false;

            if (kim.e_kodOnaylandimi == false)
                return false;




            WebSayfasiAYRINTI ayrintisi = await adresBelirleKos(vari, this, onemsiz._cizelgeAdi());
            if (ayrintisi != null)
                sayfaKimlik = ayrintisi.webSayfasiKimlik;


            if (kim._turu() == enumref_KullaniciTuru.Yazilimci || kim._turu() == enumref_KullaniciTuru.Sistem_Yoneticisi)
            {
                ViewBag.eklemeIznivar = true;
                ViewBag.guncellemeIzniVar = true;
                ViewBag.silmeIzniVar = true;
                ViewBag.gormeIzniVar = true;
                return true;
            }
            else
            {
                if (kim.e_rolTabanlimi == true)
                {
                    return await rolTabanliKos(vari, kim, aranan);
                }
                else
                {
                    ViewBag.eklemeIznivar = true;
                    ViewBag.guncellemeIzniVar = true;
                    ViewBag.silmeIzniVar = true;
                    ViewBag.gormeIzniVar = true;
                    return await kisiTabanliKos(vari, kim, aranan);
                }
            }
        }

        //
        public async Task<bool> yetkiVarmiKos(string kimden, enumref_YetkiTuru aranan)
        {
            ViewBag.eklemeIznivar = false;
            ViewBag.guncellemeIzniVar = false;
            ViewBag.silmeIzniVar = false;
            ViewBag.gormeIzniVar = false;

            if (!oturumAcildimi())
                return false;
            Bilesen onemsiz = new Bilesen();
            return await yetkiVarmiKos(onemsiz, aranan);
        }
        public ActionResult YetkiYok()
        {
            return RedirectToAction("Index", "YetkiYok");
        }
        public ActionResult YarisHakkiYok(string gerekce)
        {
            ViewBag.gerekce = gerekce;
            return RedirectToAction("Index", "YarismaBasvuruHakkiYok");
        }
        public ActionResult HataSayfasi()
        {
            return RedirectToAction("Index", "HataSayfasi");
        }



        public async Task<ActionResult> HataSayfasiKosut(Exception istisna)
        {
            string kod = "---";
            try
            {
                using (veri.Varlik vari = new Varlik())
                {
                    SistemHatasi hata = new SistemHatasi();
                    hata.tarih = DateTime.Now;
                    hata.aciklama = istisna.ToString();
                    hata.varmi = true;
                    hata.sayfaBaslik = "";
                    hata.tanitim = istisna.Message;
                    hata.kodu = Guid.NewGuid().ToString();
                    await vari.SistemHatasis.AddAsync(hata);
                    await vari.SaveChangesAsync();
                    kod = hata.kodu;
                }
            }
            catch
            {
            }

            return RedirectToAction("Index", "HataEkrani", new { @id = kod });
        }
        public static string ReplaceTurkceKarakter(string turkishWord)
        {
            string source = "ığüşöçĞÜŞİÖÇ";
            string destination = "igusocGUSIOC";

            string result = turkishWord;

            for (int i = 0; i < source.Length; i++)
            {
                result = result.Replace(source[i], destination[i]);
            }

            return result;
        }
    }
}