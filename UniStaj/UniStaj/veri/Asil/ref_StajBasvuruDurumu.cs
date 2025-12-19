using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("ref_StajBasvuruDurumu")]
    public partial class ref_StajBasvuruDurumu : Bilesen
    {
        [Key]
        [Required]
        public Int32 StajBasvuruDurumuKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string StajBasvuruDurumuAdi { get; set; }

    }
}
