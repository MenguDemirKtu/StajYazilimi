using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("TopluSmsGonderimAYRINTI")]
    public partial class TopluSmsGonderimAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 topluSMSGonderimKimlik { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? metin { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? aciklama { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

    }
}
