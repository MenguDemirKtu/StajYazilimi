using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Simge : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = ".")]
        [Required]
        public Int32 simgekimlik { get; set; }

        [Display(Name = "Başlık")]
        [Column(TypeName = "nvarchar(200)")]
        public string? baslik { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = ".")]
        public bool? varmi { get; set; }

    }
}
