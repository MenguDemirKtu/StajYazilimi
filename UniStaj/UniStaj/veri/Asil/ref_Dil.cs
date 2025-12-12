using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_Dil : Bilesen
    {
        [Key]
        [Required]
        public Int32 dilKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? dilAdi { get; set; }

    }
}
