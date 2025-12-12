using UniStaj.veri;

namespace UniStaj.Models
{
    public class HataBildirimiModel : ModelTabani
    {
        public HataBildirimi kartVerisi { get; set; }
        public List<HataBildirimiAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = HataBildirimi.olustur(kimlik);
            dokumVerisi = new List<HataBildirimiAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new HataBildirimi();
            dokumVerisi = HataBildirimiAYRINTI.ara();
        }


    }
}