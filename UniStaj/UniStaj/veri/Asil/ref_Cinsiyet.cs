using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_Cinsiyet : Bilesen
    {
        [Key]
        [Required]
        public Int32 CinsiyetKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string CinsiyetAdi { get; set; }

    }
}
