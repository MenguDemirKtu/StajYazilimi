using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class YazilimAyari : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 yazilimAyariKimlik { get; set; }

        [Display(Name = "Ayar Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ayarAdi { get; set; }

        [Display(Name = "Yazılım Adı")]
        [Column(TypeName = "nvarchar(250)")]
        public string? yazilimAdi { get; set; }

        [Display(Name = "Hesap E-Posta Adresi")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hesapEPostaAdresi { get; set; }

        [Display(Name = "Hesap E-Posta Şifresi")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hesapEPostaSifresi { get; set; }

        [Display(Name = "Hesap E-Posta Porta")]
        [Column(TypeName = "nvarchar(50)")]
        public string? hesapEPostaPort { get; set; }

        [Display(Name = "Hesap E-Posta Host")]
        [Column(TypeName = "nvarchar(50)")]
        public string? hesapEPostaHost { get; set; }

        [Display(Name = "Yazılım Adresi")]
        [Column(TypeName = "nvarchar(500)")]
        public string? yazilimAdresi { get; set; }

        [Display(Name = "Geçerli mi")]
        public bool? e_gecerlimi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "SMS Şifre")]
        [Column(TypeName = "nvarchar(100)")]
        public string? smsSifre { get; set; }

        [Display(Name = "SMS API")]
        [Column(TypeName = "nvarchar(250)")]
        public string? smsApi { get; set; }

        [Display(Name = "SMS Kullanıcı Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? smsKullaniciAdi { get; set; }

        [Display(Name = "SMS Başlık")]
        [Column(TypeName = "nvarchar(200)")]
        public string? smsBaslik { get; set; }

        [Display(Name = "Yönetim Paneli Konumu")]
        [Column(TypeName = "varchar(500)")]
        public string? yonetimPaneliKonumu { get; set; }

        [Display(Name = "Dosya Paylaşım Konum")]
        [Column(TypeName = "varchar(500)")]
        public string? dosyaPaylasimKonumu { get; set; }

        [Display(Name = "Resim Paylaşım Konumu")]
        [Column(TypeName = "varchar(500)")]
        public string? resimPaylasimKonumu { get; set; }

        [Display(Name = "Yeni Dosya Paylaşım Konumu")]
        [Column(TypeName = "varchar(500)")]
        public string? yeniDosyaPaylasimKonumu { get; set; }

        [Display(Name = "Yeni Resim Paylaşım Konumu")]
        [Column(TypeName = "varchar(500)")]
        public string? yeniResimPaylasimKonumu { get; set; }

        [Display(Name = "Dosya Paylaşım Konumu 2")]
        [Column(TypeName = "varchar(500)")]
        public string? dosyaPaylasimKonumu2 { get; set; }

        [Display(Name = "Reism Paylaşım Konumu 2")]
        [Column(TypeName = "varchar(500)")]
        public string? resimPaylasimKonumu2 { get; set; }

        [Display(Name = "Yeni Dosya Paylaşım Konumu 2")]
        [Column(TypeName = "varchar(500)")]
        public string? yeniDosyaPaylasimKonumu2 { get; set; }

        [Display(Name = "Yeni Resim Paylaşım Konumu 2")]
        [Column(TypeName = "varchar(500)")]
        public string? yeniResimPaylasimKonumu2 { get; set; }

        [Display(Name = "Son Güncelleme Tarihi")]
        [Column(TypeName = "datetime")]
        public DateTime? sonGuncellemeTarihi { get; set; }

        [Display(Name = "Menü Güncellenecek mi")]
        public bool? e_menuGuncellenecekmi { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "SMS Gönderim Sistesi")]
        public Int32? i_smsGonderimSitesiKimlik { get; set; }

        [Display(Name = "Şifre Değiştirme Gün Sayısı")]
        public Int32? sifreDegistirmeGunSayisi { get; set; }

        [Display(Name = "Sürüm Numarası")]
        [Column(TypeName = "nvarchar(10)")]
        public string? surumNumarasi { get; set; }

        [Display(Name = "Oturum Açma Türü")]
        public Int32? i_oturumAcmaTuruKimlik { get; set; }

        [Display(Name = "Günlük Oturum Sınırı")]
        public Int32? gunlukOturumSiniri { get; set; }

        [Display(Name = "Güvenlik İhlal E-Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? guvenlikIhlalEPosta { get; set; }

    }
}
