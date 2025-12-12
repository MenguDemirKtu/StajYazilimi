using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("KisiAYRINTI")]
    public partial class KisiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 kisiKimlik { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? adi { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? soyAdi { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? adres { get; set; }

        public Int32? i_sehirKimlik { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dogumTarihi { get; set; }

        public bool? e_PostaDogrulandimi { get; set; }

        public bool? e_telefonDogrulandimi { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? kisiTanimi { get; set; }

        public Int32? i_cinsiyetKimlik { get; set; }

        public Int64? i_fotoKimlik { get; set; }

    }
}
