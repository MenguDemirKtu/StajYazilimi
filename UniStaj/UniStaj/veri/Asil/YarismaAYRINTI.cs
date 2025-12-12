using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("YarismaAYRINTI")]
    public partial class YarismaAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 yarismakimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_turnuvaKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_kategoriKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? kategoriAdi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_sporBransiKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
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
