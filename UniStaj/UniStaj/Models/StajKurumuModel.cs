using Newtonsoft.Json;
using UniStaj.GenelIslemler;
using UniStaj.veri;
using UniStaj.veriTabani;
namespace UniStaj.Models
{
    public class StajKurumuModel : ModelTabani
    {
        public StajKurumu kartVerisi { get; set; }
        public List<StajKurumuAYRINTI> dokumVerisi { get; set; }
        public StajKurumuAYRINTIArama aramaParametresi { get; set; }


        public StajKurumuModel()
        {
            this.kartVerisi = new StajKurumu();
            this.dokumVerisi = new List<StajKurumuAYRINTI>();
            this.aramaParametresi = new StajKurumuAYRINTIArama();
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
                    StajKurumu? silinecek = await StajKurumu.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.StajKurumuModel modeli = new Models.StajKurumuModel();
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




        public async Task<StajKurumu> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                KullaniciAYRINTI? eslesenKullaniciAdi = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.kullaniciAdi == kartVerisi.vergiNo);
                if (eslesenKullaniciAdi != null)
                {
                    throw new Exception("Bu vergi numarasýna sahip kullanýcý var.");
                }
                KullaniciAYRINTIArama _kullaniciKosulu = new KullaniciAYRINTIArama();
                _kullaniciKosulu.kullaniciAdi = kartVerisi.vergiNo;
                _kullaniciKosulu.varmi = true;
                KullaniciAYRINTI? eskiKullanici = await _kullaniciKosulu.bul(vari);
                if (eskiKullanici == null)
                {
                    Kullanici yeniKullanici = new Kullanici();
                    yeniKullanici.kullaniciAdi = kartVerisi.vergiNo;
                    yeniKullanici.sifre = GuvenlikIslemi.sifrele(kartVerisi.vergiNo.Substring(kartVerisi.vergiNo.Length - 5));
                    yeniKullanici.i_kullaniciTuruKimlik = (int)enumref_KullaniciTuru.Kurum_Staj_Yetkilisi;
                    yeniKullanici.gercekAdi = kartVerisi.stajKurumAdi;
                    yeniKullanici.kaydet(vari, true);


                    // KULLANICI ROLÜ ARAMASI
                    // ROLLER KULLANICI ÝLE BAÐLANTI EKLE

                }
                else
                {
                    // KULLANICI ROLÜ ARAMASI
                    // KULLANICI ROLÜ VAR.SA
                    // ROLLE ARASINDA BAÐLANTI VAR MI
                    // YOKSA EKLE

                }
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
                var kart = await StajKurumu.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<StajKurumuAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public List<StajKurumTuruAYRINTI> _ayStajKurumTuruAYRINTI { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { _ayStajKurumTuruAYRINTI = await StajKurumTuruAYRINTI.ara(vari); }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                StajKurumuAYRINTIArama kosul = new StajKurumuAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new StajKurumu();
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
                    StajKurumuAYRINTIArama kosul = JsonConvert.DeserializeObject<StajKurumuAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new StajKurumuAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new StajKurumu();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
