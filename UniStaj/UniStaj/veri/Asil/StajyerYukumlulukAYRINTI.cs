using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("StajyerYukumlulukAYRINTI")]
    public partial class StajyerYukumlulukAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 stajyerYukumlulukkimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_stajyerKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? ogrenciNo { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_stajTuruKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? stajTuruAdi { get; set; }

        [Display(Name = ".")]
        public Int32? gunSayisi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(10)")]
        public string? siniflar { get; set; }

        [Display(Name = ".")]
        public Int32? yaptigiGunSayis { get; set; }

        [Display(Name = ".")]
        public Int32? kabulEdilenGunSayisi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
