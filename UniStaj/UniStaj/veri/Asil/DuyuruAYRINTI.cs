using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("DuyuruAYRINTI")]
    public partial class DuyuruAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 duyurukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(300)")]
        public string? baslik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(1000)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(250)")]
        public string? baglanti { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 sirasi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? baslangic { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "datetime")]
        public DateTime? bitis { get; set; }

        [Display(Name = ".")]
        public bool? e_yayindami { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? yayindami { get; set; }

    }
}
