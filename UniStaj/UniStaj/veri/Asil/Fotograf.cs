using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Fotograf : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 fotografKimlik { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? ilgiliCizelge { get; set; }

        public Int64? ilgiliKimlik { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? konum { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? yuklemeTarihi { get; set; }

        public bool? varmi { get; set; }

        public Int32? genislik { get; set; }

        public Int32? yukseklik { get; set; }

        public bool? e_sabitmi { get; set; }

        public bool? e_boyutlandirildimi { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? duzenlemeTarihi { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? ekAciklama { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? uzanti { get; set; }

        public bool? e_silinecekmi { get; set; }

    }
}
