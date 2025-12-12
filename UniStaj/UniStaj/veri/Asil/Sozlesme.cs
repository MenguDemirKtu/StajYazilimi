using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class Sozlesme : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 sozlesmekimlik { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? baslik { get; set; }

        [Required]
        public Int32 i_sozlesmeTuruKimlik { get; set; }

        public bool? e_gecerliMi { get; set; }

        [Column(TypeName = "text")]
        public string? metin { get; set; }

        public bool? varmi { get; set; }

    }
}
