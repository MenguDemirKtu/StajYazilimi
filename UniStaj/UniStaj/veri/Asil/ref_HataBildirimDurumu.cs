using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_HataBildirimDurumu : Bilesen
    {
        [Key]
        [Required]
        public Int32 hataBildirimDurumuKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? hataBildirimDurumu { get; set; }

    }
}
