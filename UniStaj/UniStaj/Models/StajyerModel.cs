using Newtonsoft.Json;
using UniStaj.GenelIslemler;
using UniStaj.veri;
using UniStaj.veriTabani;
namespace UniStaj.Models
{
    public class StajyerModel : ModelTabani
    {
        public Stajyer kartVerisi { get; set; }
        public List<StajyerAYRINTI> dokumVerisi { get; set; }
        public StajyerAYRINTIArama aramaParametresi { get; set; }


        public StajyerModel()
        {
            this.kartVerisi = new Stajyer();
            this.dokumVerisi = new List<StajyerAYRINTI>();
            this.aramaParametresi = new StajyerAYRINTIArama();
        }


        public static async Task<bool> stajyerinYukumlulukleriniOlustur(veri.Varlik vari, Stajyer ogrenci)
        {

            StajBirimiTurleriAYRINTIArama _kosul = new StajBirimiTurleriAYRINTIArama();
            _kosul.i_stajBirimiKimlik = ogrenci.i_stajBirimiKimlik;

            List<StajBirimiTurleriAYRINTI> olmasiGerekenler = await _kosul.cek(vari);


            StajyerYukumlulukAYRINTIArama _mevcutKosul = new StajyerYukumlulukAYRINTIArama();
            _mevcutKosul.i_stajyerKimlik = ogrenci.stajyerkimlik;

            List<StajyerYukumlulukAYRINTI> olanlar = await _mevcutKosul.cek(vari);


            for (int i = 0; i < olmasiGerekenler.Count; i++)
            {
                int yer = olanlar.FindIndex(p => p.i_stajTuruKimlik == olmasiGerekenler[i].i_stajTuruKimlik);

                if (yer == -1)
                {
                    StajyerYukumluluk yeni = new StajyerYukumluluk();
                    yeni.i_stajTuruKimlik = olmasiGerekenler[i].i_stajTuruKimlik;
                    yeni.i_stajyerKimlik = ogrenci.stajyerkimlik;
                    yeni.gunSayisi = olmasiGerekenler[i].gunu;
                    yeni.yaptigiGunSayis = 0;
                    yeni.kabulEdilenGunSayisi = 0;
                    yeni.siniflar = olmasiGerekenler[i].siniflari;
                    yeni.aciklama = "";
                    await yeni.kaydetKos(vari, false);
                }

            }



            return true;

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
                    Stajyer? silinecek = await Stajyer.olusturKos(vari, kayitlar[i]);
                    if (silinecek == null)
                        continue;
                    silinecek._sayfaAta(sayfasi);
                    await silinecek.silKos(vari);
                }
            }
            Models.StajyerModel modeli = new Models.StajyerModel();
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




        public async Task<Stajyer> kaydetKos(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                await kartVerisi.kaydetKos(vari, true);


                await stajyerinYukumlulukleriniOlustur(vari, kartVerisi);

                KullaniciAYRINTI? eslesenKullaniciAdi = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.kullaniciAdi == kartVerisi.tcKimlikNo
                                 && p.i_kullaniciTuruKimlik == (int)enumref_KullaniciTuru.Stajyer
                );

                KullaniciAYRINTIArama _kullaniciKosulu = new KullaniciAYRINTIArama();
                _kullaniciKosulu.kullaniciAdi = kartVerisi.tcKimlikNo;
                _kullaniciKosulu.varmi = true;
                KullaniciAYRINTI? eskiKullanici = await _kullaniciKosulu.bul(vari);

                if (eskiKullanici == null)
                {

                    Kullanici yeniKullanici = new Kullanici();
                    yeniKullanici.kullaniciAdi = kartVerisi.tcKimlikNo;
                    yeniKullanici.sifre = GuvenlikIslemi.sifrele(kartVerisi.tcKimlikNo.Substring(kartVerisi.tcKimlikNo.Length - 5));
                    yeniKullanici.tcKimlikNo = kartVerisi.tcKimlikNo;
                    yeniKullanici.i_kullaniciTuruKimlik = (int)enumref_KullaniciTuru.Stajyer;
                    yeniKullanici.gercekAdi = kartVerisi.stajyerAdi + " " + kartVerisi.stajyerSoyadi;
                    yeniKullanici.telefon = kartVerisi.telefon;
                    yeniKullanici.ePostaAdresi = kartVerisi.ePosta;
                    yeniKullanici.y_stajyerKimlik = kartVerisi.stajyerkimlik;
                    yeniKullanici.kaydet(vari, false);

                    // KULLANICI YETKÝSÝ VER.

                    RolAYRINTIArama _kosul = new RolAYRINTIArama();
                    _kosul.e_varsayilanmi = true;
                    _kosul.e_gecerlimi = true;
                    _kosul.i_varsayilanOlduguKullaniciTuruKimlik = (int)enumref_KullaniciTuru.Stajyer;


                    RolAYRINTI? rolu = await _kosul.bul(vari);
                    if (rolu != null)
                    {
                        KullaniciRolu bag = new KullaniciRolu();
                        bag.i_kullaniciKimlik = yeniKullanici.kullaniciKimlik;
                        bag.i_rolKimlik = rolu.rolKimlik;
                        bag.e_gecerlimi = true;
                        await bag.kaydetKos(vari, false);
                    }
                }
                else
                {
                    // ESKÝ KULLANICI VAR ANCAK ROLLE BAÐLANTISI YOKSA BÝR ÞEYLER YAP.

                    RolAYRINTIArama _kosul = new RolAYRINTIArama();
                    _kosul.e_varsayilanmi = true;
                    _kosul.e_gecerlimi = true;
                    _kosul.i_varsayilanOlduguKullaniciTuruKimlik = (int)enumref_KullaniciTuru.Stajyer;


                    RolAYRINTI? rolu = await _kosul.bul(vari);
                    if (rolu != null)
                    {
                        KullaniciRoluArama _bagKosulu = new KullaniciRoluArama();
                        _bagKosulu.i_kullaniciKimlik = eskiKullanici.kullaniciKimlik;
                        _bagKosulu.i_rolKimlik = rolu.rolKimlik;
                        _bagKosulu.varmi = true;
                        KullaniciRolu? baglanti = await _bagKosulu.bul(vari);

                        if (baglanti == null)
                        {
                            baglanti = new KullaniciRolu();
                            baglanti.i_kullaniciKimlik = eskiKullanici.kullaniciKimlik;
                            baglanti.i_rolKimlik = rolu.rolKimlik;
                            await baglanti.kaydetKos(vari, false);
                        }

                    }

                }



                return kartVerisi;
            }
        }


        public async Task veriCekKos(Yonetici kime, long kimlik)
        {
            this.kullanan = kime;
            yenimiBelirle(kimlik);
            using (veri.Varlik vari = new Varlik())
            {
                var kart = await Stajyer.olusturKos(vari, kimlik);
                if (kart != null)
                    kartVerisi = kart;
                dokumVerisi = new List<StajyerAYRINTI>();
                await baglilariCek(vari, kime);
            }
        }

        public List<StajBirimiAYRINTI> _ayStajBirimiAYRINTI { get; set; }
        public async Task baglilariCek(veri.Varlik vari, Yonetici kim) { _ayStajBirimiAYRINTI = await StajBirimiAYRINTI.ara(vari); }

        public async Task veriCekKos(Yonetici kime)
        {
            this.kullanan = kime;
            using (veri.Varlik vari = new Varlik())
            {
                StajyerAYRINTIArama kosul = new StajyerAYRINTIArama();
                kosul.varmi = true;
                kartVerisi = new Stajyer();
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
                    StajyerAYRINTIArama kosul = JsonConvert.DeserializeObject<StajyerAYRINTIArama>(talep.talepAyrintisi ?? "") ?? new StajyerAYRINTIArama();
                    dokumVerisi = await kosul.cek(vari);
                    kartVerisi = new Stajyer();
                    await baglilariCek(vari, kime);
                    aramaParametresi = kosul;
                }
            }
        }

    }
}
