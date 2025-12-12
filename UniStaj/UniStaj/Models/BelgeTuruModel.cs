using UniStaj.veri;

namespace UniStaj.Models
{
    public class BelgeTuruModel : ModelTabani
    {
        public BelgeTuru kartVerisi { get; set; }
        public List<BelgeTuruAYRINTI> dokumVerisi { get; set; }


        public BelgeTuruModel()
        {
            kartVerisi = new BelgeTuru();
            dokumVerisi = new List<BelgeTuruAYRINTI>();
        }


        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }


        public BelgeTuru kaydet(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.kaydet(true);
                return kartVerisi;
            }
        }


        public void sil(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    BelgeTuru silinecek = BelgeTuru.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.BelgeTuruModel modeli = new Models.BelgeTuruModel();
            modeli.veriCekKosut(silen);
        }

        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = BelgeTuru.olustur(kimlik);
            dokumVerisi = new List<BelgeTuruAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new BelgeTuru();
            dokumVerisi = BelgeTuruAYRINTI.ara();
        }


    }
}
