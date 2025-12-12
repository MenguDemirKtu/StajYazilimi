using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("GeciciSifreAYRINTI")]
    public partial class GeciciSifreAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 geciciSifrekimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? sifre { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
