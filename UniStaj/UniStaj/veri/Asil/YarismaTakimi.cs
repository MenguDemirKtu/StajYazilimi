using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class YarismaTakimi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 yarismaTakimiKimlik { get; set; }

        [Display(Name = "Yarışma")]
        [Required]
        public Int32 i_yarismaKimlik { get; set; }

        [Display(Name = "Takım Bilgi")]
        [Required]
        public Int32 i_turnuvaTakimBilgiKimlik { get; set; }

        [Display(Name = "Geçerli mi")]
        public bool? e_gecerlimi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "Grubu")]
        [Column(TypeName = "nvarchar(10)")]
        public string? grubu { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

    }
}
