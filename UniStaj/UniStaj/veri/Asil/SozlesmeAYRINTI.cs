using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SozlesmeAYRINTI")]
    public partial class SozlesmeAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 sozlesmekimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? baslik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_sozlesmeTuruKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(200)")]
        public string SozlesmeTuruAdi { get; set; }

        [Display(Name = ".")]
        public bool? e_gecerliMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? gecerliMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "text")]
        public string? metin { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
