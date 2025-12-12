using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("HataBildirimiAYRINTI")]
    public partial class HataBildirimiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 hataBildirimiKimlik { get; set; }

        public Int32? i_yoneticiKimlik { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? baslik { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? tarih { get; set; }

        public bool? varmi { get; set; }

        public Byte? e_goruldumu { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? goruldumu { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? hataAlinanSayfa { get; set; }

        [Column(TypeName = "text")]
        public string? hataAciklamasi { get; set; }

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

        [Column(TypeName = "nvarchar(200)")]
        public string? kodu { get; set; }

        public Int32? i_hataBildirimDurumuKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? hataBildirimDurumu { get; set; }

        public Int64? i_sonYazaKullaniciKimlik { get; set; }

        public Int32? oncelik { get; set; }

    }
}
