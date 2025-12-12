using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("CizelgeAdiAYRINTI")]
    public partial class CizelgeAdiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 cizelgeAdiKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? gercekAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? gorunurAdi { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? kisaAciklama { get; set; }

        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? dokumAciklamasi { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? kartAciklamasi { get; set; }

        public Int32? i_modulKimlik { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string? cogulu { get; set; }

    }
}
