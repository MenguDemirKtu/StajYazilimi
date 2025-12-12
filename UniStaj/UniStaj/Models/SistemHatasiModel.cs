using UniStaj.veri;

namespace UniStaj.Models
{
    public class SistemHatasiModel : ModelTabani
    {
        public SistemHatasi kartVerisi { get; set; }
        public List<SistemHatasiAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = SistemHatasi.olustur(kimlik);
            dokumVerisi = new List<SistemHatasiAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new SistemHatasi();
            dokumVerisi = SistemHatasiAYRINTI.ara();
        }


    }
}
