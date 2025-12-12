using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Hata : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 hataKimlik { get; set; }

        [Column(TypeName = "text")]
        public string? ifadesi { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        public bool? varmi { get; set; }

    }
}
