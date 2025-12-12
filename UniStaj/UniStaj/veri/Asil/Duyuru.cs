using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Duyuru : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 duyurukimlik { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? baslik { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? tanitim { get; set; }

        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? baglanti { get; set; }

        [Required]
        public Int32 sirasi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? baslangic { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? bitis { get; set; }

        public bool? e_yayindami { get; set; }

    }
}
