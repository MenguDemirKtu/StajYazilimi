using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class Stajyer : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 stajyerkimlik { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ogrenciNo { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Required]
        public Int32 i_stajBirimiKimlik { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? stajyerAdi { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? stajyerSoyadi { get; set; }

        public Int32? sinifi { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        public bool? varmi { get; set; }

    }
}
