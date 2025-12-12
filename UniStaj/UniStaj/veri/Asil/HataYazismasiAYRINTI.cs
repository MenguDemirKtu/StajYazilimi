using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("HataYazismasiAYRINTI")]
    public partial class HataYazismasiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 hataYasizmasiKimlik { get; set; }

        [Required]
        public Int64 i_hataBildirimiKimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Required]
        public Int64 i_yoneticiKimlik { get; set; }

        [Column(TypeName = "text")]
        public string? metin { get; set; }

        public bool? varmi { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? gercekAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaAdresi { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? unvan { get; set; }

    }
}
