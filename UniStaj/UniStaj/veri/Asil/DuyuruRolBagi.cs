using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class DuyuruRolBagi : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 duyuruRolBagiKimlik { get; set; }

        [Required]
        public Int32 i_duyuruKimlik { get; set; }

        [Required]
        public Int32 i_rolKimlik { get; set; }

        public bool? varmi { get; set; }

    }
}
