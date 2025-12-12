using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajBirimYetkilisi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Staj Birim YetkilisiKimlik")]
        [Required]
        public Int32 stajBirimYetkilisikimlik { get; set; }

        [Display(Name = "TC Kimlik No")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = "Ad")]
        [Column(TypeName = "nvarchar(100)")]
        public string? ad { get; set; }

        [Display(Name = "Soyad")]
        [Column(TypeName = "nvarchar(100)")]
        public string? soyad { get; set; }

        [Display(Name = "Telefon")]
        [Column(TypeName = "nvarchar(11)")]
        public string? telefon { get; set; }

        [Display(Name = "E Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
