using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class SmsKalibi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 smsKalibikimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(150)")]
        public string baslik { get; set; }

        [Required]
        public Int32 i_epostaturukimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(1000)")]
        public string kalip { get; set; }

        [Required]
        public bool e_gecerlimi { get; set; }

        public bool? varmi { get; set; }

    }
}
