using UniStaj.veri;

namespace UniStaj.Models
{
    public class EPostaKalibiModel : ModelTabani
    {
        public EPostaKalibi kartVerisi { get; set; }
        public List<EPostaKalibiAYRINTI> dokumVerisi { get; set; }

        public EPostaKalibiModel()
        {
            kartVerisi = new EPostaKalibi();
            dokumVerisi = new List<EPostaKalibiAYRINTI>();
        }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = EPostaKalibi.olustur(kimlik);
            dokumVerisi = new List<EPostaKalibiAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new EPostaKalibi();
            dokumVerisi = EPostaKalibiAYRINTI.ara();
        }
    }
}
