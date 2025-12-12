using UniStaj.veri;

namespace UniStaj.Models
{
    public class HataYazismasiModel : ModelTabani
    {
        public HataYazismasi kartVerisi { get; set; }
        public List<HataYazismasi> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = HataYazismasi.olustur(kimlik);
            dokumVerisi = new List<HataYazismasi>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new HataYazismasi();
            dokumVerisi = HataYazismasi.ara();
        }


    }
}