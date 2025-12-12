using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class ref_FederasyonBildirimTuru : Bilesen
    {
        [Key]
        [Required]
        public Int32 federasyonBildirimTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? federasyonBildirimTuruAdi { get; set; }

    }
}
