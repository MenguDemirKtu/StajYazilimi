namespace UniStaj.Models
{
    public class GirisModel : ModelTabani
    {
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }

        public bool izinVarmi { get; set; }
        public bool yabancimi { get; set; }

        public string yonlendirmeAdresi { get; set; }
        public GirisModel()
        {
            izinVarmi = false;
            yabancimi = false;
            kullaniciAdi = "";
            sifre = "";
            yonlendirmeAdresi = "/anasayfa";
        }

        //public void personelEkle(veri.varlik vari, int kimlik, veri.Harici.KTUMensup kim, string tc, string sifre)
        //{
        //    Kullanici yeni = new Kullanici();
        //    yeni.sicilNo = kim.tcKimlikNo;
        //    yeni.ustTur = "iP";
        //    yeni.gercekAdi = kim.adi + " " + kim.soyAdi;
        //    yeni.tcKimlikNo = kim.tcKimlikNo;
        //    yeni.sicilNo = "";
        //    yeni.kullaniciAdi = kim.tcKimlikNo;
        //    yeni.sifre = sifre;
        //    yeni.i_dilKimlik = 1;
        //    yeni.e_rolTabanlimi = true;
        //    yeni.i_kullaniciTuruKimlik = (int)enumref_KullaniciTuru.Idari_Personel;
        //    yeni.telefon = "";
        //    yeni.unvan = kim.sicilNo + " Numaralı Çalışan";
        //    yeni.kodu = Guid.NewGuid().ToString();
        //    yeni.fotoBilgisi = kim.foto;
        //    yeni.sicilNo = kim.tcKimlikNo;
        //    yeni.kullaniciKimlik = kimlik;
        //    yeni.kaydet(vari, false);

        //    if (kimlik <= 0)
        //    {
        //        var roller = vari.Rols.FirstOrDefault(p => p.i_varsayilanOlduguKullaniciTuruKimlik == yeni.i_kullaniciTuruKimlik && p.e_varsayilanmi == true && p.e_gecerlimi == true);

        //        if (roller != null)
        //        {

        //            KullaniciRolu rol = new KullaniciRolu();
        //            rol.varmi = true;
        //            rol.i_kullaniciKimlik = yeni.kullaniciKimlik;
        //            rol.e_gecerlimi = true;
        //            rol.kullaniciRoluKimlik = roller.rolKimlik;
        //            rol.kaydet(vari, false);
        //        }
        //    }
        //}
        //private void ogrenciEkle(veri.varlik vari, int kimlik, veri.Harici.KTUMensup kim, string tc, string sifre)
        //{
        //    Kullanici yeni = new Kullanici();
        //    yeni.kullaniciKimlik = kimlik;
        //    yeni.ogrenciNo = kim.ogrenciNo;
        //    yeni.ustTur = "Ö";
        //    yeni.gercekAdi = kim.adi + " " + kim.soyAdi;
        //    yeni.tcKimlikNo = kim.tcKimlikNo;
        //    yeni.sicilNo = "";
        //    yeni.kullaniciAdi = kim.tcKimlikNo;
        //    yeni.sifre = sifre;
        //    yeni.i_dilKimlik = 1;
        //    yeni.e_rolTabanlimi = true;
        //    yeni.i_kullaniciTuruKimlik = (int)enumref_KullaniciTuru.Ogrenci;
        //    yeni.telefon = "";
        //    yeni.unvan = kim.ogrenciNo + " Numaralı Öğrenci";
        //    yeni.kodu = Guid.NewGuid().ToString();
        //    yeni.fotoBilgisi = kim.foto;
        //    yeni.kaydet(vari, false);

        //    var roller = vari.Rols.FirstOrDefault(p => p.i_varsayilanOlduguKullaniciTuruKimlik == yeni.i_kullaniciTuruKimlik && p.e_varsayilanmi == true && p.e_gecerlimi == true);

        //    if (roller != null)
        //    {
        //        if (kimlik <= 0)
        //        {
        //            KullaniciRolu rol = new KullaniciRolu();
        //            rol.varmi = true;
        //            rol.i_kullaniciKimlik = yeni.kullaniciKimlik;
        //            rol.e_gecerlimi = true;
        //            rol.kullaniciRoluKimlik = roller.rolKimlik;
        //            rol.kaydet(vari, false);
        //        }
        //    }
        //    var karsilik = vari.OgrenciAYRINTIs.FirstOrDefault(p => p.numarasi == kim.ogrenciNo);
        //    if (karsilik == null)
        //    {
        //        Ogrenci kisi = new Ogrenci();
        //        kisi.numarasi = kim.ogrenciNo;
        //        kisi.adi = kim.adi;
        //        kisi.soyadi = kim.soyAdi;
        //        kisi.e_faalmi = true;
        //        kisi.varmi = true;
        //        kisi.tcKimlikNo = kim.tcKimlikNo;
        //        kisi.kaydet(vari, false);
        //    }
        //}

        //public void yoksaEKle(veri.varlik vari, string tc, string sifre)
        //{
        //    string adres = String.Format("https://kartapi.ktu.edu.tr/api/mensup/sifredogrula?kod=Ka564bdsvcx654234asdhzxcdaspg65BHVPKh4356qawsdasdASDFSDF&tc={0}&sifre={1}", tc, sifre);
        //    var client = new RestClient(adres); // API URL'sini buraya koyun
        //    var request = new RestRequest();

        //    try
        //    {
        //        // İsteği gönderiyoruz ve yanıtı alıyoruz
        //        var response = client.Execute(request);

        //        if (response.IsSuccessful)
        //        {
        //            // JSON yanıtını nesne dizisine dönüştürüyoruz
        //            var items = JsonConvert.DeserializeObject<List<UniStaj.veri.Harici.KTUMensup>>(response.Content);

        //            foreach (var siradaki in items)
        //            {
        //                int kimlik = 0;
        //                // öğrenci ise 
        //                if (siradaki.ustTur == "Ö")
        //                {
        //                    var karsilik = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.ustTur == "Ö" && p.ogrenciNo == siradaki.ogrenciNo);
        //                    if (karsilik == null)
        //                    {
        //                        kimlik = 0;
        //                    }
        //                    else
        //                    {
        //                        kimlik = karsilik.kullaniciKimlik;
        //                    }
        //                    ogrenciEkle(vari, kimlik, siradaki, tc, sifre);

        //                }
        //                if (siradaki.ustTur == "İP")
        //                {
        //                    bool eklensinmi = true;

        //                    var karsilik = vari.KullaniciAYRINTIs.FirstOrDefault(p => p.ustTur == "İP" && p.tcKimlikNo == siradaki.tcKimlikNo);
        //                    if (karsilik == null)
        //                    {
        //                        eklensinmi = true;
        //                        kimlik = 0;
        //                    }
        //                    else
        //                    {
        //                        eklensinmi = false;
        //                        kimlik = karsilik.kullaniciKimlik;
        //                        if (string.IsNullOrEmpty(karsilik.gercekAdi))
        //                            eklensinmi = true;
        //                    }
        //                    if (eklensinmi)
        //                        personelEkle(vari, kimlik, siradaki, tc, sifre);


        //                }
        //            }

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        string nedir = ex.ToString();
        //    }
        //}
    }
}

//SKS Kampüs Kart Birimica yazılımış bir kodu görmektesiniz.
