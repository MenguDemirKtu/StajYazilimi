using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class GaleriFotosu : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 galeriFotosuKimlik { get; set; }

        [Required]
        public Int32 i_galeriKimlik { get; set; }

        [Required]
        public Int64 i_fotoKimlik { get; set; }

        [Required]
        public bool e_gosterimdemi { get; set; }

        [Required]
        public Int32 sirasi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? baslik { get; set; }

    }
}
