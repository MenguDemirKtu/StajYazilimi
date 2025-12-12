using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Ulke : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 ulkeKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ulkeAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ingilizceAdi { get; set; }

        public Int32? sirasi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? kisaltmasi { get; set; }

    }
}
