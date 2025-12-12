using UniStaj.veri;

namespace UniStaj.Models
{
    public class ref_RolIslemiModel : ModelTabani
    {
        public ref_RolIslemi kartVerisi { get; set; }
        public List<ref_RolIslemi> dokumVerisi { get; set; }


        public ref_RolIslemiModel()
        {
            kartVerisi = new ref_RolIslemi();
            dokumVerisi = new List<ref_RolIslemi>();
        }


        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = ref_RolIslemi.olustur(kimlik);
            dokumVerisi = new List<ref_RolIslemi>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new ref_RolIslemi();
            dokumVerisi = ref_RolIslemi.ara();
        }


    }
}
