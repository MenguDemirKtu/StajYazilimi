using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;
namespace UniStaj.Models
{
    public class StajDonemiModel : ModelTabani
    {
        public StajDonemi kartVerisi { get; set; }
        public List<StajDonemiAYRINTI> dokumVerisi { get; set; }
        public StajDonemiAYRINTIArama aramaParametresi { get; set; }


        public StajDonemiModel()
        {
            this.kartVerisi = new StajDonemi();
            this.dokumVerisi = new List<StajDonemiAYRINTI>();
            this.aramaParametresi = new StajDonemiAYRINTIArama();
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
        public async Task silKos(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    StajDonemi? silinecek = await StajDonemi.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.StajDonemiModel modeli = new Models.StajDonemiModel();
            await modeli.veriCekKos(silen);
        }
        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }




        public async Task<StajDonemi> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);
                return kartVerisi;
            }
        }


        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await StajDonemi.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<StajDonemiAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                StajDonemiAYRINTIArama kosul = new StajDonemiAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new StajDonemi();
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
                    StajDonemiAYRINTIArama kosul = JsonConvert.DeserializeObject<StajDonemiAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new StajDonemiAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new StajDonemi();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
