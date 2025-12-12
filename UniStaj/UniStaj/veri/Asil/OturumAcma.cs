using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class OturumAcma : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 oturumAcmaKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "datetime")]
        public DateTime tarih { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 gunlukSayi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
