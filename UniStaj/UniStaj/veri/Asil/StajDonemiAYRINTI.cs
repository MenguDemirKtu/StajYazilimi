using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("StajDonemiAYRINTI")]
    public partial class StajDonemiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 stajDonemikimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? stajDonemAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanim { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? baslangic { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? bitis { get; set; }

        [Display(Name = ".")]
        public bool? e_gecerliDonemmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? gecerliDonemmi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
