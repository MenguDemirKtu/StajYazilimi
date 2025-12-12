using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class StajKurumTuru : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Staj Kurum TürüKimlik")]
        [Required]
        public Int32 stajKurumTurukimlik { get; set; }

        [Display(Name = "Staj Kurum Tür Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? stajKurumTurAdi { get; set; }

        [Display(Name = "Arge Firmasi mi")]
        public bool? e_argeFirmasiMi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
