using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class AramaTalebiModel : ModelTabani
    {
        public AramaTalebi kartVerisi { get; set; }
        public List<AramaTalebiAYRINTI> dokumVerisi { get; set; }
        public AramaTalebiAYRINTIArama aramaParametresi { get; set; }


        public AramaTalebiModel()
        {
            kartVerisi = new AramaTalebi();
            dokumVerisi = new List<AramaTalebiAYRINTI>();
            aramaParametresi = new AramaTalebiAYRINTIArama();
        }


        public AramaTalebi ayrintiliAra(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                AramaTalebi talep = new AramaTalebi();
                talep.kodu = Guid.NewGuid().ToString();
                talep.tarih = DateTime.Now;
                talep.varmi = true;
                talep.talepAyrintisi = Newtonsoft.Json.JsonConvert.SerializeObject(aramaParametresi);
                talep.kaydet(vari, false);
                return talep;
            }
        }
        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }


        public AramaTalebi kaydet(Sayfa sayfasi)
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
                    AramaTalebi silinecek = AramaTalebi.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.AramaTalebiModel modeli = new Models.AramaTalebiModel();
            modeli.veriCekKosut(silen);
        }

        public void kosulaGoreCek(Yonetici kime, string id)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                var talep = vari.AramaTalebis.FirstOrDefault(p => p.kodu == id);
                if (talep != null)
                {
                    AramaTalebiAYRINTIArama kosul = JsonConvert.DeserializeObject<AramaTalebiAYRINTIArama>(talep.talepAyrintisi);
                    dokumVerisi = AramaTalebiAYRINTIArama.ara(vari, kosul);
                    kartVerisi = new AramaTalebi();
                    aramaParametresi = kosul;
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = AramaTalebi.olustur(kimlik);
            dokumVerisi = new List<AramaTalebiAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new AramaTalebi();
            dokumVerisi = AramaTalebiAYRINTI.ara();
        }


    }
}
