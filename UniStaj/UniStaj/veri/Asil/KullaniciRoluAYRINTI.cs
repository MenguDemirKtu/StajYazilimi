using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("KullaniciRoluAYRINTI")]
    public partial class KullaniciRoluAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 kullaniciRoluKimlik { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Required]
        public Int32 i_rolKimlik { get; set; }

        [Required]
        public bool e_gecerlimi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? rolAdi { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? gercekAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaAdresi { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? unvan { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ustTur { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? fotoBilgisi { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? ekAciklama { get; set; }

    }
}
