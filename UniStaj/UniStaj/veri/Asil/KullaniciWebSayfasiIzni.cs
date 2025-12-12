using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class KullaniciWebSayfasiIzni : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 kullaniciWebSayfasiIzniKimlik { get; set; }

        public Int32? i_kullaniciKimlik { get; set; }

        public Int32? i_webSayfasiKimlik { get; set; }

        [Required]
        public bool e_gormeIzniVarmi { get; set; }

        [Required]
        public bool e_eklemeIzniVarmi { get; set; }

        [Required]
        public bool e_silmeIzniVarmi { get; set; }

        [Required]
        public bool e_guncellemeIzniVarmi { get; set; }

        public bool? varmi { get; set; }

    }
}
