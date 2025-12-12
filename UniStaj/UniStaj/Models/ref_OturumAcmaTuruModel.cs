using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class ref_OturumAcmaTuruModel : ModelTabani
    {
        public ref_OturumAcmaTuru kartVerisi { get; set; }
        public List<ref_OturumAcmaTuru> dokumVerisi { get; set; }
        public ref_OturumAcmaTuruArama aramaParametresi { get; set; }


        public ref_OturumAcmaTuruModel()
        {
            this.kartVerisi = new ref_OturumAcmaTuru();
            this.dokumVerisi = new List<ref_OturumAcmaTuru>();
            this.aramaParametresi = new ref_OturumAcmaTuruArama();
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
                var kart = await ref_OturumAcmaTuru.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<ref_OturumAcmaTuru>();
                await baglilariCek(vari);
            }
        }

        public List<ref_OturumAcmaTuru> _ayref_OturumAcmaTuru { get; set; }
        public async Task baglilariCek(veri.Varlik vari) { _ayref_OturumAcmaTuru = await ref_OturumAcmaTuru.ara(vari); }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                ref_OturumAcmaTuruArama kosul = new ref_OturumAcmaTuruArama();
                kartVerisi = new ref_OturumAcmaTuru();
                dokumVerisi = await kosul.cek(vari);
                await baglilariCek(vari);
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
                    ref_OturumAcmaTuruArama kosul = JsonConvert.DeserializeObject<ref_OturumAcmaTuruArama>(talep.talepAyrintisi ?? "") ?? new ref_OturumAcmaTuruArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new ref_OturumAcmaTuru();
                    await baglilariCek(vari);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
