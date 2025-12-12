using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_SmsGonderimTuru : Bilesen
    {
        [Key]
        [Required]
        public Int32 SmsGonderimTuruKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string SmsGonderimTuruAdi { get; set; }

    }
}
