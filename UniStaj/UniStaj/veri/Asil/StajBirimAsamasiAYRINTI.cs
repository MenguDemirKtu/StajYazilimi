using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("StajBirimAsamasiAYRINTI")]
    public partial class StajBirimAsamasiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 stajBirimAsamasikimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_stajBirimiKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? stajBirimAdi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_stajAsamasiKimlik { get; set; }

        [Display(Name = ".")]
        [Required, Column(TypeName = "nvarchar(200)")]
        public string StajAsamasiAdi { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public bool e_gecerliMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? gecerliMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
