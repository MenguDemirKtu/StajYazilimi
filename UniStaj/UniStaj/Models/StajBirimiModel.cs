using Microsoft.EntityFrameworkCore; //.Entity;
using Newtonsoft.Json;
using UniStaj.GenelIslemler;
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
        public String yetkiliTcleri { get; set; }



        public StajBirimiModel()
        {
            this.kartVerisi = new StajBirimi();
            this.dokumVerisi = new List<StajBirimiAYRINTI>();
            this.aramaParametresi = new StajBirimiAYRINTIArama();
            this.yetkiliTcleri = " ";
        }

        public async Task turleriKaydet()
        {
            using (veri.Varlik vari = new Varlik())
            {
                for (int i = 0; i < stajBirimTurleri.Count; i++)
                {
                    StajBirimiTurleri? tekli = await StajBirimiTurleri.olusturKos(vari, stajBirimTurleri[i].stajBirimiTurlerikimlik);
                    if (tekli != null)
                    {
                        tekli.gunu = stajBirimTurleri[i].gunu;
                        tekli.ekAciklama = stajBirimTurleri[i].ekAciklama;
                        tekli.siniflari = stajBirimTurleri[i].siniflari;
                        await tekli.kaydetKos(vari, false);
                    }
                }
            }
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

                var tcList = yetkiliTcleri.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                foreach (var tc in tcList)
                {
                    var yetkiliArama = new StajBirimYetkilisiAYRINTIArama();
                    yetkiliArama.tcKimlikNo = tc;
                    var yetkililer = await yetkiliArama.cek(vari);
                    if (yetkililer.Count == 0)
                        continue;

                    var yetkili = yetkililer.First();
                    StajBirimYetkilisiBirimiArama bagArama = new StajBirimYetkilisiBirimiArama();
                    bagArama.i_stajBirimYetkilisiKimlik = yetkili.stajBirimYetkilisikimlik;
                    bagArama.i_stajBirimiKimlik = kartVerisi.stajBirimikimlik; bagArama.varmi = true;
                    var mevcutBag = await bagArama.bul(vari);

                    if (mevcutBag == null)
                    {
                        StajBirimYetkilisiBirimi yeniYetkili = new StajBirimYetkilisiBirimi();
                        yeniYetkili.i_stajBirimYetkilisiKimlik = yetkili.stajBirimYetkilisikimlik;
                        yeniYetkili.i_stajBirimiKimlik = kartVerisi.stajBirimikimlik;
                        yeniYetkili.e_gecerliMi = true;
                        await yeniYetkili.kaydetKos(vari, false);


                        KullaniciAYRINTIArama _kullaniciKosulu = new KullaniciAYRINTIArama();
                        _kullaniciKosulu.kullaniciAdi = yetkili.tcKimlikNo;
                        _kullaniciKosulu.i_kullaniciTuruKimlik = (int)enumref_KullaniciTuru.Birim_Staj_Sorumlusu;
                        _kullaniciKosulu.varmi = true;
                        KullaniciAYRINTI? eskiKullanici = await _kullaniciKosulu.bul(vari);

                        Kullanici kullanici;
                        if (eskiKullanici == null)
                        {

                            kullanici = new Kullanici();
                            kullanici.kullaniciAdi = yetkili.tcKimlikNo;
                            kullanici.sifre = GuvenlikIslemi.sifrele(yetkili.tcKimlikNo.Substring(yetkililer[0].tcKimlikNo.Length - 5));
                            kullanici.tcKimlikNo = yetkili.tcKimlikNo;
                            kullanici.i_kullaniciTuruKimlik = (int)enumref_KullaniciTuru.Birim_Staj_Sorumlusu;
                            kullanici.gercekAdi = yetkili.ad + " " + yetkili.soyad;
                            kullanici.telefon = yetkili.telefon;
                            kullanici.ePostaAdresi = yetkili.ePosta;
                            kullanici.y_stajBirimYetkilisiKimlik = yetkili.stajBirimYetkilisikimlik;
                            kullanici.kaydet(vari, false);
                        }
                        else
                        {
                            kullanici = await vari.Kullanicis.FirstAsync(x => x.kullaniciKimlik == eskiKullanici.kullaniciKimlik);
                        }

                        RolAYRINTIArama rolKosul = new RolAYRINTIArama();
                        rolKosul.e_varsayilanmi = true;
                        rolKosul.e_gecerlimi = true;
                        rolKosul.i_varsayilanOlduguKullaniciTuruKimlik = (int)enumref_KullaniciTuru.Birim_Staj_Sorumlusu;


                        RolAYRINTI? rolu = await rolKosul.bul(vari);
                        if (rolu != null)
                        {
                            KullaniciRoluArama rolBagArama = new KullaniciRoluArama();
                            rolBagArama.i_kullaniciKimlik = kullanici.kullaniciKimlik;
                            rolBagArama.i_rolKimlik = rolu.rolKimlik;
                            rolBagArama.e_gecerlimi = true;
                            var mevcutRolBagi = await rolBagArama.bul(vari);

                            if (mevcutRolBagi == null)
                            {
                                KullaniciRolu bag = new KullaniciRolu();
                                bag.i_kullaniciKimlik = kullanici.kullaniciKimlik;
                                bag.i_rolKimlik = rolu.rolKimlik;
                                bag.e_gecerlimi = true;
                                await bag.kaydetKos(vari, false);
                            }
                        }
                    }
                }



                List<ref_StajAsamasi> asamalar = await vari.ref_StajAsamasis.OrderBy(p => p.StajAsamasiKimlik).ToListAsync();

                StajBirimAsamasiAYRINTIArama _asamaKosulu = new StajBirimAsamasiAYRINTIArama();
                _asamaKosulu.i_stajBirimiKimlik = kartVerisi.stajBirimikimlik;
                List<StajBirimAsamasiAYRINTI> kayitliAsamalar = await _asamaKosulu.cek(vari);


                int sira = 1;
                for (int i = 0; i < asamalar.Count; i++)
                {
                    int yer = kayitliAsamalar.FindIndex(p => p.i_stajAsamasiKimlik == asamalar[i].StajAsamasiKimlik);
                    if (yer == -1)
                    {
                        StajBirimAsamasi yeni = new StajBirimAsamasi();
                        yeni.i_stajAsamasiKimlik = asamalar[i].StajAsamasiKimlik;
                        yeni.i_stajBirimiKimlik = kartVerisi.stajBirimikimlik;
                        yeni.sirasi = sira;
                        yeni.e_gecerliMi = true;
                        yeni.aciklama = "";
                        await yeni.kaydetKos(vari, false);
                    }
                    sira++;
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
