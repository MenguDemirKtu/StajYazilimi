using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_OturumAcmaTuru : Bilesen
    {
        [Key]
        [Required]
        public Int32 oturumAcmaTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? oturumAcmaTuru { get; set; }

    }
}
