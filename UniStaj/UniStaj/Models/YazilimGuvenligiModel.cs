using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class YazilimGuvenligiModel : ModelTabani
    {
        public YazilimGuvenligi kartVerisi { get; set; }
        public List<YazilimGuvenligiAYRINTI> dokumVerisi { get; set; }
        public YazilimGuvenligiAYRINTIArama aramaParametresi { get; set; }


        public YazilimGuvenligiModel()
        {
            kartVerisi = new YazilimGuvenligi();
            dokumVerisi = new List<YazilimGuvenligiAYRINTI>();
            aramaParametresi = new YazilimGuvenligiAYRINTIArama();
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
                    YazilimGuvenligi? silinecek = await YazilimGuvenligi.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.YazilimGuvenligiModel modeli = new Models.YazilimGuvenligiModel();
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


        public YazilimGuvenligi kaydet(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi.pythonKodu = Sayfa.dosyaKonumDuzelt(kartVerisi.pythonKodu, Genel.yazilimAyari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.kaydet(true);
                return kartVerisi;
            }
        }


        public async Task<YazilimGuvenligi> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi.pythonKodu = Sayfa.dosyaKonumDuzelt(kartVerisi.pythonKodu, Genel.yazilimAyari);
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
                    YazilimGuvenligi silinecek = YazilimGuvenligi.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.YazilimGuvenligiModel modeli = new Models.YazilimGuvenligiModel();
            modeli.veriCekKosut(silen);
        }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await YazilimGuvenligi.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<YazilimGuvenligiAYRINTI>();
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                YazilimGuvenligiAYRINTIArama kosul = new YazilimGuvenligiAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new YazilimGuvenligi();
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
                    YazilimGuvenligiAYRINTIArama kosul = JsonConvert.DeserializeObject<YazilimGuvenligiAYRINTIArama>(talep.talepAyrintisi) ?? new YazilimGuvenligiAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new YazilimGuvenligi();
                    aramaParametresi = kosul;
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = YazilimGuvenligi.olustur(kimlik);
            dokumVerisi = new List<YazilimGuvenligiAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new YazilimGuvenligi();
            dokumVerisi = YazilimGuvenligiAYRINTI.ara();
        }


    }
}
