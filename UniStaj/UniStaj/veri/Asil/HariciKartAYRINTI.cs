using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("HariciKartAYRINTI")]
    public partial class HariciKartAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 hariciKartkimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? ad { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? soyad { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePostaAdresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? dogumTarihi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_kartBirimiKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? birimAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? unvan { get; set; }

        [Display(Name = ".")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? sifre { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? meslek { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? verilmeAmaci { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
