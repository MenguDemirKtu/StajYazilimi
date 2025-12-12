using UniStaj.veri;

namespace UniStaj.Models
{
    public class BYSSozcukModel : ModelTabani
    {
        public BYSSozcuk kartVerisi { get; set; }
        public List<BYSSozcukAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = BYSSozcuk.olustur(kimlik);
            dokumVerisi = new List<BYSSozcukAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new BYSSozcuk();
            dokumVerisi = BYSSozcukAYRINTI.ara();
        }


    }
}
