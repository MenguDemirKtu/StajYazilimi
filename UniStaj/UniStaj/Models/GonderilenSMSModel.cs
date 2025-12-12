using UniStaj.veri;

namespace UniStaj.Models
{
    public class GonderilenSMSModel : ModelTabani
    {
        public GonderilenSMS kartVerisi { get; set; }
        public List<GonderilenSMSAYRINTI> dokumVerisi { get; set; }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = GonderilenSMS.olustur(kimlik);
            dokumVerisi = new List<GonderilenSMSAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new GonderilenSMS();
            dokumVerisi = GonderilenSMSAYRINTI.ara();
        }


    }
}
