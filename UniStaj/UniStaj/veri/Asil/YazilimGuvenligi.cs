using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class YazilimGuvenligi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 yazilimGuvenligikimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? sonKullanimTarihi { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? apiKodu { get; set; }

        [Column(TypeName = "text")]
        public string? pythonKodu { get; set; }

        public bool? e_gecerliMi { get; set; }

        [Required, Column(TypeName = "nvarchar(300)")]
        public string baslik { get; set; }

        [Required]
        public bool varmi { get; set; }

    }
}
