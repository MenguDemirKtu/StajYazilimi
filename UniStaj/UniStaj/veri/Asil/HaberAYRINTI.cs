using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("HaberAYRINTI")]
    public partial class HaberAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 haberKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? baslik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? baglanti { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "date")]
        public DateTime? tarih { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(4000)")]
        public string? anahtarSozcukler { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "text")]
        public string? metin { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
