using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("StajBirimiAYRINTI")]
    public partial class StajBirimiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 stajBirimikimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? stajBirimAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(11)")]
        public string? telefon { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? birimSorumluAdi { get; set; }

        [Display(Name = ".")]
        public bool? e_altBirimMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? altBirimMi { get; set; }

        [Display(Name = ".")]
        public Int32? i_ustStajBirimiKimlik { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
