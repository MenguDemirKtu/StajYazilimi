using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class HataYazismasi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 hataYasizmasiKimlik { get; set; }

        [Required]
        public Int64 i_hataBildirimiKimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Required]
        public Int64 i_yoneticiKimlik { get; set; }

        [Column(TypeName = "text")]
        public string? metin { get; set; }

        public bool? varmi { get; set; }

    }
}
