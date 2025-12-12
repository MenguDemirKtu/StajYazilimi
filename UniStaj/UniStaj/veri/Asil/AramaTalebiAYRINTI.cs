using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("AramaTalebiAYRINTI")]
    public partial class AramaTalebiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 aramaTalebiKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? talepAyrintisi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
