using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SmsKalibiAYRINTI")]
    public partial class SmsKalibiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = "SMS KalıbıKimlik")]
        [Required]
        public Int32 smsKalibikimlik { get; set; }

        [Display(Name = "baslik")]
        [Required, Column(TypeName = "nvarchar(150)")]
        public string baslik { get; set; }

        [Display(Name = "ePostaTuru")]
        [Required]
        public Int32 i_epostaturukimlik { get; set; }

        [Display(Name = "kalip")]
        [Required, Column(TypeName = "nvarchar(1000)")]
        public string kalip { get; set; }

        [Display(Name = "gecerlimi")]
        [Required]
        public bool e_gecerlimi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? gecerlimi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaTuru { get; set; }

    }
}
