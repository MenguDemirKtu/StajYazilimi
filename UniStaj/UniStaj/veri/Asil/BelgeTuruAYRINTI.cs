using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("BelgeTuruAYRINTI")]
    public partial class BelgeTuruAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 belgeTurukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? belgeTuruAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
