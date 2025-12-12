using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class BysMenu : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 bysMenuKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? bysMenuAdi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? bysMenuBicim { get; set; }

        [Required]
        public Int32 i_ustMenuKimlik { get; set; }

        [Required]
        public Int32 i_webSayfasiKimlik { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? bysMenuUrl { get; set; }

        public bool? varmi { get; set; }

        public Int32? sirasi { get; set; }

        public bool? e_modulSayfasimi { get; set; }

        public Int32? i_modulKimlik { get; set; }

        public bool? e_gosterilsimmi { get; set; }

        public bool e_anaKullaniciGorsunmu { get; set; }

    }
}
