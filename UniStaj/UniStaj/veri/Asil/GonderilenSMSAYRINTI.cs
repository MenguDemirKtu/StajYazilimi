using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("GonderilenSMSAYRINTI")]
    public partial class GonderilenSMSAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 gonderilenSMSKimlik { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? baslik { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? adSoyAd { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? metin { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        public bool? varmi { get; set; }

        public Int64? y_topluGonderimKimlik { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        public bool? e_gonderildimi { get; set; }

    }
}
