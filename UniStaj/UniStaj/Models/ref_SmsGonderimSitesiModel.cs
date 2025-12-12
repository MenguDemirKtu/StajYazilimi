using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class ref_SmsGonderimSitesiModel : ModelTabani
    {
        public ref_SmsGonderimSitesi kartVerisi { get; set; }
        public List<ref_SmsGonderimSitesi> dokumVerisi { get; set; }
        public ref_SmsGonderimSitesiArama aramaParametresi { get; set; }


        public ref_SmsGonderimSitesiModel()
        {
            this.kartVerisi = new ref_SmsGonderimSitesi();
            this.dokumVerisi = new List<ref_SmsGonderimSitesi>();
            this.aramaParametresi = new ref_SmsGonderimSitesiArama();
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
                var kart = await ref_SmsGonderimSitesi.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<ref_SmsGonderimSitesi>();
                await baglilariCek(vari);
            }
        }

        public async Task baglilariCek(veri.Varlik vari) { }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                ref_SmsGonderimSitesiArama kosul = new ref_SmsGonderimSitesiArama();
                kartVerisi = new ref_SmsGonderimSitesi();
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
                    ref_SmsGonderimSitesiArama kosul = JsonConvert.DeserializeObject<ref_SmsGonderimSitesiArama>(talep.talepAyrintisi ?? "") ?? new ref_SmsGonderimSitesiArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new ref_SmsGonderimSitesi();
                    await baglilariCek(vari);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
