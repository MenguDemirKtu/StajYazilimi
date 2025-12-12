using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class RolWebSayfasiIzni : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 rolWebSayfasiIzniKimlik { get; set; }

        [Required]
        public Int32 i_rolKimlik { get; set; }

        [Required]
        public Int32 i_webSayfasiKimlik { get; set; }

        public bool? e_gormeIzniVarmi { get; set; }

        public bool? e_eklemeIzniVarmi { get; set; }

        public bool? e_silmeIzniVarmi { get; set; }

        public bool? e_guncellemeIzniVarmi { get; set; }

        public bool? varmi { get; set; }

    }
}
