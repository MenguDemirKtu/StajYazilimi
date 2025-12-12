using UniStaj.veri;

namespace UniStaj.Models
{
    public class ref_SmsGonderimTuruModel : ModelTabani
    {
        public ref_SmsGonderimTuru kartVerisi { get; set; }
        public List<ref_SmsGonderimTuru> dokumVerisi { get; set; }


        public ref_SmsGonderimTuruModel()
        {
            kartVerisi = new ref_SmsGonderimTuru();
            dokumVerisi = new List<ref_SmsGonderimTuru>();
        }



        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = ref_SmsGonderimTuru.olustur(kimlik);
            dokumVerisi = new List<ref_SmsGonderimTuru>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new ref_SmsGonderimTuru();
            dokumVerisi = ref_SmsGonderimTuru.ara();
        }


    }
}
