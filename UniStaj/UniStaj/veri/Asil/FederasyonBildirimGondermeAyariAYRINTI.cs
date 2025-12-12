using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("FederasyonBildirimGondermeAyariAYRINTI")]
    public partial class FederasyonBildirimGondermeAyariAYRINTI : Bilesen
    {
        [Key]
        [Required]
        public Int32 federasyonBildirimGondermeAyariKimlik { get; set; }

        [Required]
        public Int64 i_kullaniciKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string kullaniciAdi { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? gercekAdi { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? telefon { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ePostaAdresi { get; set; }

        [Required]
        public Int32 i_federasyonBildirimTuruKimlik { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? federasyonBildirimTuruAdi { get; set; }

        public bool? e_gecerlimi { get; set; }

        public bool? varmi { get; set; }

    }
}
