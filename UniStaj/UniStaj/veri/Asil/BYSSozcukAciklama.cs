using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class BYSSozcukAciklama : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 bysSozcukAciklamaKimlik { get; set; }

        [Required]
        public Int32 i_bysSozcukKimlik { get; set; }

        [Required]
        public Int32 i_dilKimlik { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? ifade { get; set; }

        public bool? varmi { get; set; }

    }
}
