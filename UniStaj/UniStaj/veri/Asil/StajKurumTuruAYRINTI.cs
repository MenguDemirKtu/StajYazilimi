using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("StajKurumTuruAYRINTI")]
    public partial class StajKurumTuruAYRINTI : Bilesen
    {
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 stajKurumTurukimlik { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(200)")]
        public string? stajKurumTurAdi { get; set; }

        [Display(Name = ".")]
        public bool? e_argeFirmasiMi { get; set; }

        [Display(Name = ".")]
        [Column(TypeName = "nvarchar(20)")]
        public string? argeFirmasiMi { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
