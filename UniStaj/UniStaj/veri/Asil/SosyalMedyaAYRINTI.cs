using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SosyalMedyaAYRINTI")]
    public partial class SosyalMedyaAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 sosyalMedyakimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? sosyamMedyaAdi { get; set; }

        public Int64? i_fotoKimlik { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? adres { get; set; }

        public Int32? sirasi { get; set; }

        public bool? varmi { get; set; }

    }
}
