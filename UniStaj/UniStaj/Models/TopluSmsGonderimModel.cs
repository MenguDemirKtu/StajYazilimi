using Microsoft.EntityFrameworkCore; // 
using UniStaj.veri;

namespace UniStaj.Models
{
    public class TopluSmsGonderimModel : ModelTabani
    {
        public TopluSmsGonderim kartVerisi { get; set; }
        public List<TopluSmsGonderimAYRINTI> dokumVerisi { get; set; }

        public int[] burslar { get; set; }

        public int[] durumlar { get; set; }

        public TopluSmsGonderimModel()
        {
            kartVerisi = new TopluSmsGonderim();
            dokumVerisi = new List<TopluSmsGonderimAYRINTI>();
            burslar = new int[0];
            durumlar = new int[0];
        }
        public async Task duzenleKos(string id, Yonetici gonderen)
        {
            kullanan = gonderen;
            using (veri.Varlik vari = new Varlik())
            {
                kartVerisi = await vari.TopluSmsGonderims.FirstOrDefaultAsync(p => p.kodu == id);
                if (kartVerisi != null)
                {

                }
            }
        }

        public async Task kaydetveGonderKos()
        {
            DateTime tarih = DateTime.Now;
            using (veri.Varlik vari = new Varlik())
            {

                if (kartVerisi.e_smsGonderilecekmi == true)
                {
                    List<TopluSMSAlicisiAYRINTI> alicilar = new List<TopluSMSAlicisiAYRINTI>();
                    alicilar = await vari.TopluSMSAlicisiAYRINTIs.Where(p => p.i_topluSMSGonderimKimlik == kartVerisi.topluSMSGonderimKimlik).ToListAsync();

                    for (int i = 0; i < alicilar.Count; i++)
                    {
                        GonderilenSMS sms = new GonderilenSMS();
                        sms.tarih = tarih;
                        sms.adSoyAd = alicilar[i].aliciTanimi;
                        sms.e_gonderildimi = true;
                        sms.telefon = alicilar[i].telefon;
                        sms.metin = kartVerisi.metin;
                        sms.y_topluGonderimKimlik = kartVerisi.topluSMSGonderimKimlik;
                        await veriTabani.GonderilenSMSCizelgesi.kaydetKos(sms, vari, false);
                        await GenelIslemler.SMSIslemi.smsGonder(sms.telefon, sms.metin);
                    }
                }
                if (kartVerisi.e_epostaGonderilecekmi == true)
                {
                    List<TopluEPostaAlicisi> alicilar2 = await vari.TopluEPostaAlicisis.Where(p => p.i_topluSMSGonderimKimlik == kartVerisi.topluSMSGonderimKimlik).ToListAsync();
                    for (int i = 0; i < alicilar2.Count; i++)
                    {
                        await EPostaIslemi.postaGonderveKaydetKos(vari, enumref_EPostaTuru.Genel, alicilar2[i].ePostaAdresi, kartVerisi.ePostaKonusu, kartVerisi.ePostaMetin, "");
                    }
                }
            }

            //using (BilgiIslemVt.OracleBaglanti bag = new BilgiIslemVt.OracleBaglanti())
            //{
            //    for (int i = 0; i < alicilar.Count; i++)
            //    {
            //        string kosul = string.Format(" INSERT INTO  BYS.OGRENCISMS( MESAJ, CEPNO ) VALUES ('" + kartVerisi.metin + "','" + alicilar[i].telefon + "')");
            //        await bag.Database.ExecuteSqlRawAsync(kosul);
            //    }
            //}
        }

        public async Task yetkiKontrolu(Sayfa sayfasi)
        {
            enumref_YetkiTuru yetkiTuru = enumref_YetkiTuru.Ekleme;
            if (kartVerisi._birincilAnahtar() > 0)
                yetkiTuru = enumref_YetkiTuru.Guncelleme;
            if (await sayfasi.yetkiVarmiKos(kartVerisi, yetkiTuru) == false)
                throw new Exception(Ikazlar.islemiYapmayaYetkinizYok(sayfasi.dilKimlik));
        }


        public TopluSmsGonderim kaydet(Sayfa sayfasi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                kullanan = sayfasi.mevcutKullanici();
                kartVerisi._kontrolEt(sayfasi.dilKimlik, vari);
                kartVerisi._sayfaAta(sayfasi);
                kartVerisi.kaydet(vari, true);
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
                    TopluSmsGonderim silinecek = TopluSmsGonderim.olustur(vari, kayitlar[i]);
                    silinecek._sayfaAta(sayfasi);
                }
            }
            Models.TopluSmsGonderimModel modeli = new Models.TopluSmsGonderimModel();
            modeli.veriCekKosut(silen);
        }

        public void veriCek(Yonetici kime, long kimlik)
        {
            kullanan = kime;
            yenimiBelirle(kimlik);
            kartVerisi = TopluSmsGonderim.olustur(kimlik);
            dokumVerisi = new List<TopluSmsGonderimAYRINTI>();
        }


        public void veriCekKosut(Yonetici kime)
        {
            kullanan = kime;
            kartVerisi = new TopluSmsGonderim();
            dokumVerisi = TopluSmsGonderimAYRINTI.ara();
        }


    }
}
