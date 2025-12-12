using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class SozlesmeOnayi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 sozlesmeOnayiKimlik { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime tarih { get; set; }

        [Required]
        public Int32 i_sozlesmeKimlik { get; set; }

        public bool? varmi { get; set; }

    }
}
