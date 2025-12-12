using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SmsDosyasiAYRINTI")]
    public partial class SmsDosyasiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 smsDosyasiKimlik { get; set; }

        public Int32? i_dosyaKimlik { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? dosyasi { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? baslik { get; set; }

        public bool? varmi { get; set; }

    }
}
