using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class YazilimAyariModel : ModelTabani
    {
        public YazilimAyari kartVerisi { get; set; }
        public List<YazilimAyariAYRINTI> dokumVerisi { get; set; }
        public YazilimAyariAYRINTIArama aramaParametresi { get; set; }


        public YazilimAyariModel()
        {
            this.kartVerisi = new YazilimAyari();
            this.dokumVerisi = new List<YazilimAyariAYRINTI>();
            this.aramaParametresi = new YazilimAyariAYRINTIArama();
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
                    YazilimAyari? silinecek = await YazilimAyari.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.YazilimAyariModel modeli = new Models.YazilimAyariModel();
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




        public async Task<YazilimAyari> kaydetKos(Sayfa sayfasi)
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
                var kart = await YazilimAyari.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<YazilimAyariAYRINTI>();
                await baglilariCek(vari);
                await fotoAyariBelirle(vari, kartVerisi._cizelgeAdi());
            }
        }

        public List<ref_SmsGonderimSitesi> _ayref_SmsGonderimSitesi { get; set; }
        public List<ref_OturumAcmaTuru> _ayref_OturumAcmaTuru { get; set; }
        public async Task baglilariCek(veri.Varlik vari)
        {
            _ayref_SmsGonderimSitesi = await ref_SmsGonderimSitesi.ara(vari);
            _ayref_OturumAcmaTuru = await ref_OturumAcmaTuru.ara(vari);
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                YazilimAyariAYRINTIArama kosul = new YazilimAyariAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new YazilimAyari();
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
                    YazilimAyariAYRINTIArama kosul = JsonConvert.DeserializeObject<YazilimAyariAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new YazilimAyariAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new YazilimAyari();
                    await baglilariCek(vari);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
