using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Ilce : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 ilcekimlik { get; set; }

        [Display(Name = "İlçe")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ilceAdi { get; set; }

        [Display(Name = "Şehir")]
        [Required]
        public Int32 i_sehirKimlik { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
