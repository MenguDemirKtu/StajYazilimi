using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("SifreDegisikligiAYRINTI")]
    public partial class SifreDegisikligiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 sifreDegisikligiKimlik { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime tarih { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Required, Column(TypeName = "nvarchar(256)")]
        public string eskiSifre { get; set; }

        [Required, Column(TypeName = "nvarchar(256)")]
        public string yeniSifre { get; set; }

        public bool? varmi { get; set; }

    }
}
