using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("YazilimGuvenligiAYRINTI")]
    public partial class YazilimGuvenligiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 yazilimGuvenligikimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? sonKullanimTarihi { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? apiKodu { get; set; }

        [Column(TypeName = "text")]
        public string? pythonKodu { get; set; }

        public bool? e_gecerliMi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? gecerliMi { get; set; }

        [Required]
        public bool varmi { get; set; }

        [Required, Column(TypeName = "nvarchar(300)")]
        public string baslik { get; set; }

    }
}
