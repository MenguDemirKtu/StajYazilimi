using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("OturumAcmaAYRINTI")]
    public partial class OturumAcmaAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 oturumAcmaKimlik { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? gercekAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaAdresi { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime tarih { get; set; }

        [Required]
        public Int32 gunlukSayi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? gun { get; set; }

    }
}
