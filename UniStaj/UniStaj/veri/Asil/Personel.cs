using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class Personel : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 personelKimlik { get; set; }

        [Display(Name = "Sicil No")]
        [Column(TypeName = "nvarchar(11)")]
        public string? sicilNo { get; set; }

        [Display(Name = "TC Kimlik No")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = "Adı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? adi { get; set; }

        [Display(Name = "Soyadı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? soyAdi { get; set; }

        [Display(Name = "Telefon")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = "E-Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "Kişi")]
        public Int64? y_kisiKimlik { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "Cinsiyet")]
        public Int32? i_cinsiyetKimlik { get; set; }

        [Display(Name = "Pasaport No")]
        [Column(TypeName = "nvarchar(30)")]
        public string? pasaportNo { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [Column(TypeName = "date")]
        public DateTime? dogumTarihi { get; set; }

        [Display(Name = "Ana Adı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? anaAdi { get; set; }

        [Display(Name = "Baba Adı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? babaAdi { get; set; }

    }
}
