using UniStaj.veri;

namespace UniStaj.Models
{
    public class FederasyonBildirimGondermeAyariModel : ModelTabani
    {
        public FederasyonBildirimGondermeAyari kartVerisi { get; set; }
        public List<FederasyonBildirimGondermeAyariAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = FederasyonBildirimGondermeAyari.olustur(kimlik);
            dokumVerisi = new List<FederasyonBildirimGondermeAyariAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new FederasyonBildirimGondermeAyari();
            dokumVerisi = FederasyonBildirimGondermeAyariAYRINTI.ara();
        }


    }
}
