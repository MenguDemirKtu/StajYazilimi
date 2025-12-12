using UniStaj.veri;

namespace UniStaj.Models
{
    public class ref_CinsiyetModel : ModelTabani
    {
        public ref_Cinsiyet kartVerisi { get; set; }
        public List<ref_Cinsiyet> dokumVerisi { get; set; }


        public ref_CinsiyetModel()
        {
            kartVerisi = new ref_Cinsiyet();
            dokumVerisi = new List<ref_Cinsiyet>();
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
            kartVerisi = ref_Cinsiyet.olustur(kimlik);
            dokumVerisi = new List<ref_Cinsiyet>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new ref_Cinsiyet();
            dokumVerisi = ref_Cinsiyet.ara();
        }


    }
}
