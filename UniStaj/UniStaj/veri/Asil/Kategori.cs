using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Kategori : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "KategoriKimlik")]
        [Required]
        public Int32 kategorikimlik { get; set; }

        [Display(Name = "Kategori Adı")]
        [Column(TypeName = "nvarchar(50)")]
        public string? kategoriAdi { get; set; }

        [Display(Name = "Tanıtım")]
        [Column(TypeName = "nvarchar(500)")]
        public string? tanitim { get; set; }

        [Display(Name = "varmi")]
        public bool? varmi { get; set; }

    }
}
