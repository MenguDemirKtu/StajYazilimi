using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class WebSayfasiController : Sayfa
    {

        public async Task<ActionResult> Duzenle()
        {
            string tanitim = "...";
            tanitim = await Genel.dokumKisaAciklama(this, "WebSayfasi");
            gorunumAyari("", "", "Ana Sayfa", "/", "/WebSayfasi/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.WebSayfasiModel modeli = new Models.WebSayfasiModel();
                await modeli.veriCekKos(mevcutKullanici());
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }


        public async Task<ActionResult> Index()
        {
            string tanitim = "...";
            tanitim = await Genel.dokumKisaAciklama(this, "WebSayfasi");
            gorunumAyari("", "", "Ana Sayfa", "/", "/WebSayfasi/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.WebSayfasiModel modeli = new Models.WebSayfasiModel();
                await modeli.veriCekKos(mevcutKullanici());
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }

        public static async Task bysMenuOlarakKaydet(veri.Varlik vari, WebSayfasi kimi)
        {
            // List<BysMenu> liste = BysMenu.ara(p => p.i_webSayfasiKimlik == kimi.webSayfasiKimlik);
            List<BysMenu> liste = await vari.BysMenus.Where(p => p.i_webSayfasiKimlik == kimi.webSayfasiKimlik && p.varmi == true).ToListAsync();
            if (liste.Count > 0)
                return;

            int ustSayfaKimlik = 0;

            if (kimi.i_modulKimlik != 0)
            {
                var ara = await vari.BysMenus.Where(p => p.e_modulSayfasimi == true && p.i_modulKimlik == kimi.i_modulKimlik && p.varmi == true).ToListAsync();
                //BysMenu.ara(p => p.e_modulSayfasimi == true && p.i_modulKimlik == kimi.i_modulKimlik && p.varmi == true).ToList();
                if (ara.Count > 0)
                {
                    ustSayfaKimlik = ara[0].bysMenuKimlik;
                }
            }

            BysMenu yeni = new BysMenu();
            yeni._varSayilan();
            yeni.i_ustMenuKimlik = 0;
            yeni.varmi = true;
            yeni.sirasi = 100;
            yeni.i_webSayfasiKimlik = kimi.webSayfasiKimlik;
            yeni.bysMenuUrl = "/" + kimi.hamAdresi;
            yeni.bysMenuAdi = kimi.sayfaBasligi;
            yeni.i_ustMenuKimlik = ustSayfaKimlik;
            if (kimi.hamAdresi != null)
                if (kimi.hamAdresi.IndexOf("/Kart/") != -1)
                    yeni.bysMenuUrl = yeni.bysMenuUrl + "0";


            if (kimi.i_sayfaTuruKimlik == 2)
            {
                yeni.sirasi = 4000;
                yeni.e_anaKullaniciGorsunmu = true;
            }
            else
            {
                yeni.sirasi = -1;
                yeni.e_anaKullaniciGorsunmu = false;
            }
            await yeni.kaydetKos(vari, false);

        }
        public async Task<ActionResult> Kart(long id)
        {
            if (!oturumAcildimi())
                return OturumAcilmadi();
            string tanitim = "....";
            tanitim = await Genel.dokumKisaAciklama(this, "WebSayfasi");
            gorunumAyari("Web Sayfası Kartı", "Web Sayfası Kartı", "Ana Sayfa", "/", "/WebSayfasi/", tanitim);
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Gorme;
            if (id > 0)
                yetkiTuru = enumref_YetkiTuru.Ekleme;
            else
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await yetkiVarmiKos("WebSayfasi", yetkiTuru))
            {
                Models.WebSayfasiModel modeli = new Models.WebSayfasiModel();
                await modeli.veriCek(mevcutKullanici(), id);
                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Sil(string id)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                if (id == null)
                    uyariVer("Hiç kayıt seçilmemiş");
                List<string> kayitlar = id.Split(',').ToList();
                if (await yetkiVarmiKos("WebSayfasi", enumref_YetkiTuru.Silme))
                {
                    for (int i = 0; i < kayitlar.Count; i++)
                    {
                        WebSayfasi silinecek = WebSayfasi.olustur(kayitlar[i]);
                        silinecek.sil();
                    }
                    Models.WebSayfasiModel modeli = new Models.WebSayfasiModel();
                    await modeli.veriCekKos(mevcutKullanici());
                    return silindiBildirimi();
                }
                else
                {
                    return yetkiYokBildirimi();
                }
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }

        public static async Task rollereKaydet(veri.Varlik vari, WebSayfasi yeni)
        {

            List<RolWebSayfasiIzni> oncekiler = await vari.RolWebSayfasiIznis.Where(p => p.i_webSayfasiKimlik == yeni.webSayfasiKimlik && p.varmi == true).ToListAsync();
            //RolWebSayfasiIzni.ara(p => p.i_webSayfasiKimlik == yeni.webSayfasiKimlik && p.varmi == true);
            List<RolAYRINTI> roller = await vari.RolAYRINTIs.ToListAsync();

            for (int i = 0; i < roller.Count; i++)
            {
                var uyan = oncekiler.FirstOrDefault(p => p.i_rolKimlik == roller[i].rolKimlik);
                if (uyan == null)
                {
                    RolWebSayfasiIzni izin = new RolWebSayfasiIzni();
                    izin._varSayilan();
                    izin.i_webSayfasiKimlik = yeni.webSayfasiKimlik;
                    izin.i_rolKimlik = roller[i].rolKimlik;
                    izin.e_eklemeIzniVarmi = yeni.e_varsayilanEklemeAcikmi;
                    izin.e_silmeIzniVarmi = yeni.e_varsayilanSilmeAcikmi;
                    izin.e_gormeIzniVarmi = yeni.e_varsayilanGorunmeAcikmi;
                    izin.e_guncellemeIzniVarmi = yeni.e_varsayilanGuncellemeAcikmi;
                    await izin.kaydetKos(vari, false);
                }
            }


        }
        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.WebSayfasiModel gelen)
        {
            try
            {
                if (!oturumAcildimi())
                    return OturumAcilmadi();
                var kaydedilecek = gelen.kartVerisi;
                enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
                if (kaydedilecek._birincilAnahtar() > 0)
                    yetkiTuru = enumref_YetkiTuru.Guncelleme;
                if (await yetkiVarmiKos(kaydedilecek, yetkiTuru) == false)
                    throw new Exception("Bu işlemi yapmaya yetkiniz yok");

                using (veri.Varlik vari = new Varlik())
                {
                    kaydedilecek._kontrolEt(dilKimlik, vari);
                    await kaydedilecek.kaydetKos(vari, false);
                    await bysMenuOlarakKaydet(vari, kaydedilecek);
                    await rollereKaydet(vari, kaydedilecek);


                    for (int i = 0; i < gelen.izinler.Count; i++)
                    {
                        var siradaki = gelen.izinler[i];
                        RolWebSayfasiIzni? izin = await RolWebSayfasiIzni.olusturKos(vari, gelen.izinler[i].rolWebSayfasiIzniKimlik);
                        if (izin != null)
                        {
                            izin.e_eklemeIzniVarmi = siradaki.e_eklemeIzniVarmi;
                            izin.e_gormeIzniVarmi = siradaki.e_gormeIzniVarmi;
                            izin.e_silmeIzniVarmi = siradaki.e_silmeIzniVarmi;
                            izin.e_guncellemeIzniVarmi = siradaki.e_guncellemeIzniVarmi;
                            await izin.kaydetKos(vari, false);
                        }
                    }


                    for (int i = 0; i < gelen.menuleri.Count; i++)
                    {
                        var siradaki = gelen.menuleri[i];
                        BysMenu? izin = await BysMenu.olusturKos(vari, gelen.menuleri[i].bysMenuKimlik);
                        if (izin == null)
                            continue;
                        izin.bysMenuAdi = siradaki.bysMenuAdi;
                        izin.sirasi = siradaki.sirasi;
                        izin.i_ustMenuKimlik = siradaki.i_ustMenuKimlik;
                        izin.e_anaKullaniciGorsunmu = siradaki.e_anaKullaniciGorsunmu;
                        await izin.kaydetKos(vari, false);
                    }

                    //veri.SayfaDegisimi deg = new SayfaDegisimi();
                    //deg.varmi = true;
                    //await vari.SayfaDegisimis.AddAsync(deg);
                    //await vari.SaveChangesAsync();
                }

                return kaydedildiBildirimi(kaydedilecek);
            }
            catch (Exception istisna)
            {
                return hataBildirimi(istisna);
            }
        }
    }
}
