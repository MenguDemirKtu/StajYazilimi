using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_SozlesmeTuru : Bilesen
    {
        [Key]
        [Required]
        public Int32 SozlesmeTuruKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string SozlesmeTuruAdi { get; set; }

    }
}
