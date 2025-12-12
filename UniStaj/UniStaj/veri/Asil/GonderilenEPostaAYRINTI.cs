using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("GonderilenEPostaAYRINTI")]
    public partial class GonderilenEPostaAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 gonderilenEPostaKimlik { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? adres { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? baslik { get; set; }

        public Int32? i_ePostaTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaTuru { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? gonderimTarihi { get; set; }

        [Column(TypeName = "text")]
        public string? metin { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        public Int64? y_topluGonderimKimlik { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? kisiAdi { get; set; }

    }
}
