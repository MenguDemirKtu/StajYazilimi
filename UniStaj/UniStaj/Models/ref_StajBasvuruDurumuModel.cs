using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;
namespace UniStaj.Models
{
    public class ref_StajBasvuruDurumuModel : ModelTabani
    {
        public ref_StajBasvuruDurumu kartVerisi { get; set; }
        public List<ref_StajBasvuruDurumu> dokumVerisi { get; set; }
        public ref_StajBasvuruDurumuArama aramaParametresi { get; set; }


        public ref_StajBasvuruDurumuModel()
        {
            this.kartVerisi = new ref_StajBasvuruDurumu();
            this.dokumVerisi = new List<ref_StajBasvuruDurumu>();
            this.aramaParametresi = new ref_StajBasvuruDurumuArama();
        }


        public async Task<AramaTalebi> ayrintiliAraKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                AramaTalebi talep = new AramaTalebi();
                talep.kodu = Guid.NewGuid().ToString();
                talep.tarih = DateTime.Now;
                talep.varmi = true;
                talep.talepAyrintisi = Newtonsoft.Json.JsonConvert.SerializeObject(aramaParametresi);
                await veriTabani.AramaTalebiCizelgesi.kaydetKos(talep, vari, false);
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


        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await ref_StajBasvuruDurumu.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<ref_StajBasvuruDurumu>();
                await baglilariCek(vari, kime);
            }
        }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                ref_StajBasvuruDurumuArama kosul = new ref_StajBasvuruDurumuArama();
                kartVerisi = new ref_StajBasvuruDurumu();
                dokumVerisi = await kosul.cek(vari);
                await baglilariCek(vari, kime);
            }
        }
        public async Task kosulaGoreCek(Yonetici kime, string id)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                var talep = vari.AramaTalebis.FirstOrDefault(p => p.kodu == id);
                if (talep != null)
                {
                    ref_StajBasvuruDurumuArama kosul = JsonConvert.DeserializeObject<ref_StajBasvuruDurumuArama>(talep.talepAyrintisi ?? "") ?? new ref_StajBasvuruDurumuArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new ref_StajBasvuruDurumu();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
