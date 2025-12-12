using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("TopluEPostaAlicisiAYRINTI")]
    public partial class TopluEPostaAlicisiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int64 topluEPostaAlicisiKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_topluSMSGonderimKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(250)")]
        public string? metin { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? aliciTanimi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePostaAdresi { get; set; }

    }
}
