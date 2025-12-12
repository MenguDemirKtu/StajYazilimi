using UniStaj.veri;

namespace UniStaj.Models
{
    public class ref_DilModel : ModelTabani
    {
        public ref_Dil kartVerisi { get; set; }
        public List<ref_Dil> dokumVerisi { get; set; }


        public ref_DilModel()
        {
            kartVerisi = new ref_Dil();
            dokumVerisi = new List<ref_Dil>();
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
            kartVerisi = ref_Dil.olustur(kimlik);
            dokumVerisi = new List<ref_Dil>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new ref_Dil();
            dokumVerisi = ref_Dil.ara();
        }


    }
}
