using UniStaj.veri;

namespace UniStaj.Models
{
    public class BYSSozcukAciklamaModel : ModelTabani
    {
        public BYSSozcukAciklama kartVerisi { get; set; }
        public List<BYSSozcukAciklamaAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = BYSSozcukAciklama.olustur(kimlik);
            dokumVerisi = new List<BYSSozcukAciklamaAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new BYSSozcukAciklama();
            dokumVerisi = BYSSozcukAciklamaAYRINTI.ara();
        }


    }
}
