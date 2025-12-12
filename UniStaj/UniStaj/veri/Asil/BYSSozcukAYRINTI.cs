using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("BYSSozcukAYRINTI")]
    public partial class BYSSozcukAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 bysSozcukKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(1000)")]
        public string sozcuk { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? parametreler { get; set; }

        public bool? varmi { get; set; }

    }
}
