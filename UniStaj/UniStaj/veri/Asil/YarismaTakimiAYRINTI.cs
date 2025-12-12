using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("YarismaTakimiAYRINTI")]
    public partial class YarismaTakimiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 yarismaTakimiKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_yarismaKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_turnuvaKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_turnuvaTakimBilgiKimlik { get; set; }

        [Display(Name = ".")]
        public bool? e_gecerlimi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? gecerlimi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(10)")]
        public string? grubu { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

    }
}
