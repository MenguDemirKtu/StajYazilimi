using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.Models
{
    public class WebSayfasiModel : ModelTabani
    {
        public WebSayfasi kartVerisi { get; set; }
        public List<WebSayfasiAYRINTI> dokumVerisi { get; set; }
        public List<RolWebSayfasiIzniAYRINTI> izinler { get; set; }

        public List<BysMenuAYRINTI> menuleri { get; set; }
        public WebSayfasiModel()
        {
            kartVerisi = new WebSayfasi();
            dokumVerisi = new List<WebSayfasiAYRINTI>();
            izinler = new List<RolWebSayfasiIzniAYRINTI>();
            menuleri = new List<BysMenuAYRINTI>();
            ay_BYSMenuAYRINTI = new List<BysMenuAYRINTI>();
        }

        public List<BysMenuAYRINTI> ay_BYSMenuAYRINTI { get; set; }
        public async Task veriCek(Yonetici kime, long kimlik)
        {
            using (veri.Varlik vari = new Varlik())
            {
                kullanan = kime;
                yenimiBelirle(kimlik);
                kartVerisi = await WebSayfasi.olusturKos(vari, kimlik) ?? throw new Exception("Olmadý");
                dokumVerisi = new List<WebSayfasiAYRINTI>();
                izinler = await vari.RolWebSayfasiIzniAYRINTIs.Where(p => p.i_webSayfasiKimlik == kartVerisi.webSayfasiKimlik).ToListAsync();
                menuleri = await vari.BysMenuAYRINTIs.Where(p => p.i_webSayfasiKimlik == kimlik).OrderBy(p => p.sirasi).ToListAsync();
                ay_BYSMenuAYRINTI = await vari.BysMenuAYRINTIs.Where(p => p.e_modulSayfasimi == true).OrderBy(p => p.sirasi).ToListAsync();
            }
        }


        public async Task veriCekKos(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new WebSayfasi();
            using (veri.Varlik vari = new Varlik())
            {
                dokumVerisi = await vari.WebSayfasiAYRINTIs.OrderBy(p => p.sayfaBasligi).ToListAsync();
                List<WebSayfasiAYRINTI> boslar = await vari.WebSayfasiAYRINTIs.Where(p => p.sayfaBasligi == null || p.sayfaBasligi == "").ToListAsync();
                List<WebSayfasiAYRINTI> dolular = await vari.WebSayfasiAYRINTIs.Where(p => p.sayfaBasligi != null && p.sayfaBasligi != "").OrderBy(p => p.sayfaBasligi).ToListAsync();
                dokumVerisi = boslar;
                dokumVerisi.AddRange(dolular);
            }
        }


    }
}
