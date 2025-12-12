using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class SayfaDegisimi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 sayfaDegisimiKimlik { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ad { get; set; }

        public bool? varmi { get; set; }

    }
}
