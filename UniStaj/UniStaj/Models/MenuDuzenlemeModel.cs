using UniStaj.veri;

namespace UniStaj.Models
{
    public class MenuDuzenlemeModel : ModelTabani
    {
        public List<Modul> moduller { get; set; }
        public List<BysMenuAYRINTI> menuler { get; set; }


        public MenuDuzenlemeModel()
        {
            moduller = new List<Modul>();
            menuler = new List<BysMenuAYRINTI>();
        }

        public async Task kaydet()
        {
            using (veri.Varlik vari = new Varlik())
            {
                List<BysMenu> tamami = new List<BysMenu>();

                tamami = vari.BysMenus.ToList();

                for (int i = 0; i < menuler.Count; i++)
                {
                    var siradaki = menuler[i];
                    var karsilik = tamami.FirstOrDefault(p => p.bysMenuKimlik == siradaki.bysMenuKimlik);
                    if (karsilik != null)
                    {
                        karsilik.sirasi = siradaki.sirasi;
                        karsilik.e_gosterilsimmi = siradaki.e_gosterilsimmi;
                        karsilik.kaydet(vari, false);
                    }
                }
            }

            Models.YenileModel model = new Models.YenileModel();
            await model.yenileKos();
        }
        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            menuler = new List<BysMenuAYRINTI>();
            using (veri.Varlik vari = new Varlik())
            {
                List<BysMenuAYRINTI> tumu = vari.BysMenuAYRINTIs.Where(p => p.varmi == true).ToList();
                var modulSayfalari = tumu.Where(p => p.i_ustMenuKimlik == 0).OrderBy(p => p.sirasi).ToList();






                // menuler.AddRange(modulSayfalari);
                for (int i = 0; i < modulSayfalari.Count; i++)
                {
                    menuler.Add(modulSayfalari[i]);
                    var alt = tumu.Where(p => p.i_ustMenuKimlik == modulSayfalari[i].bysMenuKimlik).OrderBy(p => p.sirasi).ToList();
                    menuler.AddRange(alt);
                }
            }
        }
    }
}
