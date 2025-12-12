using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;
namespace UniStaj.Models
{
    public class KullaniciRoluModel : ModelTabani
    {
        public KullaniciRolu kartVerisi { get; set; }
        public List<KullaniciRoluAYRINTI> dokumVerisi { get; set; }
        public KullaniciRoluAYRINTIArama aramaParametresi { get; set; }


        public KullaniciRoluModel()
        {
            this.kartVerisi = new KullaniciRolu();
            this.dokumVerisi = new List<KullaniciRoluAYRINTI>();
            this.aramaParametresi = new KullaniciRoluAYRINTIArama();
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
                    KullaniciRolu? silinecek = await KullaniciRolu.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.KullaniciRoluModel modeli = new Models.KullaniciRoluModel();
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




        public async Task<KullaniciRolu> kaydetKos(Sayfa sayfasi)
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
                var kart = await KullaniciRolu.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<KullaniciRoluAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public List<KullaniciAYRINTI> _ayKullaniciAYRINTI { get; set; }
        public List<RolAYRINTI> _ayRolAYRINTI { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            _ayKullaniciAYRINTI = await KullaniciAYRINTI.ara(vari);
            _ayRolAYRINTI = await RolAYRINTI.ara(vari);
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                KullaniciRoluAYRINTIArama kosul = new KullaniciRoluAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new KullaniciRolu();
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
                    KullaniciRoluAYRINTIArama kosul = JsonConvert.DeserializeObject<KullaniciRoluAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new KullaniciRoluAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);

                    kartVerisi = new KullaniciRolu();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
