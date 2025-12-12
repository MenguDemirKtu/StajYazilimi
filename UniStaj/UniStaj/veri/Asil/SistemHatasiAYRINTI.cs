using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SistemHatasiAYRINTI")]
    public partial class SistemHatasiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 sistemHatasiKimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? sayfaBaslik { get; set; }

        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

        [Column(TypeName = "text")]
        public string? tanitim { get; set; }

    }
}
