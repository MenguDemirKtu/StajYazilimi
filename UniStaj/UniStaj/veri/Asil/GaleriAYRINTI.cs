using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("GaleriAYRINTI")]
    public partial class GaleriAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 galeriKimlik { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? galeriAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? galeriUrl { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ilgiliCizelge { get; set; }

        public Int64? ilgiliKimlik { get; set; }

        public Int32? genislik { get; set; }

        public Int32? yukseklik { get; set; }

        public bool? varmi { get; set; }

        public Int64? i_fotoKimlik { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

    }
}
