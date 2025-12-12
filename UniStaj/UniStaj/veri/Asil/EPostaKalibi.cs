using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class EPostaKalibi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 ePostaKalibiKimlik { get; set; }

        [Display(Name = "Başlık")]
        [Column(TypeName = "nvarchar(200)")]
        public string? kalipBasligi { get; set; }

        [Display(Name = "Tür")]
        [Required]
        public Int32 i_ePostaTuruKimlik { get; set; }

        [Display(Name = "Kalıp")]
        [Column(TypeName = "text")]
        public string? kalip { get; set; }

        [Display(Name = "Geçerli mi")]
        [Required]
        public bool e_gecerlimi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

        [Display(Name = "Dil")]
        public Int32? i_dilKimlik { get; set; }

    }
}
