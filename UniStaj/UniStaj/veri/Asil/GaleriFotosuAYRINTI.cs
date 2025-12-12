using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("GaleriFotosuAYRINTI")]
    public partial class GaleriFotosuAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 galeriFotosuKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_galeriKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int64 i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public bool e_gosterimdemi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 sirasi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(300)")]
        public string? galeriAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? galeriUrl { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(300)")]
        public string? baslik { get; set; }

    }
}
