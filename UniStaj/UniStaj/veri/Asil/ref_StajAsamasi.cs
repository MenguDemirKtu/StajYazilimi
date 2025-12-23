using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{

    [Table("ref_StajAsamasi")]
    public partial class ref_StajAsamasi : Bilesen
    {
        [Key]
        [Required]
        public Int32 StajAsamasiKimlik { get; set; }

        [Required, Column(TypeName = "nvarchar(200)")]
        public string StajAsamasiAdi { get; set; }

    }
}
