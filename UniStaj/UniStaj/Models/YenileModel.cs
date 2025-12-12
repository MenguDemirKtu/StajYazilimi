using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.Models
{
    public class YenileModel
    {
        public async Task yenileKos()
        {
            using (veri.Varlik vari = new Varlik())
            {
                List<WebSayfasiAYRINTI> sayfalar = new List<WebSayfasiAYRINTI>();
                sayfalar = await vari.WebSayfasiAYRINTIs.ToListAsync();

                List<BysMenuAYRINTI> menuler = await vari.BysMenuAYRINTIs.Where(p => p.e_gosterilsimmi == true).ToListAsync();

                for (int i = 0; i < sayfalar.Count; i++)
                {
                    var kimi = sayfalar[i];

                    if (kimi.e_izinSayfasindaGorunsunmu == false)
                        continue;

                    if (string.IsNullOrEmpty(kimi.sayfaBasligi))
                        continue;

                    List<BysMenuAYRINTI> liste = menuler.Where(p => p.i_webSayfasiKimlik == sayfalar[i].webSayfasiKimlik).ToList();
                    if (liste.Count > 0)
                        continue;


                    int ustSayfaKimlik = 0;

                    if (kimi.i_modulKimlik != 0)
                    {
                        var ara = await vari.BysMenus.Where(p => p.e_modulSayfasimi == true && p.i_modulKimlik == kimi.i_modulKimlik && p.varmi == true).ToListAsync();
                        if (ara.Count > 0)
                        {
                            ustSayfaKimlik = ara[0].bysMenuKimlik;
                        }
                    }

                    BysMenu yeni = new BysMenu();
                    yeni._varSayilan();
                    yeni.varmi = true;
                    yeni.sirasi = 100;
                    yeni.i_webSayfasiKimlik = kimi.webSayfasiKimlik;
                    yeni.bysMenuUrl = "/" + kimi.hamAdresi;
                    yeni.bysMenuAdi = kimi.sayfaBasligi;
                    yeni.e_gosterilsimmi = true;
                    yeni.i_ustMenuKimlik = ustSayfaKimlik;
                    if (kimi.hamAdresi != null)
                        if (kimi.hamAdresi.IndexOf("/Kart/") != -1)
                            yeni.bysMenuUrl = yeni.bysMenuUrl + "0";

                    yeni.kaydet(vari, false);
                    await veriTabani.BysMenuCizelgesi.kaydetKos(yeni, vari, false);

                }
            }
        }


        public void yenile(veri.Varlik vari)
        {
            List<WebSayfasiAYRINTI> sayfalar = new List<WebSayfasiAYRINTI>();
            sayfalar = vari.WebSayfasiAYRINTIs.ToList();

            List<BysMenuAYRINTI> menuler = vari.BysMenuAYRINTIs.Where(p => p.e_gosterilsimmi == true).ToList();

            for (int i = 0; i < sayfalar.Count; i++)
            {
                var kimi = sayfalar[i];

                if (kimi.e_izinSayfasindaGorunsunmu == false)
                    continue;

                if (string.IsNullOrEmpty(kimi.sayfaBasligi))
                    continue;

                List<BysMenuAYRINTI> liste = menuler.Where(p => p.i_webSayfasiKimlik == sayfalar[i].webSayfasiKimlik).ToList();
                if (liste.Count > 0)
                    continue;


                int ustSayfaKimlik = 0;

                if (kimi.i_modulKimlik != 0)
                {
                    var ara = vari.BysMenus.Where(p => p.e_modulSayfasimi == true && p.i_modulKimlik == kimi.i_modulKimlik && p.varmi == true).ToList();
                    if (ara.Count > 0)
                    {
                        ustSayfaKimlik = ara[0].bysMenuKimlik;
                    }
                }

                BysMenu yeni = new BysMenu();
                yeni._varSayilan();
                yeni.varmi = true;
                yeni.sirasi = 100;
                yeni.i_webSayfasiKimlik = kimi.webSayfasiKimlik;
                yeni.bysMenuUrl = "/" + kimi.hamAdresi;
                yeni.bysMenuAdi = kimi.sayfaBasligi;
                yeni.e_gosterilsimmi = true;
                yeni.i_ustMenuKimlik = ustSayfaKimlik;
                if (kimi.hamAdresi != null)
                    if (kimi.hamAdresi.IndexOf("/Kart/") != -1)
                        yeni.bysMenuUrl = yeni.bysMenuUrl + "0";

                yeni.kaydet(vari, false);
                veriTabani.BysMenuCizelgesi.kaydet(yeni, vari, false);

            }
        }
    }
}