using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class BelgeTuru : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Belge TürüKimlik")]
        [Required]
        public Int32 belgeTurukimlik { get; set; }

        [Display(Name = "Belge Türü Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? belgeTuruAdi { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "nvarchar(500)")]
        public string? aciklama { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
