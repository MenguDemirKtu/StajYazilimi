using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_RolIslemi : Bilesen
    {
        [Key]
        [Required]
        public Int32 rolIslemiKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string rolIslemiAdi { get; set; }

    }
}
