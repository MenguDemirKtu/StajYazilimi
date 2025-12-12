using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class _baglantilar : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required, Column(TypeName = "nvarchar(128)")]
        public string kaynakTablo { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string? kaynakSutun { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        public string? gorunurKaynakSutun { get; set; }

        [Required, Column(TypeName = "nvarchar(128)")]
        public string yabanciTablo { get; set; }

        [Column(TypeName = "nvarchar(-1)")]
        public string? yabanciSutun { get; set; }

    }
}
