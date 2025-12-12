using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class _cizelgeler : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required, Column(TypeName = "nvarchar(128)")]
        public string ad { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime olusturma { get; set; }

        [Required, Column(TypeName = "datetime")]
        public DateTime guncelleme { get; set; }

    }
}
