using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.Models
{
    public class SmsDosyasiModel : ModelTabani
    {
        public SmsDosyasi kartVerisi { get; set; }
        public List<SmsDosyasiAYRINTI> dokumVerisi { get; set; }


        public SmsDosyasiModel()
        {
            kartVerisi = new SmsDosyasi();
            dokumVerisi = new List<SmsDosyasiAYRINTI>();
        }


        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }


        public async Task<SmsDosyasi> kaydet(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.kaydet(true);


                YuklenenDosyalarAYRINTI? dosyasi = await vari.YuklenenDosyalarAYRINTIs.FirstOrDefaultAsync(p => p.yuklenenDosyalarKimlik == kartVerisi.i_dosyaKimlik);
                if (dosyasi == null)
                    throw new Exception("Lütfen dosya yükleyiniz");

                StreamReader okucu = new StreamReader(dosyasi.fizikselKonumu());
                string ka = okucu.ReadToEnd();
                okucu.Close();

                string[] satirlar = ka.Split('\n', ',', ';');

                TopluSmsGonderim topluSmsGonderim = new TopluSmsGonderim();
                topluSmsGonderim._varSayilan();
                topluSmsGonderim.tarih = DateTime.Now;
                topluSmsGonderim.aciklama = "Dosyadan gönderim";
                topluSmsGonderim.aciklama = kartVerisi.baslik;
                topluSmsGonderim.bicimlendir(vari);
                await veriTabani.TopluSmsGonderimCizelgesi.kaydetKos(topluSmsGonderim, vari, false);

                var turu = (enumref_SmsGonderimTuru)kartVerisi.i_smsGonderimTuruKimlik;

                if (turu == enumref_SmsGonderimTuru.TC_Kimlik_No_ile)
                {
                    for (int i = 0; i < satirlar.Length; i++)
                    {
                        if (string.IsNullOrEmpty(satirlar[i]))
                            continue;

                        string tc = satirlar[i].Trim();


                        string? og = null;

                        if (og != null)
                        {
                            //TopluSMSAlicisi alici = new TopluSMSAlicisi();
                            //if (string.IsNullOrEmpty(og.telefon))
                            //    continue;
                            //alici.telefon = Genel.telNoBicimlendir(og.telefon);


                            //string tel = Genel.telNoBicimlendir(og.telefon);
                            //if (tel == ".")
                            //    continue;

                            //alici.aliciTanimi = "Öðrenci";
                            //alici.i_topluSMSGonderimKimlik = topluSmsGonderim.topluSMSGonderimKimlik;
                            //await veriTabani.TopluSMSAlicisiCizelgesi.kaydetKos(alici, vari, false);
                        }
                        else
                        {

                            //var kisi = vari.PersonelAYRINTIs.FirstOrDefault(p => p.tcKimlikNo == tc);
                            //if (kisi != null)
                            //{
                            //    TopluSMSAlicisi alici = new TopluSMSAlicisi();


                            //    if (string.IsNullOrEmpty(kisi.telefon))
                            //        continue;
                            //    string tel = Genel.telNoBicimlendir(kisi.telefon);
                            //    if (tel == ".")
                            //        continue;



                            //    alici.telefon = tel;
                            //    alici.aliciTanimi = "Personel";
                            //    alici.i_topluSMSGonderimKimlik = topluSmsGonderim.topluSMSGonderimKimlik;
                            //    await veriTabani.TopluSMSAlicisiCizelgesi.kaydetKos(alici, vari, false);

                            //}
                        }
                    }
                }

                if (turu == enumref_SmsGonderimTuru.Ogrenci_No_ile)
                {
                    for (int i = 0; i < satirlar.Length; i++)
                    {
                        if (string.IsNullOrEmpty(satirlar[i]))
                            continue;


                        string numara = satirlar[i].Trim();



                        string? og = null;

                        if (og != null)
                        {
                            //TopluSMSAlicisi alici = new TopluSMSAlicisi();
                            //if (string.IsNullOrEmpty(og.telefon))
                            //    continue;
                            //alici.telefon = Genel.telNoBicimlendir(og.telefon);
                            //alici.i_topluSMSGonderimKimlik = topluSmsGonderim.topluSMSGonderimKimlik;
                            //alici.aliciTanimi = "Öðrenci";
                            //await veriTabani.TopluSMSAlicisiCizelgesi.kaydetKos(alici, vari, false);
                        }

                    }
                }

                if (turu == enumref_SmsGonderimTuru.Telefon_No_ile)
                {
                    for (int i = 0; i < satirlar.Length; i++)
                    {

                        if (string.IsNullOrEmpty(satirlar[i]))
                            continue;
                        string tel = Genel.telNoBicimlendir(satirlar[i]);
                        if (tel == ".")
                            continue;

                        TopluSMSAlicisi alici = new TopluSMSAlicisi();
                        alici.telefon = tel;
                        alici.aliciTanimi = "";
                        alici.i_topluSMSGonderimKimlik = topluSmsGonderim.topluSMSGonderimKimlik;
                        await veriTabani.TopluSMSAlicisiCizelgesi.kaydetKos(alici, vari, false);


                    }
                }


                List<TopluSMSAlicisiAYRINTI> alicilar = vari.TopluSMSAlicisiAYRINTIs.Where(p => p.i_topluSMSGonderimKimlik == topluSmsGonderim.topluSMSGonderimKimlik).ToList();



                foreach (var siradaki in alicilar)
                {
                    GonderilenSMS yeni = new GonderilenSMS();
                    yeni._varSayilan();
                    yeni.metin = kartVerisi.metin;
                    if (string.IsNullOrEmpty(siradaki.telefon))
                        continue;
                    yeni.telefon = Genel.telNoBicimlendir(siradaki.telefon);
                    if (yeni.telefon == ".")
                        continue;
                    yeni.y_topluGonderimKimlik = topluSmsGonderim.topluSMSGonderimKimlik;
                    yeni.e_gonderildimi = false;
                    yeni.kaydet(vari, false);
                    await veriTabani.GonderilenSMSCizelgesi.kaydetKos(yeni, vari, false);

                }

                //using (BilgiIslemVt.OracleBaglanti bag = new BilgiIslemVt.OracleBaglanti())
                //{
                //    for (int i = 0; i < alicilar.Count; i++)
                //    {
                //        string kosul = string.Format(" INSERT INTO  BYS.OGRENCISMS( MESAJ, CEPNO ) VALUES ('" + kartVerisi.metin + "','" + alicilar[i].telefon + "')");
                //        await bag.Database.ExecuteSqlRawAsync(kosul);
                //    }
                //}

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
                    SmsDosyasi silinecek = SmsDosyasi.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                    silinecek.sil(vari);
                }
            }
            Models.SmsDosyasiModel modeli = new Models.SmsDosyasiModel();
            modeli.veriCekKosut(silen);
        }

        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = SmsDosyasi.olustur(kimlik);
            dokumVerisi = new List<SmsDosyasiAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new SmsDosyasi();
            dokumVerisi = SmsDosyasiAYRINTI.ara();
        }


    }
}
