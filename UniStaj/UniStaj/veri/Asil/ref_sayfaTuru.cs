using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_sayfaTuru : Bilesen
    {
        [Key]
        [Required]
        public Int32 sayfaTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? sayfaTuru { get; set; }

    }
}
