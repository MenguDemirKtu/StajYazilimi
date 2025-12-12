using Microsoft.EntityFrameworkCore; // 
using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class ModulModel : ModelTabani
    {
        public Modul kartVerisi { get; set; }
        public List<ModulAYRINTI> dokumVerisi { get; set; }
        public ModulAYRINTIArama aramaParametresi { get; set; }


        public ModulModel()
        {
            kartVerisi = new Modul();
            dokumVerisi = new List<ModulAYRINTI>();
            aramaParametresi = new ModulAYRINTIArama();
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
                    Modul? silinecek = await Modul.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.ModulModel modeli = new Models.ModulModel();
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




        public async Task bysSayfasiOlarakEkle(veri.Varlik vari, Modul kaydedilecek)
        {
            var liste = await vari.BysMenus.Where(p => p.i_modulKimlik == kaydedilecek.modulKimlik && p.e_modulSayfasimi == true && p.varmi == true).ToListAsync();

            if (liste.Count > 0)
                return;

            BysMenu yeni = new BysMenu();
            yeni.e_modulSayfasimi = true;
            yeni.i_modulKimlik = kaydedilecek.modulKimlik;
            yeni.bysMenuBicim = "has-arrow waves-effect";
            yeni.i_ustMenuKimlik = 0;
            yeni.bysMenuAdi = kaydedilecek.modulAdi;
            yeni.i_webSayfasiKimlik = 0;
            yeni.bysMenuUrl = "#";
            yeni.sirasi = 100;
            await veriTabani.BysMenuCizelgesi.kaydetKos(yeni, vari, false);


        }


        public async Task<Modul> kaydetKos(Sayfa sayfasi)
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
                await bysSayfasiOlarakEkle(vari, kartVerisi);

                List<WebSayfasi> sayfalari = await vari.WebSayfasis.Where(p => p.i_modulKimlik == kartVerisi.modulKimlik && p.varmi == true).ToListAsync();

                foreach (var siradaki in sayfalari)
                {
                    if (kartVerisi.i_fotoKimlik != null)
                    {
                        siradaki.i_fotoKimlik = kartVerisi.i_fotoKimlik.Value;
                        await veriTabani.WebSayfasiCizelgesi.kaydetKos(siradaki, vari, false);
                    }
                }

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
                    Modul silinecek = Modul.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.ModulModel modeli = new Models.ModulModel();
            modeli.veriCekKosut(silen);
        }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await Modul.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<ModulAYRINTI>();
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                ModulAYRINTIArama kosul = new ModulAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Modul();
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
                    ModulAYRINTIArama kosul = JsonConvert.DeserializeObject<ModulAYRINTIArama>(talep.talepAyrintisi) ?? new ModulAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new Modul();
                    aramaParametresi = kosul;
                }
            }
        }
        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = Modul.olustur(kimlik);
            dokumVerisi = new List<ModulAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new Modul();
            dokumVerisi = ModulAYRINTI.ara();
        }


    }
}
