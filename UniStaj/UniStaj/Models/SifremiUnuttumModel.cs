using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class SifremiUnuttumModel : ModelTabani
    {
        public SifremiUnuttum kartVerisi { get; set; }
        public List<SifremiUnuttumAYRINTI> dokumVerisi { get; set; }
        public SifremiUnuttumAYRINTIArama aramaParametresi { get; set; }


        public SifremiUnuttumModel()
        {
            this.kartVerisi = new SifremiUnuttum();
            this.dokumVerisi = new List<SifremiUnuttumAYRINTI>();
            this.aramaParametresi = new SifremiUnuttumAYRINTIArama();
        }

        public void bos()
        {
            kartVerisi = new SifremiUnuttum();
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
                    SifremiUnuttum? silinecek = await SifremiUnuttum.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.SifremiUnuttumModel modeli = new Models.SifremiUnuttumModel();
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


        public SifremiUnuttum kaydet(Sayfa sayfasi)
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


        public async Task<SifremiUnuttum> kaydetKos(Sayfa sayfasi)
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
                    SifremiUnuttum silinecek = SifremiUnuttum.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.SifremiUnuttumModel modeli = new Models.SifremiUnuttumModel();
            modeli.veriCekKosut(silen);
        }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await SifremiUnuttum.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<SifremiUnuttumAYRINTI>();
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                SifremiUnuttumAYRINTIArama kosul = new SifremiUnuttumAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new SifremiUnuttum();
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
                    SifremiUnuttumAYRINTIArama kosul = JsonConvert.DeserializeObject<SifremiUnuttumAYRINTIArama>(talep.talepAyrintisi) ?? new SifremiUnuttumAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new SifremiUnuttum();
                    aramaParametresi = kosul;
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = SifremiUnuttum.olustur(kimlik);
            dokumVerisi = new List<SifremiUnuttumAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            this.kullanan = kime;
            kartVerisi = new SifremiUnuttum();
            dokumVerisi = SifremiUnuttumAYRINTI.ara();
        }


    }
}
