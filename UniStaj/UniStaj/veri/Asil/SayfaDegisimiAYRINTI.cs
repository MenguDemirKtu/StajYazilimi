using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SayfaDegisimiAYRINTI")]
    public partial class SayfaDegisimiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 sayfaDegisimiKimlik { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ad { get; set; }

        public bool? varmi { get; set; }

    }
}
