using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Modul : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 modulKimlik { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? modulAdi { get; set; }

        public Int64? i_fotoKimlik { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ikonAdi { get; set; }

        public Int32? sirasi { get; set; }

    }
}
