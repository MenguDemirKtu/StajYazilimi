using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("StajKurumuAYRINTI")]
    public partial class StajKurumuAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 stajKurumukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? stajKurumAdi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_stajkurumturukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? stajKurumTurAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? hizmetAlani { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? webAdresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? vergiNo { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(30)")]
        public string? ibanNo { get; set; }

        [Display(Name = ".")]
        public Int32? calisanSayisi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? adresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(12)")]
        public string? telNo { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(12)")]
        public string? faks { get; set; }

        [Display(Name = ".")]
        public bool? e_karaListedeMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? karaListedeMi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
