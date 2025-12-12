using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajTuru : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Staj TürüKimlik")]
        [Required]
        public Int32 stajTurukimlik { get; set; }

        [Display(Name = "Staj Türü Adı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? stajTuruAdi { get; set; }

        [Display(Name = "Tanım")]
        [Column(TypeName = "nvarchar(400)")]
        public string? tanim { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
