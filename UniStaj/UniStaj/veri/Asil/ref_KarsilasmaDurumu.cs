using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_KarsilasmaDurumu : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 karsilasmaDurumuKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? karsilasmaDurumu { get; set; }

    }
}
