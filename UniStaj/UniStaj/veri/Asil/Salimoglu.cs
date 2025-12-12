using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Salimoglu : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 kimlik { get; set; }

        public Int32? numara { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? burskredi { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? ikamet { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? aile { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? ailesosyal { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? aligelir { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? ailebarınma { get; set; }

        public Int32? ailebireysayisi { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? YTBBursu { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? UlkeDisiKardes { get; set; }

        [Column(TypeName = "varchar(512)")]
        public string? UlkeIciKardes { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ogrenciNo { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? adSoyad { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? telefon { get; set; }

        public double? puani { get; set; }

    }
}
