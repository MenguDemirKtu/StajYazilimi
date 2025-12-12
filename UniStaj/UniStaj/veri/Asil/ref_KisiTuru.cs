using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_KisiTuru : Bilesen
    {
        [Key]
        [Required]
        public Int32 KisiTuruKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string KisiTuruAdi { get; set; }

    }
}
