using UniStaj.veri;

namespace UniStaj.Models
{
    public class BelgeGorModel : ModelTabani
    {
        public YuklenenDosyalarAYRINTI dosyasi { get; set; }

        public BelgeGorModel()
        {
            dosyasi = new YuklenenDosyalarAYRINTI();
        }
        public void veriCek(Yonetici kime, string kod)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                dosyasi = vari.YuklenenDosyalarAYRINTIs.FirstOrDefault(p => p.kodu == kod) ?? throw new Exception("Bulunamadı");
            }
        }
    }
}
