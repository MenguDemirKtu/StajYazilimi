using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class ref_StajTipi : Bilesen
    {
        [Key]
        [Required]
        public Int32 StajTipiKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string StajTipiAdi { get; set; }

    }
}
