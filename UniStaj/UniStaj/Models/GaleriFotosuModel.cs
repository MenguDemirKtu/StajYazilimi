using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class GaleriFotosuModel : ModelTabani
    {
        public GaleriFotosu kartVerisi { get; set; }
        public List<GaleriFotosuAYRINTI> dokumVerisi { get; set; }
        public GaleriFotosuAYRINTIArama aramaParametresi { get; set; }


        public GaleriFotosuModel()
        {
            this.kartVerisi = new GaleriFotosu();
            this.dokumVerisi = new List<GaleriFotosuAYRINTI>();
            this.aramaParametresi = new GaleriFotosuAYRINTIArama();
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
                    GaleriFotosu? silinecek = await GaleriFotosu.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.GaleriFotosuModel modeli = new Models.GaleriFotosuModel();
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



        public async Task<GaleriFotosu> kaydetKos(Sayfa sayfasi)
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
                var kart = await GaleriFotosu.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<GaleriFotosuAYRINTI>();
                await fotoAyariBelirle(vari, kartVerisi._cizelgeAdi());
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                GaleriFotosuAYRINTIArama kosul = new GaleriFotosuAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new GaleriFotosu();
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
                    GaleriFotosuAYRINTIArama kosul = JsonConvert.DeserializeObject<GaleriFotosuAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new GaleriFotosuAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new GaleriFotosu();
                    aramaParametresi = kosul;
                }
            }
        }


    }
}
