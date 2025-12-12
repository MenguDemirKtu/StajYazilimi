using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajBirimiTurleri : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 stajBirimiTurlerikimlik { get; set; }

        [Required]
        public Int32 i_stajBirimiKimlik { get; set; }

        [Required]
        public Int32 i_stajTuruKimlik { get; set; }

        public Int32? gunu { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? siniflari { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ekAciklama { get; set; }

        public bool? varmi { get; set; }

        public bool? e_gecerlimi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? baslangic { get; set; }

        [Column(TypeName = "date")]
        public DateTime? bitis { get; set; }

    }
}
