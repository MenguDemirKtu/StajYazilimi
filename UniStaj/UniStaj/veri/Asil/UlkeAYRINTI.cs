using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("UlkeAYRINTI")]
    public partial class UlkeAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 ulkeKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ulkeAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ingilizceAdi { get; set; }

        public Int32? sirasi { get; set; }

        public bool? varmi { get; set; }

    }
}
