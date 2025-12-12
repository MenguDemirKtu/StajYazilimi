using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class GunlukGuvenlikAnahtari : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 gunlukGuvenlikAnahtariKimlik { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tarih { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? anahtar { get; set; }

        public bool? varmi { get; set; }

    }
}
