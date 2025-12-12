using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("RolAYRINTI")]
    public partial class RolAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 rolKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(250)")]
        public string? rolAdi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = ".")]
        [Required]
        public bool e_gecerlimi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? gecerlimi { get; set; }

        [Display(Name = ".")]
        [Required]
        public bool varmi { get; set; }

        [Display(Name = ".")]
        public bool? e_varsayilanmi { get; set; }

        [Display(Name = ".")]
        public Int32? i_varsayilanOlduguKullaniciTuruKimlik { get; set; }

        [Display(Name = ".")]
        public bool? e_rolIslemiIcinmi { get; set; }

        [Display(Name = ".")]
        public Int32? i_rolIslemiKimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(150)")]
        public string? kodu { get; set; }

    }
}
