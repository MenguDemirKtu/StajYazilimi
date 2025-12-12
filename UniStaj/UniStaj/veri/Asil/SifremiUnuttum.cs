using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class SifremiUnuttum : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 sifremiUnuttumKimlik { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        public bool? e_smsmi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? ePosta { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        public string? telefon { get; set; }

        [Column(TypeName = "date")]
        public DateTime? tarih { get; set; }

        public bool? varmi { get; set; }

    }
}
