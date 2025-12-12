using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("KullaniciRolWebSayfasiIzniAYRINTI")]
    public partial class KullaniciRolWebSayfasiIzniAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int64 rolWebSayfasiIzniKimlik { get; set; }

        [Required]
        public Int32 i_rolKimlik { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Required]
        public Int32 i_webSayfasiKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? hamAdresi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? sayfaBasligi { get; set; }

        public bool? e_gormeIzniVarmi { get; set; }

        public bool? e_eklemeIzniVarmi { get; set; }

        public bool? e_silmeIzniVarmi { get; set; }

        public bool? e_guncellemeIzniVarmi { get; set; }

        public bool? varmi { get; set; }

    }
}
