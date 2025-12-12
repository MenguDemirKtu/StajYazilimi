using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    [Table("sil1")]
    public partial class sil1 : Bilesen
    {
        [Key]
        [Column(TypeName = "nvarchar(11)")]
        public string? tcKimlikNo { get; set; }

        public Int32? sayisi { get; set; }

    }
}
