using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class SosyalMedya : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Sosyal MedyaKimlik")]
        [Required]
        public Int32 sosyalMedyakimlik { get; set; }

        [Display(Name = "Sosyam Medya Adı")]
        [Column(TypeName = "nvarchar(200)")]
        public string? sosyamMedyaAdi { get; set; }

        [Display(Name = "Foto")]
        public Int64? i_fotoKimlik { get; set; }

        [Display(Name = "Adres")]
        [Column(TypeName = "nvarchar(400)")]
        public string? adres { get; set; }

        [Display(Name = "Sırası")]
        public Int32? sirasi { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
