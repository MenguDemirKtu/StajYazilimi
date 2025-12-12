using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Ogretmen : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 ogretmenKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? adi { get; set; }

        public bool? varmi { get; set; }

    }
}
