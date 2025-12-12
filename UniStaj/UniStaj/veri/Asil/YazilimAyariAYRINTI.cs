using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("YazilimAyariAYRINTI")]
    public partial class YazilimAyariAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 yazilimAyariKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ayarAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(250)")]
        public string? yazilimAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hesapEPostaAdresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hesapEPostaSifresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? hesapEPostaPort { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? hesapEPostaHost { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? yazilimAdresi { get; set; }

        [Display(Name = ".")]
        public bool? e_gecerlimi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? smsSifre { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(250)")]
        public string? smsApi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? smsKullaniciAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? smsBaslik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? yonetimPaneliKonumu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? dosyaPaylasimKonumu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? resimPaylasimKonumu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? yeniDosyaPaylasimKonumu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? yeniResimPaylasimKonumu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? dosyaPaylasimKonumu2 { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? resimPaylasimKonumu2 { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? yeniDosyaPaylasimKonumu2 { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "varchar(500)")]
        public string? yeniResimPaylasimKonumu2 { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? sonGuncellemeTarihi { get; set; }

        [Display(Name = ".")]
        public bool? e_menuGuncellenecekmi { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        public Int32? i_smsGonderimSitesiKimlik { get; set; }

        [Display(Name = ".")]
        public Int32? sifreDegistirmeGunSayisi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(10)")]
        public string? surumNumarasi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? oturumAcmaTuru { get; set; }

        [Display(Name = ".")]
        public Int32? i_oturumAcmaTuruKimlik { get; set; }

        [Display(Name = ".")]
        public Int32? gunlukOturumSiniri { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? guvenlikIhlalEPosta { get; set; }

    }
}
