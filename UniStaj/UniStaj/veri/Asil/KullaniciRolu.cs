using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    public partial class KullaniciRolu : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 kullaniciRoluKimlik { get; set; }

        [Required]
        public Int32 i_kullaniciKimlik { get; set; }

        [Required]
        public Int32 i_rolKimlik { get; set; }

        [Required]
        public bool e_gecerlimi { get; set; }

        public bool? varmi { get; set; }

    }
}
