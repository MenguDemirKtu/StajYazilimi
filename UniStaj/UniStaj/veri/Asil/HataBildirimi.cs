using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class HataBildirimi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [Column(TypeName = "nvarchar(500)")]
        public string? hataAlinanSayfa { get; set; }

        [Column(TypeName = "text")]
        public string? hataAciklamasi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? kodu { get; set; }

        public Int32? i_hataBildirimDurumuKimlik { get; set; }

        public Int64? i_sonYazaKullaniciKimlik { get; set; }

        public Int32? oncelik { get; set; }

    }
}
