using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("KullaniciWebSayfasiIzniAYRINTI")]
    public partial class KullaniciWebSayfasiIzniAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 kullaniciWebSayfasiIzniKimlik { get; set; }

        public Int32? i_kullaniciKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        public Int32? i_webSayfasiKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? hamAdresi { get; set; }

        [Required]
        public bool e_gormeIzniVarmi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? gormeIzniVarmi { get; set; }

        [Required]
        public bool e_eklemeIzniVarmi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? eklemeIzniVarmi { get; set; }

        [Required]
        public bool e_silmeIzniVarmi { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? silmeIzniVarmi { get; set; }

        [Required]
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
