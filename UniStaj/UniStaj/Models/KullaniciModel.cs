using Microsoft.EntityFrameworkCore; // 
using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class KullaniciModel : ModelTabani
    {
        public Kullanici kartVerisi { get; set; }
        public KullaniciAYRINTI? kullaniciBilgisi { get; set; }
        public List<KullaniciAYRINTI> dokumVerisi { get; set; }
        public List<KullaniciRoluAYRINTI> kayitliRoller { get; set; }
        public int[] roller { get; set; }

        public KullaniciAYRINTIArama aramaParametresi { get; set; }

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
                    Kullanici? silinecek = await Kullanici.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.KullaniciModel modeli = new Models.KullaniciModel();
            await modeli.veriCekKos(silen);
        }
        public KullaniciModel()
        {
            roller = new int[0];
            kartVerisi = new Kullanici();
            dokumVerisi = new List<KullaniciAYRINTI>();
            kullaniciBilgisi = new KullaniciAYRINTI();
            kayitliRoller = new List<KullaniciRoluAYRINTI>();
            aramaParametresi = new KullaniciAYRINTIArama();
        }

        public async Task veriCekKos(Yonetici kime)
        {
            using (veri.Varlik vari = new Varlik())
            {
                kartVerisi = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kime.kullaniciKimlik) ?? new Kullanici();
                dokumVerisi = await vari.KullaniciAYRINTIs.OrderByDescending(p => p.kullaniciAdi).ToListAsync();
                kayitliRoller = await vari.KullaniciRoluAYRINTIs.Where(p => p.i_kullaniciKimlik == kartVerisi.kullaniciKimlik).ToListAsync();
            }
        }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kimlik);
                try
                {
                    if (kart == null)
                        throw new Exception("Kullanýcý bilgisi bulunamadý");
                    if (kart.sifre == null)
                        throw new Exception("Þifre bulunamadý");
                    kart.sifre = GenelIslemler.GuvenlikIslemi.sifreCoz(kart.sifre);
                }
                catch
                {
                }
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<KullaniciAYRINTI>();
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
                        KullaniciAYRINTIArama kosul = JsonConvert.DeserializeObject<KullaniciAYRINTIArama>(talep.talepAyrintisi) ?? new KullaniciAYRINTIArama();
                        dokumVerisi = await kosul.cek(vari);
                        kartVerisi = new Kullanici();
                        aramaParametresi = kosul;
                    }
                }
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


        public async Task<Kullanici> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                if (kartVerisi.sifre != null)
                    kartVerisi.sifre = GenelIslemler.GuvenlikIslemi.sifrele(kartVerisi.sifre);
                await veriTabani.KullaniciCizelgesi.kaydetKos(kartVerisi, vari, false);
                List<KullaniciRolu> eskiler = await vari.KullaniciRolus.Where(p => p.i_kullaniciKimlik == kartVerisi.kullaniciKimlik).ToListAsync();
                foreach (var siradaki in eskiler)
                {
                    siradaki.varmi = false;
                    await veriTabani.KullaniciRoluCizelgesi.kaydetKos(siradaki, vari, false);
                }

                for (int i = 0; i < roller.Length; i++)
                {
                    KullaniciRolu rol = new KullaniciRolu();
                    rol.varmi = true;
                    rol.i_kullaniciKimlik = kartVerisi.kullaniciKimlik;
                    rol.i_rolKimlik = roller[i];
                    rol.e_gecerlimi = true;
                    //  rol.kaydet(vari, false);
                    await veriTabani.KullaniciRoluCizelgesi.kaydetKos(rol, vari, false);

                }
                kartVerisi.rolSayisi = roller.Length;
                await veriTabani.KullaniciCizelgesi.kaydetKos(kartVerisi, vari, false);
                return kartVerisi;
            }
        }


        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = Kullanici.olustur(kimlik);
            if (kartVerisi.sifre != null)
                kartVerisi.sifre = GenelIslemler.GuvenlikIslemi.sifreCoz(kartVerisi.sifre);
            dokumVerisi = new List<KullaniciAYRINTI>();
            using (veri.Varlik vari = new Varlik())
            {
                List<KullaniciRoluAYRINTI> kayitlilar = vari.KullaniciRoluAYRINTIs.Where(p => p.i_kullaniciKimlik == kimlik).ToList();
                roller = new int[kayitlilar.Count];
                for (int i = 0; i < kayitlilar.Count; i++)
                    roller[i] = kayitlilar[i].i_rolKimlik;
            }
        }




    }
}
