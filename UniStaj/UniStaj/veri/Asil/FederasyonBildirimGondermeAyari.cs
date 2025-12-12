using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class FederasyonBildirimGondermeAyari : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 federasyonBildirimGondermeAyariKimlik { get; set; }

        [Required]
        public Int64 i_kullaniciKimlik { get; set; }

        [Required]
        public Int32 i_federasyonBildirimTuruKimlik { get; set; }

        public bool? e_gecerlimi { get; set; }

        public bool? varmi { get; set; }

    }
}
