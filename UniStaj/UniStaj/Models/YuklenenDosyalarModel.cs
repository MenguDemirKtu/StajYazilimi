using UniStaj.veri;

namespace UniStaj.Models
{
    public class YuklenenDosyalarModel : ModelTabani
    {
        public YuklenenDosyalar kartVerisi { get; set; }
        public List<YuklenenDosyalarAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = YuklenenDosyalar.olustur(kimlik);
            dokumVerisi = new List<YuklenenDosyalarAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new YuklenenDosyalar();
            dokumVerisi = YuklenenDosyalarAYRINTI.ara();
        }


    }
}
