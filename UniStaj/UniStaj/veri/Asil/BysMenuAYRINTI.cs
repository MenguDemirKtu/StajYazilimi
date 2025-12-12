using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("BysMenuAYRINTI")]
    public partial class BysMenuAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 bysMenuKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? bysMenuAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? bysMenuBicim { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_ustMenuKimlik { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_webSayfasiKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(400)")]
        public string? bysMenuUrl { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        public Int32? sirasi { get; set; }

        [Display(Name = ".")]
        public bool? e_modulSayfasimi { get; set; }

        [Display(Name = ".")]
        public Int32? i_modulKimlik { get; set; }

        [Display(Name = ".")]
        public bool? e_gosterilsimmi { get; set; }

        [Display(Name = ".")]
        public bool e_anaKullaniciGorsunmu { get; set; }

    }
}
