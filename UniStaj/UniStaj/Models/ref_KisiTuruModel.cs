using UniStaj.veri;

namespace UniStaj.Models
{
    public class ref_KisiTuruModel : ModelTabani
    {
        public ref_KisiTuru kartVerisi { get; set; }
        public List<ref_KisiTuru> dokumVerisi { get; set; }


        public ref_KisiTuruModel()
        {
            kartVerisi = new ref_KisiTuru();
            dokumVerisi = new List<ref_KisiTuru>();
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
            kartVerisi = ref_KisiTuru.olustur(kimlik);
            dokumVerisi = new List<ref_KisiTuru>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new ref_KisiTuru();
            dokumVerisi = ref_KisiTuru.ara();
        }


    }
}
