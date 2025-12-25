using Newtonsoft.Json;
using UniStaj.veri;
using UniStaj.veriTabani;


namespace UniStaj.Models
{
    public class StajBasvurusuModel : ModelTabani
    {
        public StajBasvurusu kartVerisi { get; set; }
        public List<StajBasvurusuAYRINTI> dokumVerisi { get; set; }
        public StajBasvurusuAYRINTIArama aramaParametresi { get; set; }


        public StajBasvurusuModel()
        {
            this.kartVerisi = new StajBasvurusu();
            this.dokumVerisi = new List<StajBasvurusuAYRINTI>();
            this.aramaParametresi = new StajBasvurusuAYRINTIArama();
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
                    StajBasvurusu? silinecek = await StajBasvurusu.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.StajBasvurusuModel modeli = new Models.StajBasvurusuModel();
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




        public async Task<StajBasvurusu> kaydetKos(Sayfa sayfasi)
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

                var kart = await StajBasvurusu.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;

                //StajKurumuAYRINTIArama kosul = new StajKurumuAYRINTIArama();
                //kosul.vergiNo = kartVerisi.vergiNo;
                //var liste = await kosul.bul(vari); 
                //if (liste != null)
                //{
                //    var kurum = liste;
                //    kartVerisi.vergiNo = kurum.vergiNo;
                //    kartVerisi.stajKurumAdi = kurum.stajKurumAdi;
                //    kartVerisi.i_stajkurumturukimlik = kurum.i_stajkurumturukimlik;
                //    kartVerisi.hizmetAlani = kurum.hizmetAlani;
                //    kartVerisi.ibanNo = kurum.ibanNo;
                //    kartVerisi.calisanSayisi = kurum.calisanSayisi;
                //    kartVerisi.adresi = kurum.adresi;
                //    kartVerisi.telNo = kurum.telNo;
                //    kartVerisi.ePosta = kurum.ePosta;
                //    kartVerisi.faks = kurum.faks;
                //    kartVerisi.webAdresi = kurum.webAdresi;
                //}
                dokumVerisi = new List<StajBasvurusuAYRINTI>();
                kartVerisi.i_stajyerKimlik = kime.i_stajyerKimlik;

                await baglilariCek(vari, kime);
            }
        }

        public List<StajyerAYRINTI> _ayStajyerAYRINTI { get; set; }
        public List<StajKurumTuruAYRINTI> _ayStajKurumTuruAYRINTI { get; set; }
        public List<StajTuruAYRINTI> _ayStajTuruAYRINTI { get; set; }
        public List<ref_StajBasvuruDurumu> _ayref_StajBasvuruDurumu { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim)
        {
            _ayStajyerAYRINTI = await StajyerAYRINTI.ara(vari);
            _ayStajKurumTuruAYRINTI = await StajKurumTuruAYRINTI.ara(vari);
            _ayStajTuruAYRINTI = await StajTuruAYRINTI.ara(vari);

            StajyerYukumlulukAYRINTIArama kosul = new StajyerYukumlulukAYRINTIArama();
            kosul.i_stajyerKimlik = kim.i_stajyerKimlik;
            var yukumlulukler = await kosul.cek(vari);
            var yukumlulukluStajTuruIdleri = yukumlulukler.Select(x => x.i_stajTuruKimlik).Distinct().ToList();

            _ayStajTuruAYRINTI = await StajTuruAYRINTI.ara(
                vari,
                x => yukumlulukluStajTuruIdleri.Contains(x.stajTurukimlik)
            );

            _ayref_StajBasvuruDurumu = await ref_StajBasvuruDurumu.ara();


        }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                StajBasvurusuAYRINTIArama kosul = new StajBasvurusuAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new StajBasvurusu();
                if (kime.i_kullaniciTuruKimlik == (int)enumref_KullaniciTuru.Stajyer)
                {
                    Stajyer? stajyer = await Stajyer.olusturKos(vari, kime.i_stajyerKimlik);

                    if (stajyer != null)
                    {
                        //kartVerisi.i_stajyerKimlik = int.Parse(stajyer.ogrenciNo);
                        kosul.i_stajyerKimlik = stajyer.stajyerkimlik;
                    }
                }

                if (kime._KullaniciTuru == enumref_KullaniciTuru.Birim_Staj_Sorumlusu)
                {
                    StajBirimYetkilisiAYRINTI? yetkili = await StajBirimYetkilisiAYRINTI.olusturKos(vari, kime.i_stajBirimYetkilisiKimlik);

                    StajBirimYetkilisiBirimiAYRINTIArama _arama = new StajBirimYetkilisiBirimiAYRINTIArama();
                    _arama.i_stajBirimYetkilisiKimlik = kime.i_stajBirimYetkilisiKimlik;
                    StajBirimYetkilisiBirimiAYRINTI? baglanti = await _arama.bul(vari);
                    if (baglanti != null)
                    {
                        kosul.i_stajBirimiKimlik = baglanti.i_stajBirimiKimlik;
                    }
                    else
                    {
                        // hiç sonuç gelmesin
                        kosul.varmi = false;
                    }


                }

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
                    StajBasvurusuAYRINTIArama kosul = JsonConvert.DeserializeObject<StajBasvurusuAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new StajBasvurusuAYRINTIArama();

                    if (kime.i_kullaniciTuruKimlik == (int)enumref_KullaniciTuru.Stajyer)
                    {
                        Stajyer? stajyer = await Stajyer.olusturKos(vari, kime.i_stajyerKimlik);

                        if (stajyer != null)
                        {
                            //kartVerisi.i_stajyerKimlik = int.Parse(stajyer.ogrenciNo);
                            kosul.i_stajyerKimlik = stajyer.stajyerkimlik;
                        }
                    }

                    if (kime._KullaniciTuru == enumref_KullaniciTuru.Birim_Staj_Sorumlusu)
                    {
                        StajBirimYetkilisiAYRINTI? yetkili = await StajBirimYetkilisiAYRINTI.olusturKos(vari, kime.i_stajBirimYetkilisiKimlik);

                        StajBirimYetkilisiBirimiAYRINTIArama _arama = new StajBirimYetkilisiBirimiAYRINTIArama();
                        _arama.i_stajBirimYetkilisiKimlik = kime.i_stajBirimYetkilisiKimlik;
                        StajBirimYetkilisiBirimiAYRINTI? baglanti = await _arama.bul(vari);
                        if (baglanti != null)
                        {
                            kosul.i_stajBirimiKimlik = baglanti.i_stajBirimiKimlik;
                        }
                        else
                        {
                            // hiç sonuç gelmesin
                            kosul.varmi = false;
                        }


                    }



                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new StajBasvurusu();
                    await baglilariCek(vari, kime);



                    aramaParametresi = kosul;
                }
            }
        }

    }
}