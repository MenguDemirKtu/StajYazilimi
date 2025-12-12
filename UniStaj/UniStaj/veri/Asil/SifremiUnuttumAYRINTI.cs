using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SifremiUnuttumAYRINTI")]
    public partial class SifremiUnuttumAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 sifremiUnuttumKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = ".")]
        public bool? e_smsmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(11)")]
        public string? telefon { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "date")]
        public DateTime? tarih { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
