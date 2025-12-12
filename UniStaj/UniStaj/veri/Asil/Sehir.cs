using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Sehir : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 sehirKimlik { get; set; }

        [Display(Name = "Şehir Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sehirAdi { get; set; }

        [Display(Name = "Telefon Kodu")]
        [Column(TypeName = "nvarchar(5)")]
        public string? telefonKodu { get; set; }

        [Display(Name = "Plaka Kodu")]
        [Column(TypeName = "nvarchar(5)")]
        public string? plakaKodu { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
