using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Galeri : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 galeriKimlik { get; set; }

        [Display(Name = "Galeri Adı")]
        [Column(TypeName = "nvarchar(300)")]
        public string? galeriAdi { get; set; }

        [Display(Name = "Galeri URL")]
        [Column(TypeName = "nvarchar(200)")]
        public string? galeriUrl { get; set; }

        [Display(Name = "İlgili Çizelge")]
        [Column(TypeName = "nvarchar(200)")]
        public string? ilgiliCizelge { get; set; }

        [Display(Name = "İlgili Kimlik ")]
        public Int64? ilgiliKimlik { get; set; }

        [Display(Name = "Genişlik")]
        public Int32? genislik { get; set; }

        [Display(Name = "Yükseklik")]
        public Int32? yukseklik { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "Fotosu")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Kodu")]
        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

    }
}
