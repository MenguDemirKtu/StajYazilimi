using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("TopluSMSAlicisiAYRINTI")]
    public partial class TopluSMSAlicisiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 toplSMSAlicisiKimlik { get; set; }

        [Required]
        public Int32 i_topluSMSGonderimKimlik { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? metin { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? aliciTanimi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? ePostaAdresi { get; set; }

    }
}
