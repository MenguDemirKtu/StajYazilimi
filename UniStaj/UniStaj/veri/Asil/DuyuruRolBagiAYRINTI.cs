using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("DuyuruRolBagiAYRINTI")]
    public partial class DuyuruRolBagiAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 duyuruRolBagiKimlik { get; set; }

        [Required]
        public Int32 i_duyuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? baslik { get; set; }

        [Required]
        public Int32 i_rolKimlik { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? rolAdi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string? tanitim { get; set; }

        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? baglanti { get; set; }

        [Required]
        public Int32 sirasi { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime baslangic { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? bitis { get; set; }

        public bool? e_yayindami { get; set; }

    }
}
