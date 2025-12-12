using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("EPostaKalibiAYRINTI")]
    public partial class EPostaKalibiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 ePostaKalibiKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? kalipBasligi { get; set; }

        [Required]
        public Int32 i_ePostaTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaTuru { get; set; }

        [Column(TypeName = "text")]
        public string? kalip { get; set; }

        [Required]
        public bool e_gecerlimi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? gecerlimi { get; set; }

        public bool? varmi { get; set; }

        public Int32? i_dilKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? dilAdi { get; set; }

    }
}
