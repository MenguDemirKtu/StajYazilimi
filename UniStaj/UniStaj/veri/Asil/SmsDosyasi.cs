using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class SmsDosyasi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 smsDosyasiKimlik { get; set; }

        public Int32? i_dosyaKimlik { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? baslik { get; set; }

        public bool? varmi { get; set; }

        [Required]
        public Int32 i_smsGonderimTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(700)")]
        public string? metin { get; set; }

    }
}
