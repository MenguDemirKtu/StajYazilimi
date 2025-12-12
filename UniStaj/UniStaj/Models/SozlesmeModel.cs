using Microsoft.EntityFrameworkCore; // 
using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class SozlesmeModel : ModelTabani
    {
        public Sozlesme kartVerisi { get; set; }
        public List<SozlesmeAYRINTI> dokumVerisi { get; set; }
        public SozlesmeAYRINTIArama aramaParametresi { get; set; }
        public SozlesmeAYRINTI? sozu { get; set; }

        public string kullaniciKodu { get; set; }

        public bool e_onaylandimi { get; set; }
        public SozlesmeOnayi yeniOnay { get; set; }


        public SozlesmeModel()
        {
            kartVerisi = new Sozlesme();
            dokumVerisi = new List<SozlesmeAYRINTI>();
            aramaParametresi = new SozlesmeAYRINTIArama();
            kullaniciKodu = "";
            e_onaylandimi = false;
            yeniOnay = new SozlesmeOnayi();
        }
        public async Task<SozlesmeOnayi> onaylaKos()
        {
            using (veri.Varlik vari = new Varlik())
            {
                var kim = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kodu == kullaniciKodu);
                if (kim != null)
                {
                    kim.e_sozlesmeOnaylandimi = true;
                    await veriTabani.KullaniciCizelgesi.kaydetKos(kim, vari, false);
                    SozlesmeOnayi yeniOnay = new SozlesmeOnayi();
                    yeniOnay.tarih = DateTime.Now;
                    yeniOnay.i_kullaniciKimlik = kim.kullaniciKimlik;
                    SozlesmeAYRINTI? ss = await vari.SozlesmeAYRINTIs.FirstOrDefaultAsync(p => p.i_sozlesmeTuruKimlik == (int)enumref_SozlesmeTuru.Uyelik_Sozlesmesi && p.varmi == true);
                    if (ss != null)
                        yeniOnay.i_sozlesmeKimlik = ss.sozlesmekimlik;
                    yeniOnay.varmi = true;
                    await veriTabani.SozlesmeOnayiCizelgesi.kaydetKos(yeniOnay, vari, false);
                }
            }
            return yeniOnay;
        }

        public async Task uyelikOnayiCekKos(Yonetici kime, string kodu)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                kullaniciKodu = kodu;
                sozu = await vari.SozlesmeAYRINTIs.FirstOrDefaultAsync(p => p.e_gecerliMi == true && p.i_sozlesmeTuruKimlik == (int)enumref_SozlesmeTuru.Uyelik_Sozlesmesi);
            }
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
                    Sozlesme? silinecek = await Sozlesme.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.SozlesmeModel modeli = new Models.SozlesmeModel();
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


        public Sozlesme kaydet(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                if (kartVerisi.metin != null)
                    kartVerisi.metin = Sayfa.dosyaKonumDuzelt(kartVerisi.metin, Genel.yazilimAyari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.kaydet(true);
                return kartVerisi;
            }
        }


        public async Task<Sozlesme> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                if (kartVerisi.metin != null)
                    kartVerisi.metin = Sayfa.dosyaKonumDuzelt(kartVerisi.metin, Genel.yazilimAyari);
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
                    Sozlesme silinecek = Sozlesme.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.SozlesmeModel modeli = new Models.SozlesmeModel();
            modeli.veriCekKosut(silen);
        }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await Sozlesme.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<SozlesmeAYRINTI>();
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                SozlesmeAYRINTIArama kosul = new SozlesmeAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Sozlesme();
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
                    if (talep.talepAyrintisi != null)
                    {
                        SozlesmeAYRINTIArama kosul = JsonConvert.DeserializeObject<SozlesmeAYRINTIArama>(talep.talepAyrintisi) ?? new SozlesmeAYRINTIArama();
                        dokumVerisi = await kosul.cek(vari);
                        kartVerisi = new Sozlesme();
                        aramaParametresi = kosul;
                    }
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = Sozlesme.olustur(kimlik);
            dokumVerisi = new List<SozlesmeAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new Sozlesme();
            dokumVerisi = SozlesmeAYRINTI.ara();
        }


    }
}
