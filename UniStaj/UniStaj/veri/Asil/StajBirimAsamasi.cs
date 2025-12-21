using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajBirimAsamasi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Staj Birim AşamasıKimlik")]
        [Required]
        public Int32 stajBirimAsamasikimlik { get; set; }

        [Display(Name = "Staj Birimi")]
        [Required]
        public Int32 i_stajBirimiKimlik { get; set; }

        [Display(Name = "Staj Aşaması")]
        [Required]
        public Int32 i_stajAsamasiKimlik { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "Geçerli mi")]
        public bool e_gecerliMi { get; set; }

        [Display(Name = "Açıklama")]
        [Column(TypeName = "nvarchar(200)")]
        public string? aciklama { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
