using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajBirimYetkilisiBirimi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Staj Birim Yetkilisi BirimiKimlik")]
        [Required]
        public Int32 stajBirimYetkilisiBirimikimlik { get; set; }

        [Display(Name = "Staj Birim Yetkilisi")]
        public Int32? i_stajBirimYetkilisiKimlik { get; set; }

        [Display(Name = "Staj Birimi")]
        public Int32? i_stajBirimiKimlik { get; set; }

        [Display(Name = "Geçerli mi")]
        public bool? e_gecerliMi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
