using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.Models
{
    public class HataBildirimlerimModel : ModelTabani
    {
        public long i_hataBildirimiKimlik { get; set; }
        public List<HataBildirimiAYRINTI>? dokumVerisi { get; set; }
        public List<HataYazismasiAYRINTI>? yazismalari { get; set; }


        public HataBildirimi? anaBildirim { get; set; }

        public string? aciklama { get; set; }

        public void veriCek(Yonetici kime, string kod)
        {
            kullanan = kime;
            anaBildirim = HataBildirimi.ara(p => p.kodu == kod)[0];
        }
        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            dokumVerisi = HataBildirimiAYRINTI.ara(p => p.i_yoneticiKimlik == kime.kullaniciKimlik).OrderByDescending(p => p.i_hataBildirimDurumuKimlik).ToList();
        }
        public async Task veriCekKos(Yonetici kime)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
                dokumVerisi = await vari.HataBildirimiAYRINTIs.Where(p => p.i_yoneticiKimlik == kime.kullaniciKimlik).OrderByDescending(p => p.i_hataBildirimDurumuKimlik).ToListAsync();
        }
        public void tumCozulmemisler(Yonetici kime)
        {
            kullanan = kime;
            int durum = (int)enumref_HataBildirimDurumu.Sorun_cozuldu;
            dokumVerisi = HataBildirimiAYRINTI.ara(p => p.i_hataBildirimDurumuKimlik != durum).OrderByDescending(p => p.oncelik).ThenByDescending(p => p.i_hataBildirimDurumuKimlik).ThenByDescending(p => p.tarih).ToList();
        }

        public void yazismaCek(Yonetici kime, string kod)
        {
            kullanan = kime;
            HataBildirimi bil = HataBildirimi.ara(p => p.kodu == kod)[0];
            i_hataBildirimiKimlik = bil.hataBildirimiKimlik;
            yazismalari = HataYazismasiAYRINTI.ara(p => p.i_hataBildirimiKimlik == bil.hataBildirimiKimlik).OrderBy(p => p.tarih).ToList();
        }
    }
}
