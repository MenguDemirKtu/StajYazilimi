using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;


namespace UniStaj.Models
{
    public class BysMenuModel : ModelTabani
    {
        public BysMenu kartVerisi { get; set; }
        public List<BysMenuAYRINTI> dokumVerisi { get; set; }

        public List<BysMenu> baglilar { get; set; }

        public BysMenuAYRINTIArama aramaParametresi { get; set; }

        public BysMenuModel()
        {
            kartVerisi = new BysMenu();
            dokumVerisi = new List<BysMenuAYRINTI>();
            baglilar = new List<BysMenu>();
            _ayModulAYRINTI = new List<ModulAYRINTI>();
            _ayWebSayfasiAYRINTI = new List<WebSayfasiAYRINTI>();
            aramaParametresi = new BysMenuAYRINTIArama();
            _ustMenuler = new List<BysMenuAYRINTI>();
        }


        public async Task<AramaTalebi> ayrintiliAraKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                AramaTalebi talep = new AramaTalebi();
                talep.kodu = Guid.NewGuid().ToString();
                talep.tarih = DateTime.Now;
                talep.varmi = true;
                talep.talepAyrintisi = Newtonsoft.Json.JsonConvert.SerializeObject(aramaParametresi);
                await veriTabani.AramaTalebiCizelgesi.kaydetKos(talep, vari, false);
                return talep;
            }
        }


        public List<WebSayfasiAYRINTI> _ayWebSayfasiAYRINTI { get; set; }
        public List<ModulAYRINTI> _ayModulAYRINTI { get; set; }

        public List<BysMenuAYRINTI> _ustMenuler { get; set; }
        public async Task baglilariCek(veri.Varlik vari)
        {
            _ayWebSayfasiAYRINTI = await WebSayfasiAYRINTI.ara(vari);
            _ayModulAYRINTI = await vari.ModulAYRINTIs.OrderBy(p => p.modulAdi).ToListAsync();
            _ustMenuler = await vari.BysMenuAYRINTIs.Where(p => p.e_modulSayfasimi == true).OrderBy(p => p.bysMenuAdi).ToListAsync();
            //            bilesenler = bilesenler.Where(p => p.e_modulSayfasimi == true).OrderBy(p => p.bysMenuAdi).ToList();
        }
        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            using (veri.Varlik vari = new Varlik())
            {
                kullanan = kime;
                yenimiBelirle(kimlik);
                kartVerisi = await BysMenu.olusturKos(vari, kimlik);
                dokumVerisi = new List<BysMenuAYRINTI>();
                if (kartVerisi.e_modulSayfasimi == true)
                {

                    baglilar = await vari.BysMenus.Where(p => p.i_ustMenuKimlik == kartVerisi.bysMenuKimlik && p.varmi == true).OrderBy(p => p.sirasi).ToListAsync();

                }
                else
                {
                    baglilar = new List<BysMenu>();
                }
                await baglilariCek(vari);
            }
        }


        public async Task veriCekKosut(Yonetici kime)
        {
            using (veri.Varlik vari = new Varlik())
            {
                kullanan = kime;
                kartVerisi = new BysMenu();
                dokumVerisi = await vari.BysMenuAYRINTIs.ToListAsync();
                dokumVerisi = dokumVerisi.Where(p => p.e_modulSayfasimi == true).OrderBy(p => p.sirasi).ToList();
                baglilar = new List<BysMenu>();
                await baglilariCek(vari);
            }
        }

        public async Task kosulaGoreCek(Yonetici kime, string id)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                var talep = vari.AramaTalebis.FirstOrDefault(p => p.kodu == id);
                if (talep != null)
                {
                    BysMenuAYRINTIArama kosul = JsonConvert.DeserializeObject<BysMenuAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new BysMenuAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new BysMenu();
                    await baglilariCek(vari);
                    aramaParametresi = kosul;
                }
            }
        }


    }

}