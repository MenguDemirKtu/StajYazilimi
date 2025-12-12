using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Yarisma : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "YarışmaKimlik")]
        [Required]
        public Int32 yarismakimlik { get; set; }

        [Display(Name = "Turnuva")]
        [Required]
        public Int32 i_turnuvaKimlik { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        public Int32 i_kategoriKimlik { get; set; }

        [Display(Name = "Spor Branşı")]
        [Required]
        public Int32 i_sporBransiKimlik { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        public Int32? takimSayisi { get; set; }

        [Display(Name = ".")]
        public Int32? grupSayisi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

    }
}
