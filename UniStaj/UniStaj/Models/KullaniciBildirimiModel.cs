using UniStaj.veri;

namespace UniStaj.Models
{
    public class KullaniciBildirimiModel : ModelTabani
    {
        public KullaniciBildirimi kartVerisi { get; set; }
        public List<KullaniciBildirimiAYRINTI> dokumVerisi { get; set; }
        public KullaniciBildirimiAYRINTI dokumVerisi2 { get; set; }



        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            veri.Varlik vari = new Varlik();
            kartVerisi = KullaniciBildirimi.olustur(kimlik);
            dokumVerisi = new List<KullaniciBildirimiAYRINTI>();
        }
        public void veriCek2(Yonetici kime, string url)
        {
            kullanan = kime;
            veri.Varlik vari = new Varlik();
            dokumVerisi2 = vari.KullaniciBildirimiAYRINTIs.FirstOrDefault(q => q.url == url);

        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new KullaniciBildirimi();
            dokumVerisi = KullaniciBildirimiAYRINTI.ara(q => q.i_kullaniciKimlik == kime.kullaniciKimlik && q.e_goruldumu == 0);
        }


    }
}
