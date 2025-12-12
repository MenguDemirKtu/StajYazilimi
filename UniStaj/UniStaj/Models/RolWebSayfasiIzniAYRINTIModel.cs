using UniStaj.veri;

namespace UniStaj.Models
{
    public class RolWebSayfasiIzniAYRINTIModel : ModelTabani
    {
        public RolWebSayfasiIzniAYRINTI kartVerisi { get; set; }
        public List<RolWebSayfasiIzniAYRINTI> dokumVerisi { get; set; }

        public void veriCek(Yonetici kime, long kimlik)
        {
            yenimiBelirle(kimlik);
            kartVerisi = RolWebSayfasiIzniAYRINTI.olustur(kimlik);
            dokumVerisi = new List<RolWebSayfasiIzniAYRINTI>();
        }
        public void veriCekKosut(Yonetici kime)
        {
            dokumVerisi = RolWebSayfasiIzniAYRINTI.ara();
        }
    }
}
