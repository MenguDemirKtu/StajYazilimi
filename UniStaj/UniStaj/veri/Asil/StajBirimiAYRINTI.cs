using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("StajBirimiAYRINTI")]
    public partial class StajBirimiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 stajBirimikimlik { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? stajBirimAdi { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? telefon { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? birimSorumluAdi { get; set; }

        public bool? e_altBirimMi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? altBirimMi { get; set; }

        public Int32? i_ustStajBirimiKimlik { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(40)")]
        public string? kodu { get; set; }

    }
}
