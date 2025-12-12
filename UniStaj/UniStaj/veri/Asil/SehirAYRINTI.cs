using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SehirAYRINTI")]
    public partial class SehirAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 sehirKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sehirAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(5)")]
        public string? telefonKodu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(5)")]
        public string? plakaKodu { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
