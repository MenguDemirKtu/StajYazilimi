using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ResimAyari : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 resimAyariKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ilgiliCizelge { get; set; }

        public Int32? genislik { get; set; }

        public Int32? yukseklik { get; set; }

        public bool e_genislik2Varmi { get; set; }

        public bool e_genislik3Varmi { get; set; }

        public bool e_genislik4Varmi { get; set; }

        public Int32? genislik2 { get; set; }

        public Int32? yukseklik2 { get; set; }

        public Int32? genislik3 { get; set; }

        public Int32? yukseklik3 { get; set; }

        public Int32? genislik4 { get; set; }

        public Int32? yukseklik4 { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? genislikSutunAdi2 { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? genislikSutunAdi3 { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? genislikSutunAdi4 { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? dizinAdi { get; set; }

        public Int32? kalite { get; set; }

    }
}
