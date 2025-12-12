using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class SmsKalibiModel : ModelTabani
    {
        public SmsKalibi kartVerisi { get; set; }
        public List<SmsKalibiAYRINTI> dokumVerisi { get; set; }
        public SmsKalibiAYRINTIArama aramaParametresi { get; set; }


        public SmsKalibiModel()
        {
            kartVerisi = new SmsKalibi();
            dokumVerisi = new List<SmsKalibiAYRINTI>();
            aramaParametresi = new SmsKalibiAYRINTIArama();
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
                    SmsKalibi? silinecek = await SmsKalibi.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.SmsKalibiModel modeli = new Models.SmsKalibiModel();
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


        public SmsKalibi kaydet(Sayfa sayfasi)
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


        public async Task<SmsKalibi> kaydetKos(Sayfa sayfasi)
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
                    SmsKalibi silinecek = SmsKalibi.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.SmsKalibiModel modeli = new Models.SmsKalibiModel();
            modeli.veriCekKosut(silen);
        }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await SmsKalibi.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<SmsKalibiAYRINTI>();
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                SmsKalibiAYRINTIArama kosul = new SmsKalibiAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new SmsKalibi();
                dokumVerisi = await kosul.cek(vari);

                List<ref_EPostaTuru> turler = await vari.ref_EPostaTurus.ToListAsync();
                for (int i = 0; i < turler.Count; i++)
                {
                    var karsilik = dokumVerisi.FirstOrDefault(p => p.i_epostaturukimlik == turler[i].ePostaTuruKimlik && p.e_gecerlimi == true);
                    if (karsilik == null)
                    {
                        SmsKalibi yeni = new SmsKalibi();
                        yeni.varmi = true;
                        yeni.i_epostaturukimlik = turler[i].ePostaTuruKimlik;
                        yeni.e_gecerlimi = true;
                        yeni.baslik = "...";
                        yeni.kalip = "";
                        await yeni.kaydetKos(vari, false);
                    }
                }
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
                    SmsKalibiAYRINTIArama kosul = JsonConvert.DeserializeObject<SmsKalibiAYRINTIArama>(talep.talepAyrintisi) ?? new SmsKalibiAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new SmsKalibi();
                    aramaParametresi = kosul;
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = SmsKalibi.olustur(kimlik);
            dokumVerisi = new List<SmsKalibiAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new SmsKalibi();
            dokumVerisi = SmsKalibiAYRINTI.ara();
        }


    }
}
