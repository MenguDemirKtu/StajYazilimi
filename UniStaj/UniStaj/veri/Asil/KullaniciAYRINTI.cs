using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("KullaniciAYRINTI")]
    public partial class KullaniciAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 kullaniciKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sifre { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? guvenlikSorusu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? guvenlikYaniti { get; set; }

        [Display(Name = ".")]
        public Int32? i_kullaniciTuruKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciTuru { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? gercekAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaAdresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? unvan { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        public bool? e_rolTabanlimi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? rolTabanlimi { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "date")]
        public DateTime? dogumTarihi { get; set; }

        [Display(Name = ".")]
        public Int32? i_dilKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? dilAdi { get; set; }

        [Display(Name = ".")]
        public bool? e_faalmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? faalmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? ekAciklama { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? sicilNo { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? ogrenciNo { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? ustTur { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotoBilgisi { get; set; }

        [Display(Name = ".")]
        public Int32? y_iskurOgrencisiKimlik { get; set; }

        [Display(Name = ".")]
        public Int32? y_personelKimlik { get; set; }

        [Display(Name = ".")]
        public Int64? y_kisiKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? bysyeIlkGirisTarihi { get; set; }

        [Display(Name = ".")]
        public bool? e_sifreDegisecekmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? sonSifreDegistirmeTarihi { get; set; }

        [Display(Name = ".")]
        public Int32? rolSayisi { get; set; }

        [Display(Name = ".")]
        public bool? e_sozlesmeOnaylandimi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(8)")]
        public string? ciftOnayKodu { get; set; }

        [Display(Name = ".")]
        public Int32? sonGunGirisi { get; set; }

    }
}
