using Microsoft.EntityFrameworkCore; //.Entity;
using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;

namespace UniStaj.Models
{
    public class StajBirimiModel : ModelTabani
    {
        public StajBirimi kartVerisi { get; set; }
        public List<StajBirimiAYRINTI> dokumVerisi { get; set; }
        public StajBirimiAYRINTIArama aramaParametresi { get; set; }

        public List<StajTuruAYRINTI> _ayStajTuruAYRINTI { get; set; }


        public StajBirimiModel()
        {
            this.kartVerisi = new StajBirimi();
            this.dokumVerisi = new List<StajBirimiAYRINTI>();
            this.aramaParametresi = new StajBirimiAYRINTIArama();
        }

        public List<StajBirimiTurleriAYRINTI> stajBirimTurleri { get; set; }
        public async Task stajTurleriCek(Yonetici kim, string kod)
        {
            using (veri.Varlik vari = new Varlik())
            {
                StajBirimiAYRINTIArama _kosul = new StajBirimiAYRINTIArama();
                _kosul.kodu = kod;
                StajBirimiAYRINTI? kart = await _kosul.bul(vari);


                StajBirimiTurleriAYRINTIArama _kosul2 = new StajBirimiTurleriAYRINTIArama();
                _kosul2.i_stajBirimiKimlik = kart.stajBirimikimlik;
                stajBirimTurleri = await _kosul2.cek(vari);
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
                    StajBirimi? silinecek = await StajBirimi.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.StajBirimiModel modeli = new Models.StajBirimiModel();
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




        public async Task<StajBirimi> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);

                StajBirimiTurleriAYRINTIArama _kosul = new StajBirimiTurleriAYRINTIArama();
                _kosul.i_stajBirimiKimlik = kartVerisi.stajBirimikimlik;
                List<StajBirimiTurleriAYRINTI> kayitlilar = await _kosul.cek(vari);


                List<int> liste = this.turleri.ToList();

                for (int i = 0; i < liste.Count; i++)
                {
                    var karsilik = kayitlilar.FirstOrDefault(p => p.i_stajTuruKimlik == liste[i]);
                    if (karsilik == null)
                    {
                        StajBirimiTurleri yeni = new StajBirimiTurleri();
                        yeni.e_gecerlimi = true;
                        yeni.i_stajBirimiKimlik = kartVerisi.stajBirimikimlik;
                        yeni.i_stajTuruKimlik = liste[i];
                        await yeni.kaydetKos(vari, false);
                    }
                }


                for (int i = 0; i < kayitlilar.Count; i++)
                {
                    int yer = liste.IndexOf(kayitlilar[i].i_stajTuruKimlik);

                    if (yer == -1)
                    {
                        StajBirimiTurleri? silinecek = await StajBirimiTurleri.olusturKos(vari, kayitlilar[i].stajBirimiTurlerikimlik);
                        if (silinecek != null)
                        {
                            await silinecek.silKos(vari, false);
                        }
                    }
                }


                return kartVerisi;
            }
        }


        public int[] turleri { get; set; }

        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await StajBirimi.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<StajBirimiAYRINTI>();
                await baglilariCek(vari, kime);

                StajBirimiTurleriArama _kosul = new StajBirimiTurleriArama();
                _kosul.i_stajBirimiKimlik = Convert.ToInt32(kimlik);
                List<StajBirimiTurleri> liste = await _kosul.cek(vari);
                turleri = liste.Select(p => p.i_stajTuruKimlik).ToArray();
            }
        }

        public List<StajBirimiAYRINTI> _ayStajBirimiAYRINTI { get; set; }

        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            _ayStajBirimiAYRINTI = await vari.StajBirimiAYRINTIs.ToListAsync();
            _ayStajTuruAYRINTI = await StajTuruAYRINTI.ara(vari);
        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                StajBirimiAYRINTIArama kosul = new StajBirimiAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new StajBirimi();
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
                    StajBirimiAYRINTIArama kosul = JsonConvert.DeserializeObject<StajBirimiAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new StajBirimiAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new StajBirimi();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
