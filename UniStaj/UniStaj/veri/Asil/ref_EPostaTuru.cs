using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_EPostaTuru : Bilesen
    {
        [Key]
        [Required]
        public Int32 ePostaTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaTuru { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? alanAdlari { get; set; }

    }
}
