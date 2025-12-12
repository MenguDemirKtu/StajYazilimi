using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class SifreDegisikligi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 sifreDegisikligiKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "datetime")]
        public DateTime tarih { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(256)")]
        public string eskiSifre { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(256)")]
        public string yeniSifre { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
