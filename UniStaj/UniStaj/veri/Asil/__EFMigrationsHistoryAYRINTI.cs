using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniStaj.veri
{
    public partial class __EFMigrationsHistoryAYRINTI : Bilesen
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required, Column(TypeName = "nvarchar(150)")]
        public string MigrationId { get; set; }

        [Required, Column(TypeName = "nvarchar(32)")]
        public string ProductVersion { get; set; }

    }
}
