using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class KullaniciBildirimi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 kullaniciBildirimiKimlik { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? bildirimBasligi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? bildirimTanitimi { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarihi { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? ayrintisi { get; set; }

        public Byte? e_goruldumu { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? gorulmeTarihi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? url { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? kodu { get; set; }

    }
}
