using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SozlesmeOnayiAYRINTI")]
    public partial class SozlesmeOnayiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 sozlesmeOnayiKimlik { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime tarih { get; set; }

        [Required]
        public Int32 i_sozlesmeKimlik { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? baslik { get; set; }

        public bool? varmi { get; set; }

    }
}
