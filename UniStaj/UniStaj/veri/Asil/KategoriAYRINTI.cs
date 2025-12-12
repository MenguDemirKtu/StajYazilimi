using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("KategoriAYRINTI")]
    public partial class KategoriAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 kategorikimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? kategoriAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
