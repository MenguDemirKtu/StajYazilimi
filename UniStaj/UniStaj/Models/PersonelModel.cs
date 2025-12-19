using Newtonsoft.Json;
using System.Data.Entity;
using UniStaj.veri;
using UniStaj.veriTabani;
namespace UniStaj.Models
{
    public class PersonelModel : ModelTabani
    {
        public Personel kartVerisi { get; set; }
        public List<PersonelAYRINTI> dokumVerisi { get; set; }
        public PersonelAYRINTIArama aramaParametresi { get; set; }


        public PersonelModel()
        {
            this.kartVerisi = new Personel();
            this.dokumVerisi = new List<PersonelAYRINTI>();
            this.aramaParametresi = new PersonelAYRINTIArama();
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
                    Personel? silinecek = await Personel.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.PersonelModel modeli = new Models.PersonelModel();
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




        public async Task<Personel> kaydetKos(Sayfa sayfasi)
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
                var kart = await Personel.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<PersonelAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public List<ref_Cinsiyet> _ayref_Cinsiyet { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { _ayref_Cinsiyet = await vari.ref_Cinsiyets.ToListAsync(); }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                PersonelAYRINTIArama kosul = new PersonelAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Personel();
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
                    PersonelAYRINTIArama kosul = JsonConvert.DeserializeObject<PersonelAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new PersonelAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new Personel();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
