using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;
namespace UniStaj.Models
{
    public class StajBirimAsamasiModel : ModelTabani
    {
        public StajBirimAsamasi kartVerisi { get; set; }
        public List<StajBirimAsamasiAYRINTI> dokumVerisi { get; set; }
        public StajBirimAsamasiAYRINTIArama aramaParametresi { get; set; }


        public StajBirimAsamasiModel()
        {
            this.kartVerisi = new StajBirimAsamasi();
            this.dokumVerisi = new List<StajBirimAsamasiAYRINTI>();
            this.aramaParametresi = new StajBirimAsamasiAYRINTIArama();
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
                    StajBirimAsamasi? silinecek = await StajBirimAsamasi.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.StajBirimAsamasiModel modeli = new Models.StajBirimAsamasiModel();
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




        public async Task<StajBirimAsamasi> kaydetKos(Sayfa sayfasi)
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
                var kart = await StajBirimAsamasi.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<StajBirimAsamasiAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public List<StajBirimiAYRINTI> _ayStajBirimiAYRINTI { get; set; }
        public List<ref_StajAsamasi> _ayref_StajAsamasi { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            _ayStajBirimiAYRINTI = await StajBirimiAYRINTI.ara(vari);
            _ayref_StajAsamasi = await ref_StajAsamasi.ara(vari);
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                StajBirimAsamasiAYRINTIArama kosul = new StajBirimAsamasiAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new StajBirimAsamasi();
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
                    StajBirimAsamasiAYRINTIArama kosul = JsonConvert.DeserializeObject<StajBirimAsamasiAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new StajBirimAsamasiAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new StajBirimAsamasi();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
