using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class GeciciSifre : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Geçici ŞifreKimlik")]
        [Required]
        public Int32 geciciSifrekimlik { get; set; }

        [Display(Name = "TC Kimlik No")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = "Şifre")]
        [Column(TypeName = "nvarchar(50)")]
        public string? sifre { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        public Int64? i_bilgiIslemKimlik { get; set; }

    }
}
