using UniStaj.veri;

namespace UniStaj.Models
{
    public class ref_KullaniciTuruModel : ModelTabani
    {
        public ref_KullaniciTuru kartVerisi { get; set; }
        public List<ref_KullaniciTuru> dokumVerisi { get; set; }


        public ref_KullaniciTuruModel()
        {
            kartVerisi = new ref_KullaniciTuru();
            dokumVerisi = new List<ref_KullaniciTuru>();
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
            kartVerisi = ref_KullaniciTuru.olustur(kimlik);
            dokumVerisi = new List<ref_KullaniciTuru>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new ref_KullaniciTuru();
            dokumVerisi = ref_KullaniciTuru.ara();
        }


    }
}
