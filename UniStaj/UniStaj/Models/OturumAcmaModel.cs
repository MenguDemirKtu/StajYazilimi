using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class OturumAcmaModel : ModelTabani
    {
        public OturumAcma kartVerisi { get; set; }
        public List<OturumAcmaAYRINTI> dokumVerisi { get; set; }
        public OturumAcmaAYRINTIArama aramaParametresi { get; set; }


        public OturumAcmaModel()
        {
            this.kartVerisi = new OturumAcma();
            this.dokumVerisi = new List<OturumAcmaAYRINTI>();
            this.aramaParametresi = new OturumAcmaAYRINTIArama();
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
                    OturumAcma? silinecek = await OturumAcma.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.OturumAcmaModel modeli = new Models.OturumAcmaModel();
            await modeli.veriCekKos(silen);
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


        public OturumAcma kaydet(Sayfa sayfasi)
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


        public async Task<OturumAcma> kaydetKos(Sayfa sayfasi)
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


        public void sil(Sayfa sayfasi, string id, Yonetici silen)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                List<string> kayitlar = id.Split(',').ToList();
                for (int i = 0; i < kayitlar.Count; i++)
                {
                    OturumAcma silinecek = OturumAcma.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.OturumAcmaModel modeli = new Models.OturumAcmaModel();
            modeli.veriCekKosut(silen);
        }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await OturumAcma.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<OturumAcmaAYRINTI>();
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                OturumAcmaAYRINTIArama kosul = new OturumAcmaAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new OturumAcma();
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
                    OturumAcmaAYRINTIArama kosul = JsonConvert.DeserializeObject<OturumAcmaAYRINTIArama>(talep.talepAyrintisi) ?? new OturumAcmaAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new OturumAcma();
                    aramaParametresi = kosul;
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = OturumAcma.olustur(kimlik);
            dokumVerisi = new List<OturumAcmaAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            this.kullanan = kime;
            kartVerisi = new OturumAcma();
            dokumVerisi = OturumAcmaAYRINTI.ara();
        }


    }
}
