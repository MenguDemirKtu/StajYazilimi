using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using UniStaj.veri;

namespace UniStaj.veriTabani
{
    public class YazilimAyariArama
    {
        public Int32? yazilimAyariKimlik { get; set; }
        public string? ayarAdi { get; set; }
        public string? yazilimAdi { get; set; }
        public string? hesapEPostaAdresi { get; set; }
        public string? hesapEPostaSifresi { get; set; }
        public string? hesapEPostaPort { get; set; }
        public string? hesapEPostaHost { get; set; }
        public string? yazilimAdresi { get; set; }
        public bool? e_gecerlimi { get; set; }
        public bool? varmi { get; set; }
        public string? smsSifre { get; set; }
        public string? smsApi { get; set; }
        public string? smsKullaniciAdi { get; set; }
        public string? smsBaslik { get; set; }
        public string? yonetimPaneliKonumu { get; set; }
        public string? dosyaPaylasimKonumu { get; set; }
        public string? resimPaylasimKonumu { get; set; }
        public string? yeniDosyaPaylasimKonumu { get; set; }
        public string? yeniResimPaylasimKonumu { get; set; }
        public string? dosyaPaylasimKonumu2 { get; set; }
        public string? resimPaylasimKonumu2 { get; set; }
        public string? yeniDosyaPaylasimKonumu2 { get; set; }
        public string? yeniResimPaylasimKonumu2 { get; set; }
        public DateTime? sonGuncellemeTarihi { get; set; }
        public bool? e_menuGuncellenecekmi { get; set; }
        public Int64? i_fotoKimlik { get; set; }
        public Int32? i_smsGonderimSitesiKimlik { get; set; }
        public Int32? sifreDegistirmeGunSayisi { get; set; }
        public string? surumNumarasi { get; set; }
        public Int32? i_oturumAcmaTuruKimlik { get; set; }
        public Int32? gunlukOturumSiniri { get; set; }
        public string? guvenlikIhlalEPosta { get; set; }
        public YazilimAyariArama()
        {
            this.varmi = true;
        }

        public async Task<List<YazilimAyari>> cek(veri.Varlik vari)
        {
            var predicate = PredicateBuilder.New<YazilimAyari>(P => P.varmi == true);
            if (yazilimAyariKimlik != null)
                predicate = predicate.And(x => x.yazilimAyariKimlik == yazilimAyariKimlik);
            if (ayarAdi != null)
                predicate = predicate.And(x => x.ayarAdi != null && x.ayarAdi.Contains(ayarAdi));
            if (yazilimAdi != null)
                predicate = predicate.And(x => x.yazilimAdi != null && x.yazilimAdi.Contains(yazilimAdi));
            if (hesapEPostaAdresi != null)
                predicate = predicate.And(x => x.hesapEPostaAdresi != null && x.hesapEPostaAdresi.Contains(hesapEPostaAdresi));
            if (hesapEPostaSifresi != null)
                predicate = predicate.And(x => x.hesapEPostaSifresi != null && x.hesapEPostaSifresi.Contains(hesapEPostaSifresi));
            if (hesapEPostaPort != null)
                predicate = predicate.And(x => x.hesapEPostaPort != null && x.hesapEPostaPort.Contains(hesapEPostaPort));
            if (hesapEPostaHost != null)
                predicate = predicate.And(x => x.hesapEPostaHost != null && x.hesapEPostaHost.Contains(hesapEPostaHost));
            if (yazilimAdresi != null)
                predicate = predicate.And(x => x.yazilimAdresi != null && x.yazilimAdresi.Contains(yazilimAdresi));
            if (e_gecerlimi != null)
                predicate = predicate.And(x => x.e_gecerlimi == e_gecerlimi);
            if (varmi != null)
                predicate = predicate.And(x => x.varmi == varmi);
            if (smsSifre != null)
                predicate = predicate.And(x => x.smsSifre != null && x.smsSifre.Contains(smsSifre));
            if (smsApi != null)
                predicate = predicate.And(x => x.smsApi != null && x.smsApi.Contains(smsApi));
            if (smsKullaniciAdi != null)
                predicate = predicate.And(x => x.smsKullaniciAdi != null && x.smsKullaniciAdi.Contains(smsKullaniciAdi));
            if (smsBaslik != null)
                predicate = predicate.And(x => x.smsBaslik != null && x.smsBaslik.Contains(smsBaslik));
            if (yonetimPaneliKonumu != null)
                predicate = predicate.And(x => x.yonetimPaneliKonumu != null && x.yonetimPaneliKonumu.Contains(yonetimPaneliKonumu));
            if (dosyaPaylasimKonumu != null)
                predicate = predicate.And(x => x.dosyaPaylasimKonumu != null && x.dosyaPaylasimKonumu.Contains(dosyaPaylasimKonumu));
            if (resimPaylasimKonumu != null)
                predicate = predicate.And(x => x.resimPaylasimKonumu != null && x.resimPaylasimKonumu.Contains(resimPaylasimKonumu));
            if (yeniDosyaPaylasimKonumu != null)
                predicate = predicate.And(x => x.yeniDosyaPaylasimKonumu != null && x.yeniDosyaPaylasimKonumu.Contains(yeniDosyaPaylasimKonumu));
            if (yeniResimPaylasimKonumu != null)
                predicate = predicate.And(x => x.yeniResimPaylasimKonumu != null && x.yeniResimPaylasimKonumu.Contains(yeniResimPaylasimKonumu));
            if (dosyaPaylasimKonumu2 != null)
                predicate = predicate.And(x => x.dosyaPaylasimKonumu2 != null && x.dosyaPaylasimKonumu2.Contains(dosyaPaylasimKonumu2));
            if (resimPaylasimKonumu2 != null)
                predicate = predicate.And(x => x.resimPaylasimKonumu2 != null && x.resimPaylasimKonumu2.Contains(resimPaylasimKonumu2));
            if (yeniDosyaPaylasimKonumu2 != null)
                predicate = predicate.And(x => x.yeniDosyaPaylasimKonumu2 != null && x.yeniDosyaPaylasimKonumu2.Contains(yeniDosyaPaylasimKonumu2));
            if (yeniResimPaylasimKonumu2 != null)
                predicate = predicate.And(x => x.yeniResimPaylasimKonumu2 != null && x.yeniResimPaylasimKonumu2.Contains(yeniResimPaylasimKonumu2));
            if (sonGuncellemeTarihi != null)
                predicate = predicate.And(x => x.sonGuncellemeTarihi == sonGuncellemeTarihi);
            if (e_menuGuncellenecekmi != null)
                predicate = predicate.And(x => x.e_menuGuncellenecekmi == e_menuGuncellenecekmi);
            if (i_fotoKimlik != null)
                predicate = predicate.And(x => x.i_fotoKimlik == i_fotoKimlik);
            if (i_smsGonderimSitesiKimlik != null)
                predicate = predicate.And(x => x.i_smsGonderimSitesiKimlik == i_smsGonderimSitesiKimlik);
            if (sifreDegistirmeGunSayisi != null)
                predicate = predicate.And(x => x.sifreDegistirmeGunSayisi == sifreDegistirmeGunSayisi);
            if (surumNumarasi != null)
                predicate = predicate.And(x => x.surumNumarasi != null && x.surumNumarasi.Contains(surumNumarasi));
            if (i_oturumAcmaTuruKimlik != null)
                predicate = predicate.And(x => x.i_oturumAcmaTuruKimlik == i_oturumAcmaTuruKimlik);
            if (gunlukOturumSiniri != null)
                predicate = predicate.And(x => x.gunlukOturumSiniri == gunlukOturumSiniri);
            if (guvenlikIhlalEPosta != null)
                predicate = predicate.And(x => x.guvenlikIhlalEPosta != null && x.guvenlikIhlalEPosta.Contains(guvenlikIhlalEPosta));
            List<YazilimAyari> sonuc = new List<YazilimAyari>();
            sonuc = await vari.YazilimAyaris
        .Where(predicate)
        .ToListAsync();
            return sonuc;
        }

    }


    public class YazilimAyariCizelgesi
    {




        /// <summary> 
        /// Girilen koşullara göre veri çeker. 
        /// </summary>  
        /// <param name="kosullar"></param> 
        /// <returns></returns> 
        public static async Task<List<YazilimAyari>> ara(params Expression<Func<YazilimAyari, bool>>[] kosullar)
        {
            using (var vari = new veri.Varlik())
            {
                return await ara(vari, kosullar);
            }
        }
        public static async Task<List<YazilimAyari>> ara(veri.Varlik vari, params Expression<Func<YazilimAyari, bool>>[] kosullar)
        {
            var kosul = Vt.Birlestir(kosullar);
            return await vari.YazilimAyaris
                            .Where(kosul).OrderByDescending(p => p.yazilimAyariKimlik)
                   .ToListAsync();
        }



        public static async Task<YazilimAyari?> tekliCekKos(Int32 kimlik, Varlik kime)
        {
            YazilimAyari? kayit = await kime.YazilimAyaris.FirstOrDefaultAsync(p => p.yazilimAyariKimlik == kimlik && p.varmi == true);
            return kayit;
        }


        public static async Task kaydetKos(YazilimAyari yeni, Varlik vari, params bool[] yedekAlinsinmi)
        {
            if (yeni.yazilimAyariKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                await vari.YazilimAyaris.AddAsync(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                YazilimAyari? bulunan = await vari.YazilimAyaris.FirstOrDefaultAsync(p => p.yazilimAyariKimlik == yeni.yazilimAyariKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                await vari.SaveChangesAsync();
                await kay.kaydetKos(vari, yedekAlinsinmi);
            }
        }

        public static void kaydet(YazilimAyari yeni, Varlik vari)
        {
            if (yeni.yazilimAyariKimlik <= 0)
            {
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                if (yeni.varmi == null)
                    yeni.varmi = true;
                vari.YazilimAyaris.Add(yeni);
                vari.SaveChanges();
            }
            else
            {
                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                YazilimAyari? bulunan = vari.YazilimAyaris.FirstOrDefault(p => p.yazilimAyariKimlik == yeni.yazilimAyariKimlik);
                if (bulunan == null)
                    return;
                vari.Entry(bulunan).CurrentValues.SetValues(yeni);
                vari.SaveChanges();
            }
        }

        public static async Task silKos(YazilimAyari kimi, Varlik vari, params bool[] yedekAlinsinmi)
        {
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kimi.varmi = false;
            var bulunan = await vari.YazilimAyaris.FirstOrDefaultAsync(p => p.yazilimAyariKimlik == kimi.yazilimAyariKimlik);
            if (bulunan == null)
                return;
            kimi.varmi = false;
            await vari.SaveChangesAsync();
            await kay.kaydetKos(vari, yedekAlinsinmi);
        }


        public static YazilimAyari? tekliCek(Int32 kimlik, Varlik kime)
        {
            YazilimAyari? kayit = kime.YazilimAyaris.FirstOrDefault(p => p.yazilimAyariKimlik == kimlik);
            if (kayit != null)
                if (kayit.varmi != true)
                    return null;
            return kayit;
        }


        /// <summary> 
        /// Yeni kayıt ise ekler, eski kayıt ise günceller yedekleme isteğe bağlıdır. 
        /// </summary> 
        /// <param name="yeni"></param> 
        /// <param name="kime"></param> 
        /// <param name="kaydedilsinmi">Girmek isteğe bağlıdır. Eğer false değeri girilirse yedeği alınmaz. İkinci parametre </param> 
        public static void kaydet(YazilimAyari yeni, Varlik kime, params bool[] yedekAlinsinmi)
        {
            if (yeni.yazilimAyariKimlik <= 0)
            {
                if (yeni.varmi == null)
                    yeni.varmi = true;
                kime.YazilimAyaris.Add(yeni);
                kime.SaveChanges();
                Kayit kay = new Kayit(yeni, "E", yeni._tanimi() + " kaydının eklenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
            else
            {

                var bulunan = kime.YazilimAyaris.FirstOrDefault(p => p.yazilimAyariKimlik == yeni.yazilimAyariKimlik);
                if (bulunan == null)
                    return;
                kime.Entry(bulunan).CurrentValues.SetValues(yeni);
                kime.SaveChanges();

                Kayit kay = new Kayit(yeni, "G", yeni._tanimi() + " kaydının güncellenmesi ");
                kay.kaydet(yedekAlinsinmi);
            }
        }


        public static void sil(YazilimAyari kimi, Varlik kime)
        {
            kimi.varmi = false;
            var bulunan = kime.YazilimAyaris.FirstOrDefault(p => p.yazilimAyariKimlik == kimi.yazilimAyariKimlik);
            if (bulunan == null)
                return;
            kime.Entry(bulunan).CurrentValues.SetValues(kimi);
            kime.SaveChanges();
            Kayit kay = new Kayit(kimi, "S", kimi._tanimi() + " kaydının silinmesi");
            kay.kaydet();

        }
    }
}


