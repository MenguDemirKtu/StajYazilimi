using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("IlceAYRINTI")]
    public partial class IlceAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 ilcekimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ilceAdi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_sehirKimlik { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
