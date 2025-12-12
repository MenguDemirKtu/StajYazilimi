using UniStaj.veri;

namespace UniStaj.Models
{
    public class GonderilenEPostaModel : ModelTabani
    {
        public GonderilenEPosta kartVerisi { get; set; }
        public List<GonderilenEPostaAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = GonderilenEPosta.olustur(kimlik);
            dokumVerisi = new List<GonderilenEPostaAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new GonderilenEPosta();
            dokumVerisi = GonderilenEPostaAYRINTI.ara();
        }


    }
}
