using UniStaj.veri;

namespace UniStaj.Models
{
    public class FotografModel : ModelTabani
    {
        public Fotograf kartVerisi { get; set; }
        public List<FotografAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = Fotograf.olustur(kimlik);
            dokumVerisi = new List<FotografAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new Fotograf();
            dokumVerisi = FotografAYRINTI.ara();
        }


    }
}
