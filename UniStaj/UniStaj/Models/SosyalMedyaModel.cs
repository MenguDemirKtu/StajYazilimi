using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class SosyalMedyaModel : ModelTabani
    {
        public SosyalMedya kartVerisi { get; set; }
        public List<SosyalMedyaAYRINTI> dokumVerisi { get; set; }
        public SosyalMedyaAYRINTIArama aramaParametresi { get; set; }
        public SosyalMedyaModel()
        {
            this.kartVerisi = new SosyalMedya();
            this.dokumVerisi = new List<SosyalMedyaAYRINTI>();
            this.aramaParametresi = new SosyalMedyaAYRINTIArama();
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
                    SosyalMedya? silinecek = await SosyalMedya.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.SosyalMedyaModel modeli = new Models.SosyalMedyaModel();
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



        public async Task<SosyalMedya> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                long fk = 0;
                if (long.TryParse(fotoKonumu, out fk))
                    kartVerisi.i_fotoKimlik = fk;
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);
                await fotoBicimlendirKos(vari, kartVerisi, kartVerisi.i_fotoKimlik);
                return kartVerisi;
            }
        }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await SosyalMedya.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<SosyalMedyaAYRINTI>();
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                SosyalMedyaAYRINTIArama kosul = new SosyalMedyaAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new SosyalMedya();
                dokumVerisi = await kosul.cek(vari);
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
                    SosyalMedyaAYRINTIArama kosul = JsonConvert.DeserializeObject<SosyalMedyaAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new SosyalMedyaAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new SosyalMedya();
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
