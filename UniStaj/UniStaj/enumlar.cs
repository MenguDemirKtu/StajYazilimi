namespace UniStaj
{
    public enum enum_sayfaTuru
    {
        kart = 1,
        dokum = 2,
        rapor = 3,
        anaSayfa = 4,
    }
    public enum enumref_GaleriTuru
    {
        Fotograf = 1,
        Video = 2

    }

    public enum enumIzinTuru
    {
        rapor = 1,
        yillikIzin = 2,
        idariIzin = 3,
        zorunluMazeret = 4,
        ucretsizIzin = 5,
        takdireBagliMazeret = 7
    }
    public enum enumref_IslemOnceligi
    {
        Ilk_Eklenen_Deneyler_Oncelikli = 4,
        Kisa_Sureli_Islemler_Oncelikli = 1,
        Son_Eklenen_Deneyler_Oncelikli = 3,
        Uzun_Sureli_Islemler_Oncelikli = 2
    }
    public enum enumref_DeneyDurum
    {
        Olusturuldu = 1,
        Siraya_Alindi = 2,
        Tamamlanma_suresi_doldu_yeniden_olusturulacak = 3,
        Iptal_Edildi = 4,
        Tamamlandi = 5,
        HATA_OLUSTU = 6

    }
    public enum enumref_ParaBirimi
    {
        TL = 1,
        USD = 2,
        EUR = 3,
        HAS = 4
    }



    public enum enumref_YetkiTuru
    {
        Ekleme = 2,
        Gorme = 1,
        Guncelleme = 4,
        Silme = 3
    }
    public enum enumref_Dil
    {
        Turkce = 1,
        Ingilizce = 2

    }
    public enum enumref_HataBildirimDurumu
    {
        Kisi_bildirdi, _bekliyor = 1,
        Kisi_yanit_verdi = 2,
        Yetkili_yanit_verdi = 3,
        Sorun_cozuldu = 4

    }



    public enum enumref_FederasyonBildirimTuru
    {
        Etkinlik_Basvuru_Iptal_Talebi = 1,
        Sporcu_kullanici_basvuru_bildirimi = 2,
        Kulup_yetkilisi_basvuru_bildirimi = 3,
        Antrenor_basvurusu = 4,
        Atletizm_Gonullusu_Basvurusu = 5,
        Dilekce = 6,
        Kulup_uyesi_basvuru_bildirimi = 7,
        Sporcu_Transfer_Bildirimi = 8,
        Kulup_Tescil_Basvuru_Bildirimi = 9,
        Odeme_Alindi = 10,
        Hata_Bildirimi = 11,
        Yarisma_Harici_Etkinlik_Basvurusu = 12,
        Antrenorluk_Talebi = 13,
        Yeni_Lisanssiz_Sporcu_Talebi = 15,
        Yarismaya_Kulup_Basvurusu = 16,
        Sporcu_Transfer_Oldu = 18

    }
    public enum enum_islemSonuc
    {
        bos = 0,
        basarili = 1,
        dikkat = 2,
        basarisiz = 3
    }
    public enum enumref_Cinsiyet
    {
        Erkek = 2,
        Kadin = 1

    }
    public enum enumref_AsamaTuru
    {
        Asamali = 1,
        Asamasiz = 2
    }
    public enum enumref_KullaniciTuru
    {
        Sistem_Yoneticisi = 0,
        Yazilimci = 1,
        Personel = 2,
        Birim_Staj_Sorumlusu = 6,
        Birim_Staj_Alt_Sorumlusu = 7,
        Universite_Staj_Sorumlusu = 8,
        Stajyer = 9,
        Kurum_Staj_Yetkilisi = 10
    }
    public enum enumref_SmsGonderimTuru
    {
        Ogrenci_No_ile = 1,
        TC_Kimlik_No_ile = 2,
        Telefon_No_ile = 3

    }
    public enum enumref_RolIslemi
    {
        ILCE_YEMEKHANE_SORUMLU_ROLU = 2,
        PERSONEL_DEVAM_SORUMLU_ROLU = 1,
        SPOR_MERKEZI_SORUMLU = 3,
        ISKUR_BOLUM_YETKILISI = 7,
        İŞKUR_ÜNİVERSİTE_YETKİLİSİ = 8,
        İŞKUR_ÖĞRENCİ_ROLÜ = 9
    }
    public enum enumref_GelirGiderUstTuru
    {
        Gelir = 1,
        Gider = 2

    }

    public enum enumref_SmsGonderimSitesi
    {
        NETGSM = 1,
        Posta_Guvercini = 2

    }
    public enum enumref_SozlesmeTuru
    {
        Uyelik_Ayrilis_Sozlesmesi = 2,
        Uyelik_Sozlesmesi = 1

    }

    public enum enumref_OturumAcmaTuru
    {
        Dogruan_Sifre_ile = 1,
        E_Posta_ve_Sifre_ile = 3,
        SMS_E_Posta_ve_Sifre_ile = 4,
        SMS_ve_sifre_ile = 2

    }
    public enum enumref_EPostaTuru
    {
        Sifremi_Unuttum = 1,
        Cift_Yonlu_Dogrulama = 2,
        E_Posta_Onayla = 3,
        Uyelik_Onayi = 4,
        Genel = 5,
        Oturum_Sinir_Asimi = 6

    }
    public enum enumref_CalismaSureTuru
    {
        Alti_Aydan_Az = 1,
        Alti_Aydan_Fazla = 2

    }
    public enum enumref_SgkHareketTuru
    {
        Ise_Alim = 1,
        Isten_Ayrilis = 2

    }
    public enum enumref_StajTipi
    {
        Gonullu = 2,
        Zorunlu = 1

    }
    public enum enumref_BelgeDurumu
    {
        Onaylandi = 3,
        Reddedildi = 4,
        Yuklendi_Bekliyor = 2,
        Yuklenmedi = 1

    }
}





