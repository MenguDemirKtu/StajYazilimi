using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("RolWebSayfasiIzniAYRINTI")]
    public partial class RolWebSayfasiIzniAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 rolWebSayfasiIzniKimlik { get; set; }

        [Required]
        public Int32 i_rolKimlik { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? rolAdi { get; set; }

        [Required]
        public Int32 i_webSayfasiKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? hamAdresi { get; set; }

        public bool e_gormeIzniVarmi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? gormeIzniVarmi { get; set; }

        public bool e_eklemeIzniVarmi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string eklemeIzniVarmi { get; set; }

        public bool e_silmeIzniVarmi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? silmeIzniVarmi { get; set; }

        public bool e_guncellemeIzniVarmi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? guncellemeIzniVarmi { get; set; }

        public bool? varmi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? sayfaBasligi { get; set; }

        [Required]
        public Int32 i_modulKimlik { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        public bool? e_izinSayfasindaGorunsunmu { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? modulAdi { get; set; }

    }
}
