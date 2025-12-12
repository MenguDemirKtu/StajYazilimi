using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("BYSSozcukAciklamaAYRINTI")]
    public partial class BYSSozcukAciklamaAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 bysSozcukAciklamaKimlik { get; set; }

        [Required]
        public Int32 i_bysSozcukKimlik { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? ifade { get; set; }

        public bool? varmi { get; set; }

        [Required]
        public Int32 i_dilKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? dilAdi { get; set; }

        [Required, Column(TypeName = "nvarchar(1000)")]
        public string sozcuk { get; set; }

        [Required]
        public Int32 bysSozcukKimlik { get; set; }

    }
}
