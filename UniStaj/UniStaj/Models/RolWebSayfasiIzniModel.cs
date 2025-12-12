using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.Models
{
    public class RolWebSayfasiIzniModel : ModelTabani
    {
        public Rol? rolu { get; set; }
        public RolWebSayfasiIzni kartVerisi { get; set; }
        public List<RolWebSayfasiIzniAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            yenimiBelirle(kimlik);
            rolu = Rol.olustur(kimlik);
            dokumVerisi = new List<RolWebSayfasiIzniAYRINTI>();
        }


        public async Task veriCekKos(Yonetici kim, string id)
        {
            using (veri.Varlik vari = new Varlik())
            {
                rolu = await vari.Rols.FirstOrDefaultAsync(p => p.kodu == id);
                kartVerisi = new RolWebSayfasiIzni();
                if (rolu == null)
                    throw new Exception("Rol bulunamadý");
                kartVerisi.i_rolKimlik = rolu.rolKimlik;
                dokumVerisi = await vari.RolWebSayfasiIzniAYRINTIs.Where(p => p.i_rolKimlik == rolu.rolKimlik).ToListAsync();
            }
        }

        public void veriCekKosut(Yonetici kime)
        {
            dokumVerisi = RolWebSayfasiIzniAYRINTI.ara();
        }
        public void olustur(int rolKimlik)
        {
            List<WebSayfasi> sayfalar = WebSayfasi.ara(p => p.e_izinSayfasindaGorunsunmu == true);
            List<RolWebSayfasiIzni> izinler = RolWebSayfasiIzni.ara(p => p.i_rolKimlik == rolKimlik);

            veri.Varlik vari = new Varlik();
            for (int i = 0; i < sayfalar.Count; i++)
            {
                int yer = izinler.FindIndex(p => p.i_webSayfasiKimlik == sayfalar[i].webSayfasiKimlik);
                if (yer == -1)
                {
                    RolWebSayfasiIzni yeni = new RolWebSayfasiIzni();
                    yeni._varSayilan();
                    yeni.varmi = true;
                    yeni.i_rolKimlik = rolKimlik;
                    yeni.i_webSayfasiKimlik = sayfalar[i].webSayfasiKimlik;
                    yeni.e_eklemeIzniVarmi = false;
                    yeni.e_silmeIzniVarmi = false;
                    yeni.e_guncellemeIzniVarmi = false;
                    yeni.e_gormeIzniVarmi = false;

                    izinler.Add(yeni);
                    vari.RolWebSayfasiIznis.Add(yeni);
                    //yeni.kaydet(false, false);


                }
            }
            vari.SaveChanges();
            rolu = Rol.olustur(rolKimlik);
            dokumVerisi = RolWebSayfasiIzniAYRINTI.ara(p => p.i_rolKimlik == rolKimlik && p.e_izinSayfasindaGorunsunmu == true).OrderBy(p => p.sayfaBasligi).OrderBy(p => p.modulAdi).ThenBy(p => p.sayfaBasligi).ToList();

        }
    }
}
