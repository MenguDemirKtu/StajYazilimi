using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class GaleriModel : ModelTabani
    {
        public Galeri kartVerisi { get; set; }
        public List<GaleriAYRINTI> dokumVerisi { get; set; }
        public GaleriAYRINTIArama aramaParametresi { get; set; }
        public List<GaleriFotosuAYRINTI> fotolari { get; set; }


        public GaleriModel()
        {
            this.kartVerisi = new Galeri();
            this.dokumVerisi = new List<GaleriAYRINTI>();
            this.aramaParametresi = new GaleriAYRINTIArama();
            this.fotolari = new List<GaleriFotosuAYRINTI>();
        }

        public async Task siraBelirleKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new Varlik())
            {
                List<GaleriFotosu> tamami = new List<GaleriFotosu>();
                for (int i = 0; i < this.fotolari.Count; i++)
                {
                    GaleriFotosuAYRINTI foto = this.fotolari[i];

                    GaleriFotosu? tekil = await vari.GaleriFotosus.FirstOrDefaultAsync(p => p.galeriFotosuKimlik == foto.galeriFotosuKimlik);
                    if (tekil != null)
                    {
                        tekil.sirasi = foto.sirasi;
                        tekil.baslik = foto.baslik;
                        tekil._sayfaAta(sayfasi);
                        tamami.Add(tekil);
                    }
                }

                tamami = tamami.OrderBy(P => P.sirasi).ToList();
                for (int i = 0; i < tamami.Count; i++)
                {
                    tamami[i].sirasi = i + 1;
                    await tamami[i].kaydetKos(vari, false);
                }

            }
        }
        public async Task fotoSilKos(Sayfa sayfasi, long fotoKimlik)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                GaleriFotosu? foto = await vari.GaleriFotosus.FirstOrDefaultAsync(p => p.galeriFotosuKimlik == fotoKimlik);
                if (foto != null)
                {
                    foto._sayfaAta(sayfasi);
                    Fotograf? _foto = await vari.Fotografs.FirstOrDefaultAsync(p => p.fotografKimlik == foto.i_fotoKimlik);
                    await foto.silKos(vari, true);
                    if (_foto != null)
                        await veriTabani.FotografCizelgesi.kaydetKos(_foto, vari, false);

                }
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
                    Galeri? silinecek = await Galeri.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.GaleriModel modeli = new Models.GaleriModel();
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





        public async Task<Galeri> kaydetKos(Sayfa sayfasi)
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
                var kart = await Galeri.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<GaleriAYRINTI>();
                await fotoAyariBelirle(vari, kartVerisi._cizelgeAdi());
            }
        }
        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                GaleriAYRINTIArama kosul = new GaleriAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Galeri();
                dokumVerisi = await kosul.cek(vari);
            }
        }
        public async Task duzenle(Yonetici kime, string id)
        {
            kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                kartVerisi = await vari.Galeris.FirstOrDefaultAsync(p => p.kodu == id) ?? new Galeri();
                if (kartVerisi == null)
                    kartVerisi = new Galeri();
                fotolari = await vari.GaleriFotosuAYRINTIs.Where(p => p.i_galeriKimlik == kartVerisi.galeriKimlik).OrderBy(p => p.sirasi).ToListAsync();
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
                    GaleriAYRINTIArama kosul = JsonConvert.DeserializeObject<GaleriAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new GaleriAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new Galeri();
                    aramaParametresi = kosul;
                }
            }
        }



    }
}
