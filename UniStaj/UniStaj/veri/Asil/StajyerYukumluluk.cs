using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajyerYukumluluk : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Stajyer YükümlülükKimlik")]
        [Required]
        public Int32 stajyerYukumlulukkimlik { get; set; }

        [Display(Name = "Stajyer")]
        [Required]
        public Int32 i_stajyerKimlik { get; set; }

        [Display(Name = "Staj Türü")]
        [Required]
        public Int32 i_stajTuruKimlik { get; set; }

        [Display(Name = "Gün Sayısı")]
        public Int32? gunSayisi { get; set; }

        [Display(Name = "Sınıflar")]
        [Column(TypeName = "nvarchar(10)")]
        public string? siniflar { get; set; }

        [Display(Name = "Yaptığı Gün Sayıs")]
        public Int32? yaptigiGunSayis { get; set; }

        [Display(Name = "Kabul Edilen Gün Sayısı")]
        public Int32? kabulEdilenGunSayisi { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "nvarchar(100)")]
        public string? aciklama { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
