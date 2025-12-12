using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.Models
{
    public class YetkiDegistirModel : ModelTabani
    {
        public List<KullaniciAYRINTI> yetkileri { get; set; }


        public async Task yetkileriCek(Yonetici _kim)
        {
            this.kullanan = _kim;
            using (veri.Varlik vari = new Varlik())
            {
                yetkileri = await vari.KullaniciAYRINTIs.Where(p => p.tcKimlikNo == _kim.tcKimlikNo && p.e_faalmi == true).ToListAsync();
            }
        }

        public YetkiDegistirModel()
        {
            yetkileri = new List<KullaniciAYRINTI>();
        }
    }
}
