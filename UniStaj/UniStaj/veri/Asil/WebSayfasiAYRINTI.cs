using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("WebSayfasiAYRINTI")]
    public partial class WebSayfasiAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 webSayfasiKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? hamAdresi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sayfaBasligi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_modulKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(100)")]
        public string? modulAdi { get; set; }

        [Display(Name = ".")]
        [Required]
        public Int32 i_sayfaTuruKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "text")]
        public string? aciklama { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(256)")]
        public string? fotosu { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(50)")]
        public string? sayfaTuru { get; set; }

        [Display(Name = ".")]
        public bool? e_izinSayfasindaGorunsunmu { get; set; }

        [Display(Name = ".")]
        public bool? e_varsayilanEklemeAcikmi { get; set; }

        [Display(Name = ".")]
        public bool? e_varsayilanGorunmeAcikmi { get; set; }

        [Display(Name = ".")]
        public bool? e_varsayilanGuncellemeAcikmi { get; set; }

        [Display(Name = ".")]
        public bool? e_varsayilanSilmeAcikmi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(1500)")]
        public string? dokumAciklamasi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(1500)")]
        public string? kartAciklamasi { get; set; }

    }
}
