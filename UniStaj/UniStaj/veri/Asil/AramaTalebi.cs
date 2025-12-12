using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class AramaTalebi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 aramaTalebiKimlik { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? talepAyrintisi { get; set; }

        public bool? varmi { get; set; }

    }
}
