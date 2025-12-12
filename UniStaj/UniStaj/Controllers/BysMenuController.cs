using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.Controllers
{
    public class BysMenuController : Sayfa
    {

        [HttpPost]
        public async Task<ActionResult> Cek(Models.BysMenuModel modeli)
        {
            var nedir = await modeli.ayrintiliAraKos(this);
            return basariBildirimi("/BysMenu?id=" + nedir.kodu);
        }

        public async Task<IActionResult> Index(string id)
        {

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "BysMenu");
            gorunumAyari("", "", "Ana Sayfa", "/", "/BysMenu/", tanitim);
            if (!oturumAcildimi())
                return OturumAcilmadi();
            if (await yetkiVarmiKos())
            {
                Models.BysMenuModel modeli = new Models.BysMenuModel();
                if (string.IsNullOrEmpty(id))
                    await modeli.veriCekKosut(mevcutKullanici());
                else
                    await modeli.kosulaGoreCek(mevcutKullanici(), id);

                return View(modeli);
            }
            else
            {
                return YetkiYok();
            }
        }
        public async Task<ActionResult> Kart(long id)
        {
            if (!oturumAcildimi())
                return OturumAcilmadi();

            string tanitim = await Genel.dokumKisaAciklamaKos(this, "BysMenu");
            gorunumAyari("BYS Menü Kartı", "BYS Menü Kartı", "Ana Sayfa", "/", "/BysMenu/", tanitim);
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Gorme;
            if (id > 0)
                yetkiTuru = enumref_YetkiTuru.Ekleme;
            else
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await yetkiVarmiKos("BysMenu", yetkiTuru))
            {
                Models.BysMenuModel modeli = new Models.BysMenuModel();
                await modeli.veriCekKos(mevcutKullanici(), id);
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
                if (string.IsNullOrEmpty(id))
                    uyariVer("Hiç kayıt seçilmemiş");
                if (id == null)
                    id = "";
                List<string> kayitlar = id.Split(',').ToList();
                if (await yetkiVarmiKos("BysMenu", enumref_YetkiTuru.Silme))
                {
                    for (int i = 0; i < kayitlar.Count; i++)
                    {
                        BysMenu silinecek = BysMenu.olustur(kayitlar[i]);
                        silinecek._sayfaAta(this);
                        silinecek.sil();
                    }
                    Models.BysMenuModel modeli = new Models.BysMenuModel();
                    await modeli.veriCekKosut(mevcutKullanici());
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
        [HttpPost]
        public async Task<ActionResult> Kaydet(Models.BysMenuModel gelen)
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

                using (veri.Varlik vari = new veri.Varlik())
                {
                    kaydedilecek._kontrolEt(dilKimlik, vari);
                    List<WebSayfasi> sayfalar = await vari.WebSayfasis.ToListAsync();
                    if (gelen.baglilar != null)
                    {
                        for (int i = 0; i < gelen.baglilar.Count; i++)
                        {
                            var karslik = await vari.BysMenus.FirstOrDefaultAsync(p => p.bysMenuKimlik == gelen.baglilar[i].bysMenuKimlik);
                            if (karslik == null)
                                continue;
                            karslik.sirasi = gelen.baglilar[i].sirasi;
                            karslik.bysMenuAdi = gelen.baglilar[i].bysMenuAdi;
                            karslik.i_ustMenuKimlik = gelen.baglilar[i].i_ustMenuKimlik;
                            karslik.e_anaKullaniciGorsunmu = gelen.baglilar[i].e_anaKullaniciGorsunmu;


                            if (karslik.i_webSayfasiKimlik > 0)
                            {
                                var sayf = sayfalar.FirstOrDefault(p => p.webSayfasiKimlik == karslik.i_webSayfasiKimlik);
                                if (sayf != null)
                                {
                                    if (karslik.i_modulKimlik > 0)
                                        sayf.i_modulKimlik = karslik.i_modulKimlik.Value;

                                    sayf.sayfaBasligi = gelen.baglilar[i].bysMenuAdi;
                                    await sayf.kaydetKos(vari, false);
                                }

                            }

                            await veriTabani.BysMenuCizelgesi.kaydetKos(karslik, vari, false);
                        }
                    }
                    kaydedilecek._sayfaAta(this);
                    await kaydedilecek.kaydetKos(vari, true);

                    List<BysMenu> altlar = await vari.BysMenus.Where(p => p.i_ustMenuKimlik == kaydedilecek.bysMenuKimlik && p.sirasi >= 0).OrderBy(p => p.sirasi).ToListAsync();
                    for (int i = 0; i < altlar.Count; i++)
                    {
                        altlar[i].sirasi = (i + 1) * 10;
                        await veriTabani.BysMenuCizelgesi.kaydetKos(altlar[i], vari, false);
                    }
                    List<BysMenu> ustler = await vari.BysMenus.Where(p => p.e_modulSayfasimi == true && p.sirasi >= 0).OrderBy(p => p.sirasi).ToListAsync();
                    for (int i = 0; i < ustler.Count; i++)
                    {
                        ustler[i].sirasi = (i + 1) * 10;
                        await veriTabani.BysMenuCizelgesi.kaydetKos(ustler[i], vari, false);
                    }
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
