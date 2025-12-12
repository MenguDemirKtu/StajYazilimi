using UniStaj.veri;

namespace UniStaj.Models
{
    public class KullaniciRoluModel : ModelTabani
    {
        public KullaniciRolu kartVerisi { get; set; }
        public List<KullaniciRoluAYRINTI> dokumVerisi { get; set; }


        public KullaniciRoluModel()
        {
            kartVerisi = new KullaniciRolu();
            dokumVerisi = new List<KullaniciRoluAYRINTI>();
        }

        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = KullaniciRolu.olustur(kimlik);
            dokumVerisi = new List<KullaniciRoluAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new KullaniciRolu();
            dokumVerisi = KullaniciRoluAYRINTI.ara();
        }
    }
}
