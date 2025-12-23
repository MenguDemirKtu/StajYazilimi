using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajDonemi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Staj DönemiKimlik")]
        [Required]
        public Int32 stajDonemikimlik { get; set; }

        [Display(Name = "Staj Dönem Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? stajDonemAdi { get; set; }

        [Display(Name = "Tanım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanim { get; set; }

        [Display(Name = "Başlangıç")]
        [Column(TypeName = "datetime")]
        public DateTime? baslangic { get; set; }

        [Display(Name = "Bitiş")]
        [Column(TypeName = "datetime")]
        public DateTime? bitis { get; set; }

        [Display(Name = "Geçerli Dönemmi")]
        public bool? e_gecerliDonemmi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
