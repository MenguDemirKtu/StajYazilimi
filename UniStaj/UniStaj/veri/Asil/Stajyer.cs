using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class Stajyer : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "StajyerKimlik")]
        [Required]
        public Int32 stajyerkimlik { get; set; }

        [Display(Name = "Öğrenci No")]
        [Column(TypeName = "nvarchar(20)")]
        public string? ogrenciNo { get; set; }

        [Display(Name = "TC Kimlik No")]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        [Display(Name = "Staj Birimi")]
        [Required]
        public Int32 i_stajBirimiKimlik { get; set; }

        [Display(Name = "Stajyer Adı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? stajyerAdi { get; set; }

        [Display(Name = "Stajyer Soyadı")]
        [Column(TypeName = "nvarchar(100)")]
        public string? stajyerSoyadi { get; set; }

        [Display(Name = "Sınıfı")]
        public Int32? sinifi { get; set; }

        [Display(Name = "Telefon")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Display(Name = "E Posta")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
