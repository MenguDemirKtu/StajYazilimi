using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Kayit : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 kayitKimlik { get; set; }

        [Column(TypeName = "nchar(1)")]
        public string? islemTuru { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? cizelgeAdi { get; set; }

        [Required]
        public Int64 cizelgeKimlik { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string? ipAdresi { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ekBilgi { get; set; }

        public Byte? kullaniciTuru { get; set; }

    }
}
